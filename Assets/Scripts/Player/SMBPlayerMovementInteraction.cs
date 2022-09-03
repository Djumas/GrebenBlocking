using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMBPlayerMovementInteraction : StateMachineBehaviour
{
    public bool turnToClosestEnemy = false;
    public float angleRange;
    public float distance;
    public bool setDodging = false;
    public bool dodgingValue = false;
    public bool setAttacking = false;
    public bool attackingValue = false;
    
    private PlayerMovement playerMovement;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerMovement = animator.gameObject.GetComponent<PlayerMovement>();

        if (turnToClosestEnemy)
        {
            playerMovement.TurnToClosestEnemy(angleRange, distance);
        }

        if (setDodging) playerMovement.isDodging = dodgingValue;
        if (setAttacking) playerMovement.isAttacking = attackingValue;
    }
}
