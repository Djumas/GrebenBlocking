using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class SMBAffectBehavior : StateMachineBehaviour
{
    public BehaviorTree behaviorTree;
    public string behaviorStateToSet = "";
    public string animatorBoolToSet = "";
    public bool animatorBoolToSetValue = true;

    public string animatorBoolToSetOnExit = "";
    public bool animatorBoolToSetOnExitValue = true;

    public string animatorBoolToSetOnTime = "";
    public bool animatorBoolToSetOnTimeValue = true;
    public float timeAmountToSetBool = 0;
    public float BoolSetTimeLeft = 0;

    public string behaviorBoolToSet = "";
    public bool behaviorBoolToSetValue = true;
    public string animatorTriggerToSet = "";
    public string behaviorTimeToSet = "";
    public float timeAmount = 0;
    public float delayAmount = 0;
    public bool setCharacterStatus = false;
    public UnitStatus statusToSet = UnitStatus.Alive;
    public bool restartBehavior = false;
    private Unit character;
    public bool disableBehavior = false;
    public bool enableBehavior = false;
    public float timeAmountToEnable = 0;
    public float EnableTimeLeft = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character = animator.gameObject.GetComponent<Unit>();
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

        if (behaviorStateToSet != "")
        {
            behaviorTree.SetVariableValue("CurrentStatus", behaviorStateToSet);
        }

        if (behaviorTimeToSet != "")
        {
            behaviorTree.SetVariableValue(behaviorTimeToSet, timeAmount);
        }

        if (animatorBoolToSet != "")
        {
            animator.SetBool(animatorBoolToSet, animatorBoolToSetValue);
        }
        if (behaviorBoolToSet != "")
        {
            behaviorTree.SetVariableValue(behaviorBoolToSet, behaviorBoolToSetValue);
        }
        if (animatorTriggerToSet != "")
        {
            animator.SetTrigger(animatorTriggerToSet);
        }

        if (restartBehavior) {
            behaviorTree.enabled = false;
            behaviorTree.enabled = true;
        }

        if (disableBehavior) {
            behaviorTree.enabled = false;
        }

        if (enableBehavior) {
            EnableTimeLeft = timeAmountToEnable;
        }

        if (animatorBoolToSetOnTime != "")
        {
            BoolSetTimeLeft = timeAmountToSetBool;
        }
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enableBehavior && !behaviorTree.enabled) {
            if (EnableTimeLeft > 0)
            {
                EnableTimeLeft -= Time.deltaTime;
            }
            else {
                behaviorTree.enabled = true;
            }
        }

        if ((animatorBoolToSetOnTime != "") && (animator.GetBool(animatorBoolToSetOnTime) != animatorBoolToSetOnTimeValue))
        {
            if (BoolSetTimeLeft > 0)
            {
                BoolSetTimeLeft -= Time.deltaTime;
            }
            else
            {
                animator.SetBool(animatorBoolToSetOnTime, animatorBoolToSetOnTimeValue);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animatorBoolToSetOnExit != "")
        {
            animator.SetBool(animatorBoolToSetOnExit, animatorBoolToSetOnExitValue);
        }
    }

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
