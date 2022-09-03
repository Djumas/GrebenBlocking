using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public SOTest testScriptableObject;
    //public Rigidbody rigidBody;
    public float smoothing = 5f;
    public float rotSpeed = 1;
    public CinemachineClearShot clearShot;
    public float xShortActionTreshold = 0.5f;
    public bool xActionDone = false;
    public Character currentTargetCharacter;
    public static bool inFocus = false;
    public float kickSearchRange = 2.0f;
    public float kickSearchAngle = 90f;
    public GameObject cameraFocus;
    public float cameraFocusSpeed = 0.1f;
    public float cameraFocusDistance = 2f;
    public float cameraFocusDistanceRunning = 4f;
    public bool useFocusVelocity = false;
    public float cameraFocusVelocity = 0.35f;
    public bool useFocusDirection = false;
    public float cameraFocusWalkingTreshhold = 0.5f;
    public float walkingTime = 0f;
    

    [HideInInspector]
    private Animator anim;
    private Vector3 direction = new Vector3(0, 0, 0);
    private Quaternion rotQuat;
    public Gamepad gamepad;
    private float xButtonPressedFor = 0f;
    private CinemachineVirtualCamera currentCamera;
    private bool inRunMode = false;
    private bool isWalking = false;
    private CharacterController _controller;
    private Character character;
    private Vector3 _deltaPosition;
    

    private void Start()
    {
       gamepad = Gamepad.current;
       //rigidBody = GetComponent<Rigidbody>();
       character = GetComponent<Character>();
       _controller = GetComponent<CharacterController>();
    }

    void Awake()
    {
       currentCamera = (CinemachineVirtualCamera)clearShot.LiveChild;
        anim = GetComponent<Animator>();
       
    }

    private void Update()
    {
        UpdateCamera();
        ReadMovement();
        ReadJoystick();
    }

    private void UpdateCamera() {
        currentCamera = (CinemachineVirtualCamera)clearShot.LiveChild;
    }


    void ReadJoystick()
    {
        if (character.unitStatus == UnitStatus.KnockOut) {
            return;
        }

        if (gamepad.xButton.wasPressedThisFrame)
        {
            xActionDone = false;
            xButtonPressedFor = 0f;
        }
        else
        {
            if (gamepad.xButton.isPressed)
            {
                xButtonPressedFor += Time.deltaTime;
                CheckJoystickX(false);
            }
        }

        if (gamepad.xButton.wasReleasedThisFrame)
        {
            CheckJoystickX(true);
            xButtonPressedFor = 0f;
        }


    }

    void CheckJoystickX(bool released) {
        Debug.Log("CheckJoystickX");
        //Checking x-botton pressing result: Was it a "single" or continious (but short) pressing. Invoking corresponding action.
        if (released)
        {
            if (!xActionDone) {
                if (xButtonPressedFor < xShortActionTreshold)
                {
                    XInstantAction();
                }
                else
                {
                    XShortAction();
                }
            }
            xActionDone = false;
        }
        
        if(!released)
        {
            if (!xActionDone)
            {
                if (xButtonPressedFor >= xShortActionTreshold)
                {
                    XShortAction();
                    xActionDone = true;
                }                
            }
        }

    }

    public void XInstantAction()
    {
        Debug.Log("XInstantAction");
        anim.SetTrigger("xInstant");
    }

    public void XShortAction()
    {
        Debug.Log("XShortAction");
        //Kick();
        anim.SetTrigger("xShort");
    }

    public void TurnToClosestEnemy()
    {
        Debug.Log("TurnToClosestEnemy");
        Character closestEnemy = UnitManager.Instance.GetClosestEnemyByAngle(transform, kickSearchRange, kickSearchAngle);
        Vector3 playerToCharVector;
        
        if (closestEnemy != null)
        {
            playerToCharVector = closestEnemy.transform.position - transform.position;
            //transform.LookAt(closestEnemy.transform.position, Vector3.up);
            transform.forward = new Vector3(playerToCharVector.x,0, playerToCharVector.z);
        }
    }

    public void TurnToClosestEnemy(float angleRange, float distance)
    {
        Debug.Log("TurnToClosestEnemy");
        Character closestEnemy = UnitManager.Instance.GetClosestEnemyByAngle(transform, distance, angleRange);
        Vector3 playerToCharVector;

        if (closestEnemy != null)
        {
            playerToCharVector = closestEnemy.transform.position - transform.position;
            //transform.LookAt(closestEnemy.transform.position, Vector3.up);
            transform.forward = new Vector3(playerToCharVector.x, 0, playerToCharVector.z);
        }
    }



    public void ReadMovement()
    {
        if (character.unitStatus == UnitStatus.KnockOut)
        {
            return;
        }
        float h = gamepad.leftStick.x.ReadValue();
        float v = gamepad.leftStick.y.ReadValue();

        if (gamepad.rightShoulder.isPressed)
        {
            inRunMode = true;
        }
        else
        {
            inRunMode = false;
        }
        anim.SetBool("inRunMode", inRunMode);


        isWalking = (Mathf.Abs(h) >= 0.3 || Mathf.Abs(v) >= 0.3);
        anim.SetBool("isMoving", isWalking);
 
        if (useFocusDirection)
        {
            SetCameraFocusByDirection();
        } else
        {
            SetCameraFocus(h);
        }

        if (isWalking)
        {
            walkingTime += Time.deltaTime;
            direction = h * currentCamera.transform.right + v * (currentCamera.transform.forward - (currentCamera.transform.forward.y) * (new Vector3(0, 1, 0)));
            if (!inFocus)
            {
                rotQuat.SetLookRotation(direction.normalized);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotQuat, Time.deltaTime * rotSpeed);
            }

        }
        else {
            walkingTime = 0f;
        }
    }

    private Vector3 GetDeltaPosition() {
        _deltaPosition = anim.deltaPosition;
        _deltaPosition.y = Physics.gravity.y * Time.deltaTime;
        return _deltaPosition;
    }

    private void OnAnimatorMove()
    {
        
        _controller.Move(GetDeltaPosition());

    }

    private void SetCameraFocus(float h) {
        float xVelocity;
        if (useFocusVelocity)
        {
            if (walkingTime > cameraFocusWalkingTreshhold || inRunMode)
            {
                xVelocity = currentCamera.transform.InverseTransformDirection(anim.velocity).x * cameraFocusVelocity;
            }
            else
            {
                xVelocity = 0;
            }
            cameraFocus.transform.position = Vector3.Lerp(cameraFocus.transform.position, (transform.position + xVelocity * currentCamera.transform.right), cameraFocusSpeed);
        }
        else
        {
            if (walkingTime < cameraFocusWalkingTreshhold && !inRunMode)
            {
                h = 0;
            }

            if (!inRunMode)
            {
                cameraFocus.transform.position = Vector3.Lerp(cameraFocus.transform.position, (transform.position + h * currentCamera.transform.right * cameraFocusDistance), cameraFocusSpeed);
            }
            else
            {
                cameraFocus.transform.position = Vector3.Lerp(cameraFocus.transform.position, (transform.position + h * currentCamera.transform.right * cameraFocusDistanceRunning), cameraFocusSpeed);
            }
        }

    }

    private void SetCameraFocusByDirection()
    {
        cameraFocus.transform.position = transform.position + cameraFocus.transform.forward * cameraFocusDistance;
    }

    private void SetMoveAnim()
    {
        Vector3 blendTreeDirection = transform.InverseTransformDirection(direction);

        anim.SetFloat("X", blendTreeDirection.x);
        anim.SetFloat("Z", blendTreeDirection.z);
    }
}