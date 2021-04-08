using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "PlayerData", order = 52)]
public class PlayerSettings : ScriptableObject
{

    [Header("Количество здоровья.")]
    [SerializeField]
    public int MaxHP;

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