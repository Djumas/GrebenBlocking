using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageEffectTypes {
    GroinHit,
    ChestKick,
    StaggerHit, 
    HeadHit,
    RightHit,
    LeftHit
}

public class HealthManager : MonoBehaviour
{
    public float baseHealth;
    public float currentHealth;
    public bool isImmortal = false;
    public bool isDead = false;
    private Animator anim;
    private Unit unit;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        unit = GetComponent<Unit>();
        currentHealth = baseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount, List<DamageEffectTypes> damageEffects, GameObject origin)
    {
        Debug.Log("DamageTaken by "+gameObject+" from"+origin);
        
        if (isDead){
            return;
        }

        if (unit.unitStatus == UnitStatus.KnockOut){
            return;
        }

        if (!isImmortal) {
            currentHealth -= amount;
            DeathCheck();
        }

        if (damageEffects.Contains(DamageEffectTypes.GroinHit))
        {
         TurnToTarget(origin);
         anim.SetTrigger("isHitToGroin");
         return;
        }
        if (damageEffects.Contains(DamageEffectTypes.ChestKick))
        {
            TurnToTarget(origin);
            Debug.Log(gameObject+" KickedToChest");
            anim.SetTrigger("isKickedToChest");
            return;
        }
        if (damageEffects.Contains(DamageEffectTypes.StaggerHit))
        {
            TurnToTarget(origin);
            Debug.Log(gameObject + " StaggerHit");
            anim.SetTrigger("StaggerHitFront");
            return;
        }
        if (damageEffects.Contains(DamageEffectTypes.RightHit))
        {
            TurnToTarget(origin);
            Debug.Log(gameObject + " RightHit");
            anim.SetTrigger("HitRight");
            return;
        }
        if (damageEffects.Contains(DamageEffectTypes.LeftHit))
        {
            TurnToTarget(origin);
            Debug.Log(gameObject + " LeftHit");
            anim.SetTrigger("HitLeft");
            return;
        }
        if (damageEffects.Contains(DamageEffectTypes.HeadHit))
        {
            TurnToTarget(origin);
            Debug.Log(gameObject + " HeadHit");
            anim.SetTrigger("HitHead");
            return;
        }

        anim.SetTrigger("isHitFront");
    }

    public void TakeDamage(float amount) {
        TakeDamage(amount, new List<DamageEffectTypes>(),null);
    }


    public void TurnToTarget(GameObject target)
    {
        //Debug.Log("TurnToClosestEnemy");
        Vector3 targetToCharVector;

        if (target != null)
        {
            targetToCharVector = target.transform.position - transform.position;
            //transform.LookAt(closestEnemy.transform.position, Vector3.up);
            transform.forward = new Vector3(targetToCharVector.x, 0, targetToCharVector.z);
        }
    }

    public void DeathCheck() {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        anim.SetBool("IsDead", true);
        isDead = true;
        gameObject.GetComponent<Unit>().unitStatus = UnitStatus.Dead;
//        rigiBody.detectCollisions = false;
//        rigiBody.isKinematic = true;

    }
}
