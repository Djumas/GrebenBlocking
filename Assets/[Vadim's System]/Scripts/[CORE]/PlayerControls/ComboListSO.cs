using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboListSO", menuName = "ScriptableObjects/Combos/ComboListSO", order = 1)]
public class ComboListSO : ScriptableObject
{
    public List<ComboSO> combos;
}