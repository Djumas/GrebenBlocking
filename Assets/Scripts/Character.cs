using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitRoles
{
    Player,
    Enemy,
    Ally,
    Neutral,
    Boss
}

public enum UnitTypes
{
    Player,
    Fool,
    Bull,
    Artist
}

public enum UnitStatus
{
    Alive,
    Dead
}

public class Character : MonoBehaviour
{
    public UnitRoles unitRole;
    public UnitTypes unitType;
    public UnitStatus unitStatus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
