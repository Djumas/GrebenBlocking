using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class SMBAffectBehavior : StateMachineBehaviour
{
    public BehaviorTree behaviorTree;
    public string behaviorStateToSet = "None";
    public string animatorBoolToSet = "None";
    public bool animatorBoolToSetValue = true;
    public string behaviorBoolToSet = "None";
    public bool behaviorBoolToSetValue = true;
    public string animatorTriggerToSet = "None";
    public string behaviorTimeToSet = "None";
    public float timeAmount = 0;
    public float delayAmount = 0;
    public bool setCharacterStatus = false;
    public UnitStatus statusToSet = UnitStatus.Alive;
    public bool restartBehavior = false;
    private Character character;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character = animator.gameObject.GetComponent<Character>();
        behaviorTree = animator.gameObject.GetComponent<BehaviorTree>();

        if (character.unitStatus == UnitStatus.Dead && behaviorTree != null) {
            behaviorTree.enabled = false;
            return;
        }
            if (setCharacterStatus)
        {
            character.unitStatus = statusToSet;
        }
        //Debug.Log("AffectingBehavior");

        if (behaviorStateToSet != "None")
        {
            behaviorTree.SetVariableValue("CurrentStatus", behaviorStateToSet);
        }

        if (behaviorTimeToSet != "None")
        {
            behaviorTree.SetVariableValue(behaviorTimeToSet, timeAmount);
        }

        if (animatorBoolToSet != "None")
        {
            animator.SetBool(animatorBoolToSet, animatorBoolToSetValue);
        }
        if (behaviorBoolToSet != "None")
        {
            behaviorTree.SetVariableValue(behaviorBoolToSet, behaviorBoolToSetValue);
        }
        if (animatorTriggerToSet != "None")
        {
            animator.SetTrigger(animatorTriggerToSet);
        }

        if (restartBehavior) {
            behaviorTree.enabled = false;
            behaviorTree.enabled = true;
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
