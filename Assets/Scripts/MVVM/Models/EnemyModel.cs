using Assets.Scripts.CSharpClasses.Interfaces;
using Assets.Scripts.CSharpClasses.Reactive;
using UnityEngine;

public class EnemyModel : MonoBehaviour, ICharacter
{
    private EnemyScriptableObject m_enemyScriptableObject;
    private Transform m_targetTransform;
    private Rigidbody2D m_rigidbody;
    private Vector2 m_moveDirection;
    private float m_attackCooldown;
    private AudioSource m_audioSource;

    private bool m_inBeforeDeathEvents = false;

    public ReactiveProperty<float> CurrentHealth { get; set; } = new ReactiveProperty<float>();

    #region Unity Default Methods
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (m_targetTransform == null || m_inBeforeDeathEvents)
        {
            return;
        }
        Vector2 direction = m_targetTransform.position - transform.position;
        m_moveDirection = direction.normalized;
    }

    private void FixedUpdate()
    {
        if (m_targetTransform == null || m_inBeforeDeathEvents)
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
    public void InitEnemyObject(Transform targetTransform, EnemyScriptableObject enemyScriptableObject)
    {
        if (targetTransform == null || enemyScriptableObject == null)
        {
            Destroy(gameObject);
        }

        m_targetTransform = targetTransform;
        m_enemyScriptableObject = enemyScriptableObject;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = m_enemyScriptableObject.Icon;

        CurrentHealth.Value = m_enemyScriptableObject.MaxHealth;
        CurrentHealth.OnValueChanged += HealthCheck;

        if (m_audioSource == null)
        {
            m_audioSource = GetComponent<AudioSource>();
        }
        m_audioSource.clip = m_enemyScriptableObject.EnemyHitSound;
    }

    private void TakeDamage(float damage)
    {
        m_audioSource?.Play();
        CurrentHealth.Value -= damage;
    }

    private void HealthCheck(float value)
    {
        if (value <= 0)
        {
            DoBeforeDeathEvents();
            Destroy(gameObject, m_audioSource.clip.length);
        }
    }

    private void DoBeforeDeathEvents()
    {
        m_audioSource?.Play();
        m_inBeforeDeathEvents = true;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
    #endregion
}
