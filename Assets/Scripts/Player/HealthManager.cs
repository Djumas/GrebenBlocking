using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageEffectTypes {
    GroinHit
}

public class HealthManager : MonoBehaviour
{
    public float baseHealth;
    public float currentHealth;
    public bool isDead = false;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = baseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount, List<DamageEffectTypes> damageEffects, GameObject origin)
    {
        if (isDead) {
            return;
        }

        if (damageEffects.Contains(DamageEffectTypes.GroinHit))
        {
            currentHealth -= amount;
            DeathCheck();
            TurnToTarget(origin);
            if (!isDead && !anim.GetBool("isShocked")) {
                anim.SetTrigger("isHitToGroin");
            }
            return;
        }

        currentHealth -= amount;
        DeathCheck();
        if (!isDead)
        {
            anim.SetTrigger("isHitFront");
        }

    }

    public void TakeDamage(float amount) {
        TakeDamage(amount, new List<DamageEffectTypes>(),null);
    }


    public void TurnToTarget(GameObject target)
    {
        Debug.Log("TurnToClosestEnemy");
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
        gameObject.GetComponent<Character>().unitStatus = UnitStatus.Dead;
    }
}
