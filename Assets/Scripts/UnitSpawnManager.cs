using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitSpawnBinding {
    public UnitType unitType;
    public GameObject prefab;
}

public class UnitSpawnManager : MonoSingleton<UnitSpawnManager>
{
    public UnitSpawnBinding[] unitSpawnBindings;

    public UnitSpawnPoint[] unitSpawnPoints;


    public GameObject getBinding(UnitType unitType) {
        foreach (UnitSpawnBinding spawnBinding in unitSpawnBindings) {
            if (spawnBinding.unitType == unitType) return spawnBinding.prefab;
        }
        Debug.LogError("No bindings found");
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        unitSpawnPoints = FindObjectsOfType<UnitSpawnPoint>();
        foreach (UnitSpawnPoint spawnPoint in unitSpawnPoints) {
            Instantiate(getBinding(spawnPoint.unitType),spawnPoint.transform.position, spawnPoint.transform.rotation);
            Destroy(spawnPoint.gameObject);
            Debug.Log(getBinding(spawnPoint.unitType)+"spawned");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
