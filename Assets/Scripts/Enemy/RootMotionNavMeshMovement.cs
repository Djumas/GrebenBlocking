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
    private const float DrawPathTreshhold = 0.1f;
    //private NavMeshPath path;
    public bool useBlendTree = false;

    // Start is called before the first frame update
    private void Start()
    {
        character = GetComponent<Character>();
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        navAgent.updatePosition = false;
    }

    // Update is called once per frame
    private void Update()
    {
        var forwardDir = transform.forward;
        var rightDir = transform.right;
        var position = transform.position;
        
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

        anim.SetBool("UseBlendTree", useBlendTree);
        if (useBlendTree)
        {
            navAgent.updateRotation = false;
        }

        navAgent.nextPosition = position;
        if ((navAgent.remainingDistance > navAgent.stoppingDistance)&&!navAgent.isStopped)
        {
            anim.SetBool("isMoving", true);
            var normalisedDirection = navAgent.desiredVelocity.normalized;
            var yVector = Vector3.Project(normalisedDirection, forwardDir);
            var xVector = Vector3.Project(normalisedDirection, rightDir);
            var yDirectiron = yVector.magnitude * Vector3.Dot(yVector, forwardDir);
            var xDirectiron = xVector.magnitude * Vector3.Dot(xVector, rightDir);

            anim.SetFloat("XDirection", xDirectiron);
            anim.SetFloat("YDirection", yDirectiron);

            //Debug.Log("RemainingDistance" + navAgent.remainingDistance);
        }
        else {
            anim.SetBool("isMoving", false);
            //Debug.Log("NavAgent Stopped");
            return;
        }

        if ((navAgent.destination-position).magnitude > DrawPathTreshhold && drawPath) {
            /*
            var path = navAgent.path;
            for (int i = 0; i < path.corners.Length - 1; i++) {
                Debug.DrawLine(path.corners[i],path.corners[i+1],Color.red);
            }
            */
            Debug.DrawLine(position,position+navAgent.desiredVelocity);
        }

    }

    
}
