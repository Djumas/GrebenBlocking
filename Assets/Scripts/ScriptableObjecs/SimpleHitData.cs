using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SimpleHitData", order = 1)]

public class SimpleHitData : ScriptableObject
{
    public float damage;
    public float range;
    public List<DamageEffectTypes> damageEffects;
    public float normalisedStartTime;
    public float normalisedStopTime;
    public float angleCenter;
    public float angleRange;
    public int ID;
}
