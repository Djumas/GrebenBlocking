using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observe : StateMachineBehaviour
{
    public float sightRange;
    public EnemyAI AI;
    public Transform transform;
    public string nextState;
    public Animator anim;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AI = animator.gameObject.GetComponent<EnemyAI>();
        transform = animator.gameObject.GetComponent<Transform>();
        Debug.Log(AI);
        anim = animator;
    }

    public void search() {
        float distance = (transform.position - AI.currentAim.aim.transform.position).magnitude;
        //Debug.Log(AI.currentAim.aim.transform.position);
        //Debug.Log(distance);
        if (distance <= sightRange) {
            Debug.Log("Found");
            anim.SetTrigger("FoundAim");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        search();
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
