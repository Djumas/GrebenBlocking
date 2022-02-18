using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SimpleHit
{
    public float damage;
    public float range;
    public List<DamageEffectTypes> damageEffects;
    public float normalisedStartTime;
    public float normalisedStopTime;
    public float angleCenter;
    public float angleRange;
    public int ID;
}

public class SMBSimpleHit : StateMachineBehaviour
{
    bool active = false;
    bool attackStarted = false;
    public SimpleHit hit;

    


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackStarted = false;
        active = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
            /*
            Character closestEnemy = UnitManager.Instance.GetClosestEnemy(animator.gameObject.transform, hit.range, hit.angleRange);
            Debug.Log(closestEnemy);
            if (closestEnemy != null)
            {
                
                active = false;
                HealthManager enemyHealth = closestEnemy.gameObject.GetComponent<HealthManager>();
                enemyHealth.TakeDamage(hit.damage, hit.damageEffects);

            }*/
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
