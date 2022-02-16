using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoSingleton<UnitManager>
{
    public Character[] characters;
    public Character player;

    // Start is called before the first frame update
    void Start()
    {
        characters = FindObjectsOfType<Character>();
        foreach (Character character in characters) {
            if (character.unitRole == UnitRoles.Player) {
                player = character;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetCurrentTarget(GameObject unit) {
        return player.gameObject;
    }

    public List<Character> GetEnemiesInRange(Transform origin, float searchDistance, float searchAngle) {
        List<Character> charactersInRange = new List<Character>();
        Vector3 charToOriginVector;
        
        foreach (Character character in characters)
        {
            
            if (character.gameObject != origin.gameObject && character.unitStatus != UnitStatus.Dead)
            {
                charToOriginVector = character.transform.position - origin.position;
                //Debug.Log(character +" "+Vector3.Magnitude(charToOriginVector));
                if (Vector3.Magnitude(charToOriginVector) < searchDistance)
                {
                    
                    if (Vector3.Angle(charToOriginVector, origin.forward) < searchAngle/2)
                    {
                        charactersInRange.Add(character);
                    }
                }
            }
        }

        if (charactersInRange.Count > 0)
        {
            //Debug.Log(charactersInRange + " " + charactersInRange.Count);
            return charactersInRange;

        }
        else { return null; }
    }

    public Character GetClosestEnemy(Transform origin, float searchDistance, float searchAngle) {
        float minDistance = searchDistance;
        float currentDistance;
        List<Character> charactersInRange = GetEnemiesInRange(origin, searchDistance, searchAngle);
        Character closestCharacter = null;
        if (charactersInRange != null)
        {
            foreach (Character character in charactersInRange)
            {
                currentDistance = Vector3.Magnitude(character.transform.position - origin.position);
               if (currentDistance <= minDistance)
                    {
                        minDistance = currentDistance;
                        closestCharacter = character;
                    }
    
            }
        }

        return closestCharacter;
    }

    public Character GetClosestEnemyByAngle(Transform origin, float searchDistance, float searchAngle)
    {
        float minAngle = 180;
        float currentAngle;
        List<Character> charactersInRange = GetEnemiesInRange(origin, searchDistance, searchAngle);
        Character closestCharacter = null;
        if (charactersInRange != null)
        {
            
            foreach (Character character in charactersInRange)
            {
                var charToOriginVector = character.transform.position - origin.position;
                currentAngle = Vector3.Angle(charToOriginVector, origin.forward);
                    if (currentAngle < minAngle)
                    {
                    minAngle = currentAngle;
                    closestCharacter = character;
                    }              

            }         
            

        }

        return closestCharacter;
    }
}
