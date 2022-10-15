using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoSingleton<UnitManager>
{
    public Unit[] units;
    public Unit player;

    // Start is called before the first frame update
    void Start()
    {
        units = FindObjectsOfType<Unit>();
        foreach (Unit unit in units) {
            if (unit.unitRole == UnitRoles.Player) {
                player = unit;
            }
        }

    }

    private void Awake()
    {
        
    }


    public GameObject GetCurrentTarget(GameObject unit) {
        return player.gameObject;
    }

    public List<Unit> GetEnemiesInRange(Transform origin, float searchDistance, float searchAngle) {
        List<Unit> unitsInRange = new List<Unit>();
        Vector3 unitToOriginVector;
        
        foreach (Unit unit in units)
        {
            
            if (unit.gameObject != origin.gameObject && unit.unitStatus != UnitStatus.Dead)
            {
                unitToOriginVector = unit.transform.position - origin.position;
                //Debug.Log(character +" "+Vector3.Magnitude(charToOriginVector));
                if (Vector3.Magnitude(unitToOriginVector) < searchDistance)
                {
                    
                    if (Vector3.Angle(unitToOriginVector, origin.forward) < searchAngle/2)
                    {
                        unitsInRange.Add(unit);
                    }
                }
            }
        }

        if (unitsInRange.Count > 0)
        {
            //Debug.Log(charactersInRange + " " + charactersInRange.Count);
            return unitsInRange;

        }
        else { return null; }
    }

    public Unit GetClosestEnemy(Transform origin, float searchDistance, float searchAngle) {
        float minDistance = searchDistance;
        float currentDistance;
        List<Unit> unitsInRange = GetEnemiesInRange(origin, searchDistance, searchAngle);
        Unit closestUnit = null;
        if (unitsInRange != null)
        {
            foreach (Unit unit in unitsInRange)
            {
                currentDistance = Vector3.Magnitude(unit.transform.position - origin.position);
               if (currentDistance <= minDistance)
                    {
                        minDistance = currentDistance;
                        closestUnit = unit;
                    }
    
            }
        }

        return closestUnit;
    }

    public Unit GetClosestEnemyByAngle(Transform origin, float searchDistance, float searchAngle)
    {
        float minAngle = 180;
        float currentAngle;
        List<Unit> unitsInRange = GetEnemiesInRange(origin, searchDistance, searchAngle);
        Unit closestUnit = null;
        if (unitsInRange != null)
        {
            
            foreach (Unit unit in unitsInRange)
            {
                var unitToOriginVector = unit.transform.position - origin.position;
                currentAngle = Vector3.Angle(unitToOriginVector, origin.forward);
                    if (currentAngle < minAngle)
                    {
                    minAngle = currentAngle;
                    closestUnit = unit;
                    }              

            }         
            

        }

        return closestUnit;
    }
}
