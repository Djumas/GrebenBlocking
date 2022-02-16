﻿using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("Get FleePoint")]
    [TaskCategory("Custom")]
    [TaskIcon("{SkinColor}ReflectionIcon.png")]
    public class GetCurrentHP : Action
    {
        /*[Tooltip("The amount of time to wait")]
        public SharedFloat waitTime = 1;
        [Tooltip("Should the wait be randomized?")]
        public SharedBool randomWait = false;
        [Tooltip("The minimum wait time if random wait is enabled")]
        public SharedFloat randomWaitMin = 1;
        [Tooltip("The maximum wait time if random wait is enabled")]
        public SharedFloat randomWaitMax = 1;*/

        public SharedFloat currentHP = Mathf.NegativeInfinity;


        // The time to wait
        //private float waitDuration;
        // The time that the task started to wait.
        //private float startTime;
        // Remember the time that the task is paused so the time paused doesn't contribute to the wait time.
        //private float pauseTime;



        public override void OnStart()
        {
            // Remember the start time.
            /*startTime = Time.time;
            if (randomWait.Value) {
                waitDuration = Random.Range(randomWaitMin.Value, randomWaitMax.Value);
            } else {
                waitDuration = waitTime.Value;
            }*/

            currentHP.Value = gameObject.GetComponent<HealthManager>().currentHealth;

        }

        public override TaskStatus OnUpdate()
        {
            // The task is done waiting if the time waitDuration has elapsed since the task was started.
            /*if (startTime + waitDuration < Time.time) {
                return TaskStatus.Success;
            }*/
            // Otherwise we are still waiting.
            /*return TaskStatus.Running;*/
            if (currentHP.Value != Mathf.NegativeInfinity) {
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