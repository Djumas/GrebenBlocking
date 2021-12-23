using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth > 0)
        {
            anim.SetTrigger("IsHitFront");
        }
        else
        {
            anim.SetBool("IsDead",true);
            isDead = true;
        }
    }
}
