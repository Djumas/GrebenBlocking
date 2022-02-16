using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TestScriptableObject", order = 1)]

public class SOTest : ScriptableObject
{
    public bool boolParameter;
    public int intParameter;
    [RangeAttribute(0, 90)]
    public float floatParameter;
}
