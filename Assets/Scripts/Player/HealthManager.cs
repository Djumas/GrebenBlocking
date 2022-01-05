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

    public void TakeDamage(float amount, List<DamageEffectTypes> damageEffects)
    {
        if (isDead) {
            return;
        }

        if (damageEffects.Contains(DamageEffectTypes.GroinHit))
        {
            currentHealth -= amount;
            DeathCheck();
            if (!isDead) {
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
        TakeDamage(amount, new List<DamageEffectTypes>());
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
