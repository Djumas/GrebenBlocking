using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RootMotionNavMeshMovement : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public Animator anim;
    public Character character;
    public bool drawPath = true;
    private float drawPathTreshhold = 0.1f;
    private NavMeshPath path;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        navAgent.updatePosition = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (character.unitStatus == UnitStatus.Dead)
        {
            navAgent.isStopped = false;
            return;
        }

        if (character.unitStatus == UnitStatus.KnockOut)
        {
            navAgent.isStopped = true;
        }
        else {
            navAgent.isStopped = false;
        }


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

        if ((navAgent.destination-transform.position).magnitude > drawPathTreshhold && drawPath) {
            path = navAgent.path;
            for (int i = 0; i < path.corners.Length - 1; i++) {
                Debug.DrawLine(path.corners[i],path.corners[i+1],Color.red);
            }
        }

    }

    
}
