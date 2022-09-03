using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SMBSimpleHit : StateMachineBehaviour
{
    private bool active = false;
    private bool attackStarted = false;
    public SimpleHitData hit;

    


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Attacking status:" + animator.GetBool("isAttacking"));
        attackStarted = false;
        active = false;
        animator.SetBool("isAttacking", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool("isAttacking",true);
        
        float currentTime = stateInfo.normalizedTime * 100;
        
        if (currentTime >= hit.normalisedStartTime && !attackStarted) {
            attackStarted = true;
            active = true;
        }

        if (currentTime >= hit.normalisedStopTime)
        {
            active = false;
        }

        if (active) {

            List<Character> enemiesInRange = UnitManager.Instance.GetEnemiesInRange(animator.gameObject.transform, hit.range, hit.angleRange);
            if (enemiesInRange != null)
            {
                active = false;
                foreach (Character enemy in enemiesInRange)
                {
                    HealthManager enemyHealth = enemy.gameObject.GetComponent<HealthManager>();
                    enemyHealth.TakeDamage(hit.damage, hit.damageEffects, animator.gameObject);
                }
            }
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Attacking status:" + animator.GetBool("isAttacking"));
        Debug.Log("Action:" + hit);
        Debug.Log("Time:" + stateInfo.normalizedTime);
        //animator.SetBool("isAttacking", false);  
    }


}
