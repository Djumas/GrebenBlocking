using NodeCanvas.Framework;
using System.Collections.Generic;
using UnityEngine;
using EnemyDefault;


public class EnemyCore : MonoBehaviour
{
    [SerializeField]
    public EnemySettings enemySettings;
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    public List<GameObject> wayPoints;

    [SerializeField]
    public float minAttackTime;
    [SerializeField]
    public float maxAttackTime;

    public int attackDamage;
    public GameObject attackPointLeft;
    public GameObject attackPointRight;

    public LayerMask enemyLayerMask;

    public Collider[] hitEnemies;

    private Health health;
    private Animator anim;

    private void Awake()
    {
        minAttackTime = enemySettings.MinAttackTime;
        maxAttackTime = enemySettings.MaxAttackTime;
        health = GetComponent<Health>();
        health.hp = enemySettings.MaxHP;
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
    }

    public void Attack(float attackRange, float attackDamage, AttackListener.AttackPoints attackPoint, int attackId)
    {
        hitEnemies = Physics.OverlapSphere(attackPointLeft.transform.position, attackRange, enemyLayerMask);

        foreach (Collider enemy in hitEnemies)
            enemy.GetComponent<Health>().TakeDamage(attackDamage, attackId);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPointLeft.transform.position, 0.5f);
        Gizmos.DrawWireSphere(attackPointRight.transform.position, 0.5f);
    }
}

public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
