using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ComboConfig", order = 1)]
public class ComboConfig : ScriptableObject
{
    public List<Combo> combos;
}
