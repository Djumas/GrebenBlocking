using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void SetMovement(Vector3 direction)
    {
        animator.SetFloat("MovementX",direction.x);
        animator.SetFloat("MovementY", direction.z);

        if (direction.magnitude > 0.1)
        {
            animator.SetBool("Move", true);
        }
        else {
            animator.SetBool("Move", false);
        }
    }
}
