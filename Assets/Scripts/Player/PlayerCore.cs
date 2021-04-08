using UnityEngine;



public class PlayerCore : MonoBehaviour
{
    [SerializeField]
    public PlayerSettings defaultSettings;

    public GameObject attackPoint;

    public LayerMask enemyLayerMask;

    public Collider[] hitEnemies;

    private Health health;
    private Animator animator;

    public bool drawCircleGizmo = false;
    public float circleGizmoRadius = 2.0f;

    public void Update()
    {
        InputHandler();
    }

    private void Awake()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }

    public void Attack(float attackRange, float attackDamage)
    {
        hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayerMask);

        foreach (Collider enemy in hitEnemies)
            enemy.GetComponent<Health>().TakeDamage(attackDamage);
    }

    private void InputHandler() {
        if (Input.GetButtonDown("LightHit")){
            Debug.Log("LightHit");
            animator.SetTrigger("LightHit");

        }
        if (Input.GetButtonDown("HeavyHit"))
        {
            Debug.Log("HeavyHit");
            animator.SetTrigger("HeavyHit");
        }
        if (Input.GetButtonDown("Act"))
        {
            Debug.Log("Act");
        }
        if (Input.GetButtonDown("Parry"))
        {
            Debug.Log("Parry");
        }
    }

    private void OnDrawGizmos()
    {
        if (drawCircleGizmo) {
            Gizmos.DrawSphere(transform.position, circleGizmoRadius);
        }
    }


}

