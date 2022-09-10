using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SMBSimpleHit : StateMachineBehaviour
{
    private bool attackActive = false;
    private bool attackStarted = false;
    private bool attackStopped = false;
    public SimpleHitData hit;

    


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Attacking status:" + animator.GetBool("isAttacking"));
        attackStarted = false;
        attackActive = false;
        animator.SetBool("isAttacking", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool("isAttacking",true);
        
        float currentTime = stateInfo.normalizedTime * 100;
        
        if (currentTime >= hit.normalisedStartTime && !attackStarted) {
            attackStarted = true;
            attackActive = true;
            //Debug.Log("Simple hit Activated:"+currentTime);
           
        }

        if (currentTime >= hit.normalisedStopTime && !attackStopped)
        {
            attackStopped = true;
            attackActive = false;
            //Debug.Log("Simple hit Deactivated:"+currentTime);
        }

        if (attackActive) {
            //Debug.Log("Simple hit Performing:"+currentTime);
            List<Character> enemiesInRange = UnitManager.Instance.GetEnemiesInRange(animator.gameObject.transform, hit.range, hit.angleRange);
            //Debug.Log(enemiesInRange);
            if (enemiesInRange != null)
            {
                attackActive = false;
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
        //Debug.Log("Attacking status:" + animator.GetBool("isAttacking"));
        //Debug.Log("Action:" + hit);
        //Debug.Log("Time:" + stateInfo.normalizedTime);
        //animator.SetBool("isAttacking", false);  
    }


}
