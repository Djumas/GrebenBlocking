using UnityEngine;
using System.Linq;
using System.Collections.Generic;


public class Health : MonoBehaviour
{
    public float hp;
    private List<int> takenAttacks = new List<int>();
    public bool isDead = false;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage, int attackId)
    {

        if (!takenAttacks.Contains(attackId))
        {
            hp -= damage;
            anim.SetTrigger("isDamageHead");
            Debug.Log(hp);
            takenAttacks.Add(attackId);
        }
        else {
            Debug.Log("already attacked");
        }

        if (hp <= 0) {
            Die();
        }
    }

    public void Die() {
        Debug.Log("Health Dead");
        anim.SetBool("isDead", true);
        isDead = true;
    }

    /*public void ResetTrigger()
    {
        anim.SetBool("isAttacking", false);
    }

    public void ResetTrigger1()
    {
        anim.ResetTrigger("isDamageHead");
    }*/

    public void EndHit()
    {
        anim.SetInteger("HitType", 0);
    }
}
