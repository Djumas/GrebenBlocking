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

        public override void OnDrawGizmos()
        {
            MovementUtility.DrawLineOfSight(Owner.transform, new Vector3(0,0,0), viewAngle.Value, 0, viewDistance.Value, false);
        }

        public override void OnBehaviorComplete()
        {
            MovementUtility.ClearCache();
        }

    }
}
