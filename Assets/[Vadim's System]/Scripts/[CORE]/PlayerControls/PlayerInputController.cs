using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputController : MonoBehaviour
{
    [SerializeField] MovementComponent movementComponent;
    [SerializeField] RotationComponent rotationComponent;
    [SerializeField] Camera currentCamera;
    [SerializeField] private BehaviorTree behaviorTree;
    
    private SharedBool kickPressed;
    private SharedBool punchPressed;
    private SharedBool modifierPressed;
    

    private void Awake()
    {
        kickPressed = (SharedBool)behaviorTree.GetVariable("KickPressed");
        punchPressed = (SharedBool)behaviorTree.GetVariable("PunchPressed");
        modifierPressed = (SharedBool)behaviorTree.GetVariable("ModifierPressed");
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
        kickPressed.Value = value.isPressed;
        Debug.Log("KickPressed:"+value.isPressed);
    }
    

    public void OnPunch(InputValue value)
    {
        punchPressed.Value = value.isPressed;
        Debug.Log("PunchPressed:"+value.isPressed);
    }


    public void OnModifier(InputValue value)
    {
        modifierPressed.Value = value.isPressed;
        Debug.Log("ModifierPressed:"+value.isPressed);
    }

}
