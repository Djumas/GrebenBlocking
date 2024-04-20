using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationComponent : MonoBehaviour
{    

    [SerializeField] float rotationRate;

    private Vector3 targetDirection;

    private bool performRotation;

    public void SetRotation(Vector3 direction) {
        if (direction.magnitude >= 0.2)
        {
            targetDirection = direction;
            performRotation = true;
        }
        else {
            performRotation = false;
        }
    }

    private void Update()
    {
        if (performRotation) {
            Debug.Log(targetDirection);
            var targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationRate);
        }
    }
}
