using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;


public class Health : MonoBehaviour
{
    public float hp;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        anim.SetTrigger("isDamageHead");
        Debug.Log(hp);
    }

    public void ResetTrigger()
    {
        anim.SetBool("isAttacking", false);
    }

    public void ResetTrigger1()
    {
        anim.ResetTrigger("isDamageHead");
    }

    public void EndHit()
    {
        anim.SetInteger("HitType", 0);
    }
}
