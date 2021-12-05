using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RootMotionNavMeshMovement : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        navAgent.updatePosition = false;
    }

    // Update is called once per frame
    void Update()
    {
        navAgent.nextPosition = transform.position;
        if ((navAgent.remainingDistance > navAgent.stoppingDistance)&&!navAgent.isStopped)
        {
            anim.SetBool("isMoving", true);
            //Debug.Log("RemainingDistance" + navAgent.remainingDistance);
        }
        else {
            anim.SetBool("isMoving", false);
            //Debug.Log("NavAgent Stopped");
            return;
        }
    }
}
