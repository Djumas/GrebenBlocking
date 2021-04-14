using UnityEngine;


public class PlayerCore : MonoBehaviour
{
    [SerializeField]
    public PlayerSettings defaultSettings;

    public GameObject attackPointLeft;
    public GameObject attackPointRight;


    public LayerMask enemyLayerMask;

    public Collider[] hitEnemies;

    private Health health;
    private Animator animator;

    public bool drawCircleGizmo = false;
    private bool drawLeftGizmo = false;
    private bool drawRightGizmo = false;
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

    public void Attack(float attackRange, float attackDamage, AttackListener.AttackPoints attackPoint, int attackId)
    {
        drawCircleGizmo = true;
        drawLeftGizmo = false;
        drawRightGizmo = false;
        circleGizmoRadius = attackRange;
        switch (attackPoint) {
            case AttackListener.AttackPoints.Left:
                hitEnemies = Physics.OverlapSphere(attackPointLeft.transform.position, attackRange, enemyLayerMask);
                drawLeftGizmo = true;
                break;
            case AttackListener.AttackPoints.Right:
                hitEnemies = Physics.OverlapSphere(attackPointRight.transform.position, attackRange, enemyLayerMask);
                drawRightGizmo = true;
                break;
        }



        foreach (Collider enemy in hitEnemies)
            enemy.GetComponent<Health>().TakeDamage(attackDamage, attackId);
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

    private void OnDrawGizmos() {
        if (drawCircleGizmo) {
            if (drawLeftGizmo)
            {
                Gizmos.DrawWireSphere(attackPointLeft.transform.position, circleGizmoRadius);
            }
            if (drawRightGizmo)
            {
                Gizmos.DrawWireSphere(attackPointRight.transform.position, circleGizmoRadius);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPointLeft.transform.position, 0.5f);
        Gizmos.DrawWireSphere(attackPointRight.transform.position, 0.5f);
    }


}

