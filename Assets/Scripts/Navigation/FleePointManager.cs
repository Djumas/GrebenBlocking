using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleePointManager : MonoSingleton<FleePointManager>
{
    public FleePoint[] fleePoints;
    // Start is called before the first frame update
    void Start()
    {
        fleePoints = FindObjectsOfType<FleePoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetFleePoint(GameObject target)
    {
        if (fleePoints.Length > 0) return fleePoints[Random.Range(0,fleePoints.Length)].gameObject;
        else
        {
            Debug.Log("No FleePoints available");
            return null;
        }
    }
}
