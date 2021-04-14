using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Serializable]
    public struct EnemyReputation
    {
        public int hate;
        public int fear;
        public GameObject aim;
        public bool bulled;
    }

    public float fleeTresh = 30f;
    public EnemyReputation currentAim;
    public List<EnemyReputation> relations;
    public EnemyCore core;
    public NavMeshAgent navMeshAgent;

    public void DefineEnemy() {
        currentAim = relations[0];
        foreach (EnemyReputation enemy in relations)
        {
            if (enemy.hate > currentAim.hate)
            {
                currentAim = enemy;
            }
        }
    }

    public void pointAtAim() {
        transform.LookAt(currentAim.aim.transform.position-(new Vector3(0, currentAim.aim.transform.position.y)));
    }

    public void chaseAim() {
        navMeshAgent.SetDestination(currentAim.aim.transform.position);
    }

    public void stopChasing() {
        navMeshAgent.SetDestination(navMeshAgent.transform.position);
    }
    // Start is called before the first frame update
    void Start()
    {
        DefineEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



