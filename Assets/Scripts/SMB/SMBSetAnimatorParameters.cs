using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NamedBool
{
    public string name;
    public bool value;
}

public class SMBSetAnimatorParameters : StateMachineBehaviour
{
    public List<NamedBool> animatorBoolsToSet;
    public List<NamedBool> animatorTriggersToSet;
    


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (NamedBool item in animatorBoolsToSet) {
            animator.SetBool(item.name,item.value);
        }
        foreach (NamedBool item in animatorTriggersToSet)
        {
            animator.SetBool(item.name, item.value);
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
