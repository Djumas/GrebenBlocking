using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Check to see if the any objects are within sight of the agent.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}CanSeeObjectIcon.png")]

    public class CanSeeOjectSimple : Conditional
    {
        public SharedGameObject target;
        public SharedFloat viewAngle;
        public SharedFloat viewDistance;
        public SharedGameObject result;
        public Color gizmoColor = Color.yellow;


        public override void OnAwake()
        {
            base.OnAwake();
        }

        public override TaskStatus OnUpdate()
        {
            var direction = target.Value.transform.position - transform.position;
            var angle = Vector3.Angle(transform.forward, direction);
            if (direction.magnitude < viewDistance.Value)
            {
                if (angle < (viewAngle.Value / 2))
                {
                    result.Value = target.Value;
                    return TaskStatus.Success;
                }
            }
            return TaskStatus.Failure;
        }

        void DrawLineOfSight(Transform transform, Vector3 positionOffset, float fieldOfViewAngle, float angleOffset, float viewDistance, bool usePhysics2D)
        {
#if UNITY_EDITOR
            var oldColor = UnityEditor.Handles.color;
            var color = gizmoColor;
            color.a = 0.1f;
            UnityEditor.Handles.color = color;

            var halfFOV = fieldOfViewAngle * 0.5f + angleOffset;
            var beginDirection = Quaternion.AngleAxis(-halfFOV, (usePhysics2D ? transform.forward : transform.up)) * (usePhysics2D ? transform.up : transform.forward);
            UnityEditor.Handles.DrawSolidArc(transform.TransformPoint(positionOffset), (usePhysics2D ? transform.forward : transform.up), beginDirection, fieldOfViewAngle, viewDistance);

            UnityEditor.Handles.color = oldColor;
#endif
        }

        public override void OnDrawGizmos()
        {
            var oldColor = UnityEditor.Handles.color;
            UnityEditor.Handles.color = gizmoColor;
            DrawLineOfSight(Owner.transform, new Vector3(0,0,0), viewAngle.Value, 0, viewDistance.Value, false);
            UnityEditor.Handles.color = oldColor;
        }
    }
}
