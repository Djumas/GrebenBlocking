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
    [Header("OnStart")]
    public List<NamedBool> animatorBoolsToSet;
    public List<NamedBool> animatorTriggersToSet;
    [Space(10)]
    
    [Header ("OnExit")]
    public List<NamedBool> animatorExitBoolsToSet;
    public List<NamedBool> animatorExitTriggersToSet;


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

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (NamedBool item in animatorExitBoolsToSet) {
            animator.SetBool(item.name,item.value);
        }
        foreach (NamedBool item in animatorExitTriggersToSet)
        {
            animator.SetBool(item.name, item.value);
        }
    }
}
