using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboSO", menuName = "ScriptableObjects/Combos/ComboSO", order = 1)]
public class ComboSO: ScriptableObject 
{
    public List<ComboElement> comboSequence;
    public InputResult finalResult;
    public InputResult flawResult;
}