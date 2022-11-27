using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Hit
{
    public float Damage;
    public float Range;
    public List<DamageEffectTypes> damageEffects;
    public string attackPointID;
    public AttackPoint attackPoint;
    public float NormalisedStartTime;
    public float NormalisedStopTime;
    public int ID;
}


public class SMBEnemyMultiHit : StateMachineBehaviour // Currently deprecated
{
    //public Hit[] hitList;
    public SimpleHitData[] hits;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    /*
override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
{
    AttackPoint[] attackPoints = animator.gameObject.GetComponentsInChildren<AttackPoint>();
    for (int i = 0; i < hits.Length; i++)
    {
        hits[i].ID = Random.Range(0, 10000);
        foreach (AttackPoint attackPoint in attackPoints)
        {
            if (attackPoint.ID == hits[i].attackPointID)
            {
                hits[i].attackPoint = attackPoint;
                //Debug.Log(hits[i].attackPointID+" found");
                break;
            }
            Debug.Log(hits[i].attackPointID + " not found");
        }

    }
}*/

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks

        /*
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float currentTime = stateInfo.normalizedTime*100;
        for (int i = 0; i< hits.Length; i++)
        {
            if (currentTime >= hits[i].NormalisedStartTime) {
                hits[i].attackPoint.active = true;
                hits[i].attackPoint.currenthit = hits[i];
            }
            if (currentTime >= hits[i].NormalisedStopTime)
            {
                hits[i].attackPoint.active = false;
            }
        }
    }*/

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
