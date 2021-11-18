using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetFollowTarget : MonoBehaviour
{
    public CinemachineBrain brain;
    public CapsuleCollider camerCollider;

    private int index = 0;
    [SerializeField]
    private CinemachineVirtualCameraBase currentCamera;
    [SerializeField]
    private CinemachineVirtualCameraBase previousCamera;
    [SerializeField]
    private CinemachineVirtualCameraBase nextCamera;
    [SerializeField]
    private CinemachineClearShot clearShot;

    private CinemachineVirtualCameraBase[] Cameras;
    

    void Start()
    {
        clearShot = GetComponent<CinemachineClearShot>();
        Cameras = clearShot.ChildCameras;

        previousCamera = Cameras[index + 2];
        currentCamera = Cameras[index];
        nextCamera = Cameras[index + 1];
        index++;

        currentCamera.Follow = camerCollider.transform;
    }

    void Update()
    {
        if (clearShot.LiveChild as Object == nextCamera)
        {
            index++;
            currentCamera.Follow = null;

            previousCamera = currentCamera;
            currentCamera = nextCamera;
            nextCamera = Cameras[index];
            currentCamera.Follow = camerCollider.transform;
        }

        if (clearShot.LiveChild as Object == previousCamera)
        {
            index--;
            currentCamera.Follow = null;

            nextCamera = currentCamera;
            currentCamera = previousCamera;
            previousCamera = Cameras[index];
            currentCamera.Follow = GameObject.FindGameObjectWithTag("CameraCollider").transform;
        }
    }
}
