using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedBehaviorRunner : MonoBehaviour
{
    public ScriptedBehavior currentBehavior;
    [System.Serializable]
    public struct NamedBehavior {
        public string name;
        public ScriptedBehavior behavior;
    }
    public NamedBehavior[] behaviors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AssignBehavior(string behavior) {
        foreach (NamedBehavior namedBehavior in behaviors) {
            if (namedBehavior.name == behavior) {
                currentBehavior = namedBehavior.behavior;
                return;
            }
        }
        Debug.Log("No such behavior");
    }
}
