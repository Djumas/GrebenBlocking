using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SMBSimpleHit : StateMachineBehaviour
{
    private bool attackActive = false;
    private bool attackStarted = false;
    private bool attackStopped = false;
    public bool damagePlayerOnly = false;
    private UnitAttackModifier attackModifier;
    public SimpleHitData hit;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Attacking status:" + animator.GetBool("isAttacking"));
        attackModifier = animator.gameObject.GetComponent<UnitAttackModifier>();
        if (attackModifier != null)
        {
            damagePlayerOnly = attackModifier.damagePlayerOnly;
        }
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
            List<Unit> enemiesInRange = UnitManager.Instance.GetEnemiesInRange(animator.gameObject.transform, hit.range, hit.angleRange);
            //Debug.Log(enemiesInRange);
            if (enemiesInRange != null)
            {
                foreach (Unit enemy in enemiesInRange)
                {
                    if (!damagePlayerOnly||(enemy.unitType == UnitType.Player)) {
                        attackActive = false;
                        HealthManager enemyHealth = enemy.gameObject.GetComponent<HealthManager>();
                        enemyHealth.TakeDamage(hit.damage, hit.damageEffects, animator.gameObject);
                    }
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
