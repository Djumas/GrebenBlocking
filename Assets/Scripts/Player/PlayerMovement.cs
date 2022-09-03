using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public SOTest testScriptableObject;
    public float smoothing = 5f;
    public float rotSpeed = 1;
    public CinemachineClearShot clearShot;
    public float xShortActionTreshold = 0.5f;
    public float yShortActionTreshold = 0.5f;
    
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
    public float dodgeWalkTimeTresh = 0.3f;
    public float dodgePressTimeTresh = 0.3f;
    

    [HideInInspector]
    private Animator anim;
    private Vector3 direction = new Vector3(0, 0, 0);
    private Quaternion rotQuat;
    private Gamepad gamepad;
    private float xButtonPressedFor = 0f;
    private float yButtonPressedFor = 0f;
    private float aButtonPressedFor = 0f;
    private CinemachineVirtualCamera currentCamera;
    private bool inRunMode = false;
    private bool dodged = false;
    private bool isWalking = false;
    public bool isDodging = false;
    public bool isAttacking = false;
    private Character character;
    private Vector3 deltaPosition;
    private bool aButtonPressed;
    private CharControllerRMAnim controllerRmAnim;
    
    private bool xActionDone = false;
    private bool yActionDone = false;

    private void Awake()
    {
       currentCamera = (CinemachineVirtualCamera)clearShot.LiveChild;
       anim = GetComponent<Animator>();
       gamepad = Gamepad.current;
       character = GetComponent<Character>();
       controllerRmAnim = GetComponent<CharControllerRMAnim>();
    }

    private void Update()
    {
        UpdateCamera();
        ReadMovement();
        ReadJoystick();
        controllerRmAnim.updateY = !isDodging;
    }

    private void UpdateCamera() {
        currentCamera = (CinemachineVirtualCamera)clearShot.LiveChild;
    }


    private void ReadJoystick()
    {
        if (character.unitStatus == UnitStatus.KnockOut) {
            return;
        }
        #region xButton manage
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
        #endregion
        #region yButton manage
     
        if (gamepad.yButton.wasPressedThisFrame)
        {
            yActionDone = false;
            yButtonPressedFor = 0f;
        }
        else
        {
            if (gamepad.yButton.isPressed)
            {
                yButtonPressedFor += Time.deltaTime;
                CheckJoystickY(false);
            }
        }

        if (gamepad.yButton.wasReleasedThisFrame)
        {
            CheckJoystickY(true);
            yButtonPressedFor = 0f;
        }
        
        #endregion 
        

        if (gamepad.aButton.isPressed)
        {
            aButtonPressedFor += Time.deltaTime;
            CheckJoystickA(false);
        }
        
        if (gamepad.aButton.wasReleasedThisFrame)
        {
            CheckJoystickA(true);
            aButtonPressedFor = 0f;
        }
        
        
    }

    private void CheckJoystickA(bool released)
    {
        if (!released)
        {
            aButtonPressed = true;
        }
        else
        {
            aButtonPressed = false;
            dodged = false;
        }
    }

    private void CheckJoystickX(bool released) {
//        Debug.Log("CheckJoystickX");
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
    
    private void CheckJoystickY(bool released) {
        Debug.Log("CheckJoystickX");
        //Checking x-botton pressing result: Was it a "single" or continious (but short) pressing. Invoking corresponding action.
        if (released)
        {
            if (!yActionDone) {
                if (yButtonPressedFor < yShortActionTreshold)
                {
                    YInstantAction();
                }
                else
                {
                    YShortAction();
                }
            }
            yActionDone = false;
        }
        
        if(!released)
        {
            if (!yActionDone)
            {
                if (yButtonPressedFor >= yShortActionTreshold)
                {
                    YShortAction();
                    yActionDone = true;
                }                
            }
        }

    }

    public void YInstantAction()
    {
        Debug.Log("YInstantAction");
        anim.SetTrigger("yInstant");
    }

    public void YShortAction()
    {
        Debug.Log("YShortAction");
        anim.SetTrigger("yShort");
    }
    
    public void XInstantAction()
    {
//        Debug.Log("XInstantAction");
        anim.SetTrigger("xInstant");
    }

    public void XShortAction()
    {
//        Debug.Log("XShortAction");
        //Kick();
        anim.SetTrigger("xShort");
    }
/*
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
    }*/

    public void TurnToClosestEnemy(float angleRange, float distance)
    {
//        Debug.Log("TurnToClosestEnemy");
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
        
        if (aButtonPressed && !dodged && isWalking && (aButtonPressedFor <= dodgePressTimeTresh))
        {
            if (walkingTime > dodgeWalkTimeTresh)
            {
                anim.SetTrigger("DodgeForward");
            }
            else
            {
                //anim.SetTrigger("DodgeForward");
                anim.SetTrigger("DodgeDir");
            }

            dodged = true;
        }

        direction = h * currentCamera.transform.right + v * (currentCamera.transform.forward - (currentCamera.transform.forward.y) * (new Vector3(0, 1, 0)).normalized);
        if(!isDodging) SetMoveAnim();
        
        if (isWalking)
        {
            walkingTime += Time.deltaTime;
            
            if (!inFocus && !isDodging && !isAttacking)
            {
                rotQuat.SetLookRotation(direction.normalized);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotQuat, Time.deltaTime * rotSpeed);
            }
        }
        else {
            walkingTime = 0f;
        }
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
        var blendTreeDirection = transform.InverseTransformDirection(direction);
        var dodgeTreeDirection = blendTreeDirection.normalized;

        anim.SetFloat("X", blendTreeDirection.x);
        anim.SetFloat("Z", blendTreeDirection.z);
        
        anim.SetFloat("dodgeX", dodgeTreeDirection.x);
        anim.SetFloat("dodgeZ", dodgeTreeDirection.z);
        //Debug.Log(dodgeTreeDirection.magnitude);
    }
}