using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyObject", menuName = "Enemy Object", order = 52)]
public class EnemyScriptableObject : BaseScriptableObject
{
    #region SerializeField
    [Header("EnemyScriptableObject fileds")]
    [Header("Attack")]
    [SerializeField] private float m_attackDamage;
    [SerializeField] private float m_attackCooldown;
    [Header("Level system")]
    [SerializeField] private int m_experienceForKill;
    [Header("Health")]
    [SerializeField] private float m_maxHealth;
    [Header("Movement")]
    [SerializeField] private float m_movementSpeed;
    [Header("Sound")]
    [SerializeField] private AudioClip m_enemyHitSound;
    #endregion

    public float AttackDamage => m_attackDamage;
    public float AttackCooldown => m_attackCooldown;
    public int ExperienceForKill => m_experienceForKill;
    public float MaxHealth => m_maxHealth;
    public float MovementSpeed => m_movementSpeed;
    public AudioClip EnemyHitSound => m_enemyHitSound;
}
