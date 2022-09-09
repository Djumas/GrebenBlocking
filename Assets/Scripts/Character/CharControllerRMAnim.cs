﻿using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class CharControllerRMAnim : MonoBehaviour
{
    // Start is called before the first frame update
    
    private CharacterController characterController;
    private Animator animator;
    private Vector3 deltaPosition;
    public bool updateY = true;
    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private Vector3 GetDeltaPosition() {
        deltaPosition = animator.deltaPosition;
        if(updateY && !characterController.isGrounded) deltaPosition.y = Physics.gravity.y * Time.deltaTime;
        return deltaPosition;
    }
    
    private void OnAnimatorMove()
    {
        characterController.Move(GetDeltaPosition());
    }
}