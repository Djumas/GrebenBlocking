using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    public Hit currenthit;
    public bool active = false;
    public string ID;
    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        HealthManager enemyHealth;
        if (active) {
            //Debug.Log(ID + " Collided " + other);
            enemyHealth = other.GetComponent<HealthManager>();
            if (enemyHealth != null && !enemyHealth.isDead) {
                enemyHealth.TakeDamage(currenthit.Damage,currenthit.damageEffects);
                active = false;
            }
        }
    }
}
