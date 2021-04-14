using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bully : StateMachineBehaviour
{
    public float bullTime;
    public float lostRange;
    private EnemyAI AI;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AI = animator.gameObject.GetComponent<EnemyAI>();
        Debug.Log(stateInfo.length);
        if (!AI.currentAim.bulled) { 
        animator.SetTrigger("Taunting");
        Debug.Log("StartTaunt");
        }
    }

     //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if ((stateInfo.length * stateInfo.normalizedTime < bullTime)&&!AI.currentAim.bulled)
        {
            //Debug.Log(stateInfo.length * stateInfo.normalizedTime);
            AI.pointAtAim();
        }
        else {
            AI.currentAim.bulled = true;
            Debug.Log("Chasing");
            animator.SetTrigger("NextAction");
            animator.SetTrigger("NextBehavior");
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
