using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;



public class FocusHandler : MonoBehaviour
{
    public List<Transform> enemiesInFocusRange;
    public float focusDistance = 10;
    public float focusAngle = 180;
    public float distanceToEnemy;
    public float angleToEnemy;
    public float rotSpeed = 5;
    private Quaternion rotQuat;

    public Camera mainCamera;

    [HideInInspector]
    public Transform nearestEnemy;

    public GameObject pointer;

    private GameObject[] enemies;

    

    private void Awake()
    {
        pointer.GetComponent<Renderer>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Focus"))
        {
            InitFocus();            
        }

        if (PlayerMovement.setFocus) {
           if(CheckEnemyStatus()){
                PerformFocus();
            }
        }

    }

    public void InitFocus() {
        Debug.Log("Focus pressed");
        FindEnemies();
        if (enemiesInFocusRange.Count > 0)
        {
            PlayerMovement.setFocus = !PlayerMovement.setFocus;
            pointer.GetComponent<Renderer>().enabled = PlayerMovement.setFocus;

        }
        else
        {
            Debug.Log("No enemies in range");
            PlayerMovement.setFocus = false;
            pointer.GetComponent<Renderer>().enabled = false;
        }
    }

    public void PerformFocus()
    {
        rotQuat.SetLookRotation((nearestEnemy.position - new Vector3(0,nearestEnemy.position.y)  - transform.position + new Vector3(0, transform.position.y)).normalized);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotQuat, Time.deltaTime * rotSpeed);
        pointer.transform.position = nearestEnemy.position + new Vector3(0, 2.5f, 0);
        pointer.transform.LookAt(mainCamera.transform);
        pointer.transform.Rotate(0, 180, 0);
    }


    public void FindEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesInFocusRange.Clear();
        foreach (GameObject enemy in enemies)
        {
            Transform enemyTransform = enemy.GetComponent<Transform>();

            distanceToEnemy = Vector3.Distance(enemyTransform.position, transform.position);
            angleToEnemy = (Vector3.Angle(enemy.transform.position - transform.position, transform.forward));

            if (distanceToEnemy < focusDistance && angleToEnemy >= -90 && angleToEnemy <= 90)
                enemiesInFocusRange.Add(enemy.transform);
        }
        nearestEnemy = FindNearestEnemy();
    }

    public bool CheckEnemyStatus() {
        if (nearestEnemy.tag != "Enemy") {
            Debug.Log("Enemy in focus doesn't exist");
            InitFocus();
            return false;
        } else {
            return true;
        }
    }

    private Transform FindNearestEnemy()
    {
        return enemiesInFocusRange.OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).FirstOrDefault();
    }
}
