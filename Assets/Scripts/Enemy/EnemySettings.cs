using UnityEngine;

namespace EnemyDefault
{

    [CreateAssetMenu(fileName = "New Enemy", menuName = "EnemyData", order = 51)]
    public class EnemySettings : ScriptableObject
    {

        [Header("Имя.")]
        [SerializeField]
        public string Name;


        [Header("Количество здоровья.")]
        [SerializeField]
        public int MaxHP;

        [Header("Весовая категория.")]
        [SerializeField]
        public WeightCategory m_WeightCategory;
        public enum WeightCategory { Light, Medium, Heavy }

        [Header("Скорость передвижения.")]
        [SerializeField]
        public float m_MoveSpeedCategory;

        [Header("Скорость атаки")]
        [SerializeField]
        public float AttackSpeed;

        [Header("Минимальное время между атаками.")]
        [SerializeField]
        public float MinAttackTime;

        [Header("Максимальное время между атаками.")]
        [SerializeField]
        public float MaxAttackTime;

        [Header("Порог самосохранения.")]
        [SerializeField]
        public int ScaredPercentHP;

    }
}