using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAtCamera : MonoBehaviour
{
    Transform _cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        _cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(_cameraTransform.forward);
    }
}
