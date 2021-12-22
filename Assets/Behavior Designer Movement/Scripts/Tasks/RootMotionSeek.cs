using UnityEngine;
using UnityEngine.AI;
namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Seek the target specified using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}SeekIcon.png")]
    public class RootMotionSeek : Action
    {
        [Tooltip("The GameObject that the agent is seeking")]
        public SharedGameObject target;
        [Tooltip("If target is null then use the target position")]
        public SharedVector3 targetPosition;
        public NavMeshAgent navAgent;
        public SharedFloat stoppingDistance;
        public Color gizmoColor = Color.yellow;


        public override void OnStart()
        {
            base.OnStart();
            navAgent = GetComponent<NavMeshAgent>();
            navAgent.isStopped = false;
        }

        // Seek the destination. Return success once the agent has reached the destination.
        // Return running if the agent hasn't reached the destination yet
        public override TaskStatus OnUpdate()
        {
            MoveToTarget();
            //Debug.Log(navAgent.remainingDistance);
            // Debug.Log(navAgent.transform.position);
            // Debug.Log(target.Value.transform.position);
            //Debug.Log("Remaining:"+RemainingDistance());
            //Debug.Log("RemainingDistance" + navAgent.remainingDistance);
            if (RemainingDistance() < stoppingDistance.Value) {
                //Debug.Log(RemainingDistance());
                //Debug.Log("Arrived");
                navAgent.SetDestination(navAgent.transform.position);
                navAgent.isStopped = true;
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }

        public void MoveToTarget() {            
            navAgent.SetDestination(Target());
        }

        // Return targetPosition if target is null
        private Vector3 Target()
        {
            if (target.Value != null) {
                return target.Value.transform.position;
            }
            return targetPosition.Value;
        }

        public override void OnReset()
        {
            base.OnReset();
            target = null;
            targetPosition = Vector3.zero;
        }

        public float RemainingDistance() {
            return (transform.position - navAgent.destination).magnitude;
        }

        public override void OnDrawGizmos()
        {
#if UNITY_EDITOR
            if (Owner == null || stoppingDistance.Value == 0)
            {
                return;
            }
            var oldColor = UnityEditor.Handles.color;
            UnityEditor.Handles.color = gizmoColor;
            UnityEditor.Handles.DrawWireDisc(Owner.transform.position, Owner.transform.up, stoppingDistance.Value);
            UnityEditor.Handles.color = oldColor;
#endif
        }


    }
}