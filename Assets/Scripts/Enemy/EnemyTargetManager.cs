using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetManager : MonoBehaviour
{
    public GameObject fightTarget;
    public GameObject fleePointTarget;

    // Start is called before the first frame update
    void Start()
    {
        fightTarget = UnitManager.Instance.player.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
