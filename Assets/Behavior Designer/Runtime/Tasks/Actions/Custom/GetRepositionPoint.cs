using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("Get RetreatPoint")]
    [TaskCategory("Custom")]
    [TaskIcon("{SkinColor}ReflectionIcon.png")]
    public class GetRepositionPoint : Action
    {
        /*[Tooltip("The amount of time to wait")]
        public SharedFloat waitTime = 1;
        [Tooltip("Should the wait be randomized?")]
        public SharedBool randomWait = false;
        [Tooltip("The minimum wait time if random wait is enabled")]
        public SharedFloat randomWaitMin = 1;
        [Tooltip("The maximum wait time if random wait is enabled")]
        public SharedFloat randomWaitMax = 1;*/

        [Tooltip("The point to write value")]
        public SharedVector3 repositionPoint;
        public SharedFloat repositionMinAngle = 30;
        public SharedFloat repositionMaxAngle = 120;
        public SharedGameObject target;

        // The time to wait
        //private float waitDuration;
        // The time that the task started to wait.
        //private float startTime;
        // Remember the time that the task is paused so the time paused doesn't contribute to the wait time.
        //private float pauseTime;



        public override void OnStart()
        {
            float repositionAngle = Random.Range(repositionMinAngle.Value, repositionMaxAngle.Value);
            Vector3 vectorToTarget = target.Value.transform.position - transform.position;
            repositionPoint.Value = Quaternion.Euler(0, repositionAngle, 0) * (-vectorToTarget) + target.Value.transform.position;
        }

        public override TaskStatus OnUpdate()
        {
            if (repositionPoint.Value != null) {
                return TaskStatus.Success;
            } else {
                return TaskStatus.Failure;
            }
        }

        public override void OnPause(bool paused)
        {
            /*
            if (paused) {
                // Remember the time that the behavior was paused.
                pauseTime = Time.time;
            } else {
                // Add the difference between Time.time and pauseTime to figure out a new start time.
                startTime += (Time.time - pauseTime);
            }
            */
        }

        public override void OnReset()
        {
            
        }
    }
}