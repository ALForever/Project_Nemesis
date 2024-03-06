using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyObject", menuName = "Enemy Object", order = 52)]
public class EnemyScriptableObject : ScriptableObject
{
    #region SerializeField
    [Header("Standart fields")]
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private string m_name;
    [SerializeField] private string m_description;
    [SerializeField] private Sprite m_icon;

    [Header("EnemyScriptableObject fileds")]
    [SerializeField] private float m_attackDamage;
    [SerializeField] private float m_maxHealth;
    [SerializeField] private float m_movementSpeed;
    [SerializeField] private float m_attackCooldown;
    [SerializeField] private AudioClip m_enemyHitSound;
    #endregion

    public GameObject Prefab => m_prefab;
    public string Name => m_name;
    public string Description => m_description;
    public Sprite Icon => m_icon;
    public float AttackDamage => m_attackDamage;
    public float MaxHealth => m_maxHealth;
    public float MovementSpeed => m_movementSpeed;
    public float AttackCooldown => m_attackCooldown;
    public AudioClip EnemyHitSound => m_enemyHitSound;
}
