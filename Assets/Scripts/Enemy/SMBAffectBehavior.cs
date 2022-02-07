using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class SMBAffectBehavior : StateMachineBehaviour
{
    public BehaviorTree behaviorTree;
    public string behaviorStateToSet = "None";
    public string animatorBoolToSet = "None";
    public string behaviorBoolToSet = "None";
    public string animatorTriggerToSet = "None";
    public float timeAmount = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("AffectingBehavior");
        behaviorTree = animator.gameObject.GetComponent<BehaviorTree>();
        if (behaviorStateToSet == "Shocked") {
            behaviorTree.SetVariableValue("ShockTime", timeAmount);
            behaviorTree.SetVariableValue("CurrentStatus", "Shocked");
        }
        if (animatorBoolToSet != "None")
        {
            animator.SetBool(animatorBoolToSet, true);
        }
        if (behaviorBoolToSet != "None")
        {
            behaviorTree.SetVariableValue(behaviorBoolToSet, true);
        }
        if (animatorTriggerToSet != "None")
        {
            animator.SetTrigger(animatorTriggerToSet);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
