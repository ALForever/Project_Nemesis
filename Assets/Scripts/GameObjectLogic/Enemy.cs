using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyScriptableObject m_enemyScriptableObject;
    private Transform m_targetTransform;
    private float m_currentHealth;
    private Rigidbody2D m_rigidbody;
    private Vector2 m_moveDirection;
    private float m_attackCooldown = 0.01f;

    private UIController m_uIController;
    private AudioSource m_audioSource;

    private bool isInAfterDeathEvents = false;

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
        m_audioSource = GetComponent<AudioSource>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (m_targetTransform == null || isInAfterDeathEvents)
        {
            return;
        }
        Vector2 direction = m_targetTransform.position - transform.position;
        m_moveDirection = direction.normalized;
    }

    private void FixedUpdate()
    {
        if (m_targetTransform == null || isInAfterDeathEvents)
        {
            return;
        }
        m_rigidbody.MovePosition(m_rigidbody.position + m_enemyScriptableObject.MovementSpeed * Time.fixedDeltaTime * m_moveDirection);
    }
    #endregion

    #region Collider Methods
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            TakeDamage(bullet.BulletDamage);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerModel player))
        {
            if (m_attackCooldown <= 0)
            {
                player.CurrentHealth.Value -= m_enemyScriptableObject.AttackDamage;
                m_attackCooldown = m_enemyScriptableObject.AttackCooldown;
            }
            else
            {
                m_attackCooldown -= Time.deltaTime;
            }
        }
    }
    #endregion

    #region Enemy Controller Methods
    public void InitEnemyObject(Transform targetTransform, EnemyScriptableObject enemyScriptableObject, UIController uIController)
    {
        if (targetTransform == null || enemyScriptableObject == null)
        {
            DestroyEnemy();
        }

        m_uIController = uIController;
        m_targetTransform = targetTransform;
        m_enemyScriptableObject = enemyScriptableObject;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = m_enemyScriptableObject.Icon;

        CurrentHealth = m_enemyScriptableObject.MaxHealth;

        if (m_audioSource == null)
        {
            m_audioSource = GetComponent<AudioSource>();
        }
        m_audioSource.clip = m_enemyScriptableObject.EnemyHitSound;
    }

    private void TakeDamage(float damage)
    {
        m_audioSource?.Play();
        CurrentHealth -= damage;
    }

    private void DestroyEnemy()
    {
        m_uIController.KilledEnemyCounter++;
        DoAfterDeathEvents();
        Destroy(gameObject, m_audioSource.clip.length);
    }

    private void DoAfterDeathEvents()
    {
        m_audioSource?.Play();
        isInAfterDeathEvents = true;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
    #endregion
}
