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
        private NavMeshAgent navAgent;
        private RootMotionNavMeshMovement rootMotionNavMeshMovement;
        public SharedFloat stoppingDistance;
        public Color gizmoColor = Color.yellow;
        public bool useResample = false;
        public SharedFloat resampleDistance = 2.0f;
        //public bool updateRotation = true;
        //private bool storedUpdateRotation;
        public bool useBlendTree = false;
        private bool storedUseBlendTree;


        public override void OnStart()
        {
            //base.OnStart();
            navAgent = GetComponent<NavMeshAgent>();
            rootMotionNavMeshMovement = GetComponent<RootMotionNavMeshMovement>();
            navAgent.isStopped = false;
            navAgent.stoppingDistance = stoppingDistance.Value;
            //storedUpdateRotation = navAgent.updateRotation;
            //navAgent.updateRotation = updateRotation;
            storedUseBlendTree = rootMotionNavMeshMovement.useBlendTree;
            rootMotionNavMeshMovement.useBlendTree = useBlendTree;
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
                //navAgent.SetDestination(navAgent.transform.position);
                navAgent.isStopped = true;
                //navAgent.updateRotation = storedUpdateRotation;
                rootMotionNavMeshMovement.useBlendTree = storedUseBlendTree;
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
            Vector3 result;
            if (target.Value != null) {
                result = target.Value.transform.position;
            } else result = targetPosition.Value;

            if (useResample)
            {
                NavMeshHit hit;
                NavMesh.SamplePosition(result, out hit, resampleDistance.Value, NavMesh.AllAreas);
                result = hit.position;
            }
            return result;
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