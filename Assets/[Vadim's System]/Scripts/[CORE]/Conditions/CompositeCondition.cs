using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeCondition : MonoBehaviour
{
    List<Func<bool>> _conditions = new List<Func<bool>>();

    public void AddCondition(Func<bool> condition) {
        _conditions.Add(condition);
    }

    public bool Evaluate() {
        foreach (var condition in _conditions) {
            if (!condition.Invoke()) {
                return false;
            }            
        }
        return true;
    }
}
