using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyScriptableObject m_enemyScriptableObject;
    private Transform m_targetTransform;
    private float m_currentHealth;
    private Rigidbody2D m_rigidbody;
    private Vector2 m_moveDirection;

    public float CurrentHealth
    { 
        get => m_currentHealth;
        set
        {
            if (value <= 0)
            {
                DestroyEnemy();
            }
            m_currentHealth = value;
        }
    }

    #region Unity Default Methods
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 direction = m_targetTransform.position - transform.position;
        m_moveDirection = direction.normalized;
    }

    private void FixedUpdate()
    {
        m_rigidbody.MovePosition(m_rigidbody.position + m_enemyScriptableObject.MovementSpeed * Time.fixedDeltaTime * m_moveDirection);
    }
    #endregion

    #region Collider Methods
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BulletController bulletController))
        {
            TakeDamage(bulletController.BulletDamage);
        }
    }
    #endregion

    #region Enemy Controller Methods
    public void InitEnemyObject(Transform targetTransform, EnemyScriptableObject enemyScriptableObject)
    {
        if (targetTransform == null || enemyScriptableObject == null)
        {
            DestroyEnemy();
        }

        m_targetTransform = targetTransform;
        m_enemyScriptableObject = enemyScriptableObject;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = m_enemyScriptableObject.Icon;

        CurrentHealth = m_enemyScriptableObject.MaxHealth;
    }

    private void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    #endregion
}
