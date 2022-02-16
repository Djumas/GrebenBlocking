using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageEffectTypes {
    GroinHit,
    ChestKick
}

public class HealthManager : MonoBehaviour
{
    public float baseHealth;
    public float currentHealth;
    public bool isDead = false;
    Animator anim;
    Character character;
    Rigidbody rigiBody;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        character = GetComponent<Character>();
        rigiBody = GetComponent<Rigidbody>();
        currentHealth = baseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount, List<DamageEffectTypes> damageEffects, GameObject origin)
    {
        if (isDead){
            return;
        }

        if (character.unitStatus == UnitStatus.KnockOut){
            return;
        }

        currentHealth -= amount;
        DeathCheck();

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
        gameObject.GetComponent<Character>().unitStatus = UnitStatus.Dead;
        rigiBody.detectCollisions = false;
        rigiBody.isKinematic = true;

    }
}
