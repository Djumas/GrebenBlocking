using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;

    private Vector3 offset;
    private Collider cameraCollider;

    private void Start()
    {
        cameraCollider = GetComponent<Collider>();
        offset = playerTransform.transform.position;
    }
    private void LateUpdate()
    {
        cameraCollider.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, offset.z);
    }
}
