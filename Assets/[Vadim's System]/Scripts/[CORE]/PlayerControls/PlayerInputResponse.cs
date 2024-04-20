using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputResponse : MonoBehaviour
{
    [SerializeField] MovementComponent movementComponent;
    [SerializeField] RotationComponent rotationComponent;
    [SerializeField] Camera currentCamera;

    private bool _modifierPressed;

    private void Awake()
    {
        currentCamera = Camera.main;
    }

    public void OnMove(InputValue value)
    {
        var readValue = value.Get<Vector2>();
        var direction = readValue.x * currentCamera.transform.right + readValue.y * (currentCamera.transform.forward - (currentCamera.transform.forward.y) * (new Vector3(0, 1, 0)).normalized);
        movementComponent.SetMovement(direction);
        rotationComponent.SetRotation(direction);
    }

    public void OnKick(InputValue value)
    {
        if (!value.isPressed) return;
        if (_modifierPressed)
        {
            Debug.Log("Heavy Kick performed");
        }
        else Debug.Log("Kick performed");
    }
    

    public void OnPunch(InputValue value)
    {
        //if (!value.isPressed) return;
        if (_modifierPressed)
        {
            Debug.Log("Heavy punch performed");
        }
        else Debug.Log("Punch performed"+value.isPressed);
    }


    public void OnModifier(InputValue value)
    {
        _modifierPressed = value.isPressed;
    }

}
