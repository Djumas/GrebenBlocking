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

    public List<Character> GetEnemiesInRange(float searchDistance, float searchAngle) {
        List<Character> charactersInRange = new List<Character>();
        Vector3 charToPlayerVector;
        foreach (Character character in characters)
        {
            if (character.unitRole == UnitRoles.Enemy && character.unitStatus != UnitStatus.Dead)
            {
                charToPlayerVector = character.transform.position - player.transform.position;
                if (Vector3.Magnitude(charToPlayerVector) < searchDistance)
                {
                    
                    if (Vector3.Angle(charToPlayerVector,player.transform.forward) < searchAngle/2)
                    {
                        charactersInRange.Add(character);
                    }
                }
            }
        }

        if (charactersInRange.Count > 0)
        {
            return charactersInRange;
        }
        else { return null; }
    }

    public Character GetClosestEnemy(float searchDistance, float searchAngle) {
        float minDistance = searchDistance;
        List<Character> charactersInRange = GetEnemiesInRange(searchDistance, searchAngle);
        Character closestCharacter = null;
        if (charactersInRange != null)
        {
            foreach (Character character in charactersInRange)
            {
                if (character != player)
                {
                    if (Vector3.Magnitude(character.transform.position - player.transform.position) <= minDistance)
                    {
                        closestCharacter = character;
                    }
                }
            }
        }

        return closestCharacter;
    }
}
