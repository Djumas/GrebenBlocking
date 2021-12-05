using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public float smoothing = 5f;
    public float rotSpeed = 1;
    public CinemachineClearShot clearShot;

    [HideInInspector]
    public static bool inFocus = false;
    private Animator anim;
    private Vector3 direction = new Vector3(0, 0, 0);
    private Quaternion rotQuat;

    private CinemachineVirtualCamera currentCamera;

    private void Awake()
    {
       currentCamera = (CinemachineVirtualCamera)clearShot.LiveChild;
        anim = GetComponent<Animator>();
    }

    public void Test() {
        Debug.Log("Test");
    }

    private void Update()
    {
        Move();
        
    }


    public void Move()
    {
        currentCamera = (CinemachineVirtualCamera)clearShot.LiveChild;

        var gamepad = Gamepad.current;
        
        float h = gamepad.leftStick.x.ReadValue();
        float v = gamepad.leftStick.y.ReadValue();

        if (gamepad.rightShoulder.isPressed)
        {
            anim.SetBool("inRunMode", true);
        }
        else {
            anim.SetBool("inRunMode", false);
        }

        
        bool isWalking = (Mathf.Abs(h) >= 0.3 || Mathf.Abs(v) >= 0.3);
        anim.SetBool("isMoving", isWalking);



        if (isWalking)
        {
            direction = h * currentCamera.transform.right + v * (currentCamera.transform.forward-(currentCamera.transform.forward.y)*(new Vector3(0,1,0)));
            Rotate();
            //SetMoveAnim();
        }
    }

    private void Rotate()
    {
        if (!inFocus)
        {
            rotQuat.SetLookRotation(direction.normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotQuat, Time.deltaTime * rotSpeed);
        }
    }


    private void SetMoveAnim()
    {
        Vector3 blendTreeDirection = transform.InverseTransformDirection(direction);

        anim.SetFloat("X", blendTreeDirection.x);
        anim.SetFloat("Z", blendTreeDirection.z);
    }
}