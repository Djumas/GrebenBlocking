using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public float smoothing = 5f;
    public float rotSpeed = 1;
    public CinemachineClearShot clearShot;

    [HideInInspector]
    public static bool inFocus = false;

    private float hsmt = 0f;
    private float vsmt = 0f;
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

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        hsmt = Mathf.Lerp(hsmt, h, Time.deltaTime * smoothing);
        vsmt = Mathf.Lerp(vsmt, v, Time.deltaTime * smoothing);

        bool isWalking = (Mathf.Abs(hsmt) >= 0.1 || Mathf.Abs(vsmt) >= 0.1);
        anim.SetBool("isMoving", isWalking);

        if (isWalking)
        {
            direction = hsmt * currentCamera.transform.right + vsmt * (currentCamera.transform.forward-(currentCamera.transform.forward.y)*(new Vector3(0,1,0)));
            Rotate();
            SetMoveAnim();
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