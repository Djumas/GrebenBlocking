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
}
