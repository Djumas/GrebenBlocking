using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SimpleHitData", order = 1)]

public class SimpleHitData : ScriptableObject
{
    public float damage;
    public float range;
    public List<DamageEffectTypes> damageEffects;
    [Range(0,100)]
    public float normalisedStartTime;
    [Range(0,100)]
    public float normalisedStopTime;
    public float angleCenter;
    public float angleRange;
    public int ID;
}
