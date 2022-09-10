using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public float smoothing = 5f;
    public float rotSpeed = 1;
    public CinemachineClearShot clearShot;
    public float xShortActionTreshold = 0.5f;
    public float yShortActionTreshold = 0.5f;
    public float leftStickDirectedTresh = 0.3f;
    public float leftStickResetTresh = 0.1f;
    
    public Character currentTargetCharacter;
    public static bool setFocus = false;
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
    
    public float inputX;
    public float inputY;
    
    [HideInInspector]
    private Animator anim;
    //private Vector3 direction = new Vector3(0, 0, 0);
    private Quaternion rotQuat;
    private Gamepad gamepad;
    private float xButtonPressedFor = 0f;
    private float yButtonPressedFor = 0f;
    private float aButtonPressedFor = 0f;
    private CinemachineVirtualCamera currentCamera;
    private bool inRunMode = false;
    public bool dodged = false;
    private bool isLeftStickDirected = false;
    private bool isLeftStickReset = false;
    public bool isDodging = false;
    public bool isAttacking = false;
    private Character character;
    private Vector3 deltaPosition;
    private bool aButtonPressed;
    private bool lShoulderPressed;
    private CharControllerRMAnim controllerRmAnim;
    private GameplaySpeedManager gameplaySpeedManager;
    
    private bool xActionDone = false;
    private bool yActionDone = false;

    private void Awake()
    {
       currentCamera = (CinemachineVirtualCamera)clearShot.LiveChild;
       anim = GetComponent<Animator>();
       gamepad = Gamepad.current;
       character = GetComponent<Character>();
       controllerRmAnim = GetComponent<CharControllerRMAnim>();
       gameplaySpeedManager = GameplaySpeedManager.Instance;
    }

    private void Update()
    {
        UpdateCamera();
        ReadMovement();
        ReadJoystick();
        SetCameraFocus();
        
        //controllerRmAnim.updateY = !isDodging;
    }

    private void UpdateCamera() {
        currentCamera = (CinemachineVirtualCamera)clearShot.LiveChild;
    }

    private void ReadJoystick()
    {
        if (character.unitStatus == UnitStatus.KnockOut) return;
        #region xButton manage
        if (gamepad.xButton.wasPressedThisFrame)
        {
            CheckJoystickX(true);
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
        #region aButton manage
        if (gamepad.aButton.isPressed) CheckJoystickA(false);
        if (gamepad.aButton.wasReleasedThisFrame) CheckJoystickA(true);
        #endregion
        #region lShoulder manage
        if (gamepad.leftShoulder.wasPressedThisFrame) CheckLeftShoulder(true);
        if (gamepad.leftShoulder.wasReleasedThisFrame) CheckLeftShoulder(false);
        #endregion
    }

    private void CheckLeftShoulder(bool isPressed)
    {
        lShoulderPressed = isPressed;
        if (isPressed) gameplaySpeedManager.SetGameplaySpeed();
        else gameplaySpeedManager.ResetGameplaySpeed();
    }
    

    private void CheckJoystickA(bool released)
    {
        if (!released)
        {
            aButtonPressedFor += Time.deltaTime;
            aButtonPressed = true;
            isDodging = true;
        }
        else
        {
            if (aButtonPressedFor <= dodgePressTimeTresh && !dodged) anim.SetTrigger("DodgeForward");
            aButtonPressedFor = 0f;
            aButtonPressed = false;
            dodged = false;
            isDodging = false;
        }
    }

    private void CheckJoystickX(bool released) 
    {
        XInstantAction();
        /*if (released)
        {
            if (xButtonPressedFor < xShortActionTreshold && !xActionDone) XInstantAction(); 
            else XShortAction();
            xActionDone = false;
        }
        
        if (xButtonPressedFor >= xShortActionTreshold && !xActionDone && !released)
        { 
             XShortAction();
             xActionDone = true;
                    
        }*/
    }
    
    private void CheckJoystickY(bool released) 
    {
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
        var direction = inputX * currentCamera.transform.right + inputY * (currentCamera.transform.forward - (currentCamera.transform.forward.y) * (new Vector3(0, 1, 0)).normalized);
        
        inputX = gamepad.leftStick.x.ReadValue();
        inputY = gamepad.leftStick.y.ReadValue();
        
        if (character.unitStatus == UnitStatus.KnockOut) return;
        
        inRunMode = gamepad.rightShoulder.isPressed;
        anim.SetBool("inRunMode", inRunMode);

        isLeftStickDirected = Mathf.Abs(inputX) >= leftStickDirectedTresh || Mathf.Abs(inputY) >= leftStickDirectedTresh;
        isLeftStickReset = Mathf.Abs(inputX) <= leftStickResetTresh && Mathf.Abs(inputY) <= leftStickResetTresh;
        
     
        if (isLeftStickReset)
        {
            dodged = false;
            anim.ResetTrigger("DodgeDir");
        }
        
        if (isDodging && isLeftStickDirected && !dodged)
        {
            Debug.Log("DodgeDir");
            anim.SetTrigger("DodgeDir");
            SetDodgeAnim(direction);
            dodged = true;
        }

        if (!isDodging && setFocus)
        {
            SetMoveAnim(direction);
        }

        if (!isDodging && isLeftStickDirected)
        {
            anim.SetBool("isMoving",true);
            walkingTime += Time.deltaTime;
        }
        else
        {
            walkingTime = 0f;
            anim.SetBool("isMoving", false);
        }
        
        if (!setFocus && !isDodging && !isAttacking && isLeftStickDirected)
        {
            rotQuat.SetLookRotation(direction.normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotQuat, Time.deltaTime * rotSpeed);
        }
    }

    private void SetCameraFocus() 
    {
        var h = gamepad.leftStick.x.ReadValue();
        if (useFocusDirection)
        {
            cameraFocus.transform.position = transform.position + cameraFocus.transform.forward * cameraFocusDistance;
            return;
        }

        if (useFocusVelocity)
        {
            float xVelocity;
            if (walkingTime > cameraFocusWalkingTreshhold || inRunMode) xVelocity = currentCamera.transform.InverseTransformDirection(anim.velocity).x * cameraFocusVelocity;
            else xVelocity = 0;
            cameraFocus.transform.position = Vector3.Lerp(cameraFocus.transform.position, (transform.position + xVelocity * currentCamera.transform.right), cameraFocusSpeed);
        }
        else
        {
            if (walkingTime < cameraFocusWalkingTreshhold && !inRunMode) h = 0;
            if (!inRunMode) cameraFocus.transform.position = Vector3.Lerp(cameraFocus.transform.position, (transform.position + h * currentCamera.transform.right * cameraFocusDistance), cameraFocusSpeed);
            else cameraFocus.transform.position = Vector3.Lerp(cameraFocus.transform.position, (transform.position + h * currentCamera.transform.right * cameraFocusDistanceRunning), cameraFocusSpeed);
        }
    }

    private void SetMoveAnim(Vector3 direction)
    {
        var blendTreeDirection = transform.InverseTransformDirection(direction);
        anim.SetFloat("X", blendTreeDirection.x);
        anim.SetFloat("Z", blendTreeDirection.z);
    }

    private void SetDodgeAnim(Vector3 direction)
    {
        var dodgeTreeDirection = transform.InverseTransformDirection(direction).normalized;
        anim.SetFloat("dodgeX", dodgeTreeDirection.x);
        anim.SetFloat("dodgeZ", dodgeTreeDirection.z);
    }
}