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

    public CinemachineClearShot clearShot;
    private CinemachineVirtualCamera currentCamera;

    [HideInInspector]
    public Transform nearestEnemy;

    public GameObject pointer;

    private GameObject[] enemies;

    

    private void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        pointer.GetComponent<Renderer>().enabled = false;
        currentCamera = (CinemachineVirtualCamera)clearShot.LiveChild;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Focus"))
        {
            Debug.Log("Focus pressed");
            FindEnemies();
            if (enemiesInFocusRange.Count > 0)
            {
                PlayerMovement.inFocus = !PlayerMovement.inFocus;
                pointer.GetComponent<Renderer>().enabled = PlayerMovement.inFocus;

            }
            else
            {
                Debug.Log("No enemies in range");
                PlayerMovement.inFocus = false;
                pointer.GetComponent<Renderer>().enabled = false;
            }
            
        }

        if (PlayerMovement.inFocus) {
            Focus();
        }

    }

    public void Focus()
    {
        nearestEnemy = FindNearestEnemy();
        //transform.LookAt(nearestEnemy);
        rotQuat.SetLookRotation((nearestEnemy.position - new Vector3(0,nearestEnemy.position.y)  - transform.position + new Vector3(0, transform.position.y)).normalized);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotQuat, Time.deltaTime * rotSpeed);
        pointer.transform.position = nearestEnemy.position + new Vector3(0, 2.5f, 0);
        pointer.transform.LookAt(currentCamera.transform);
    }


    public void FindEnemies()
    {
        enemiesInFocusRange.Clear();
        foreach (GameObject enemy in enemies)
        {
            Transform enemyTransform = enemy.GetComponent<Transform>();

            distanceToEnemy = Vector3.Distance(enemyTransform.position, transform.position);
            angleToEnemy = (Vector3.Angle(enemy.transform.position - transform.position, transform.forward));

            if (distanceToEnemy < focusDistance && angleToEnemy >= -90 && angleToEnemy <= 90)
                enemiesInFocusRange.Add(enemy.transform);
        }
    }

    private Transform FindNearestEnemy()
    {
        return enemiesInFocusRange.OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).FirstOrDefault();
    }
}
