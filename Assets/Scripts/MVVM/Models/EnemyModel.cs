using Assets.Scripts.CSharpClasses.Extensions;
using Assets.Scripts.CSharpClasses.Interfaces;
using Assets.Scripts.CSharpClasses.MonoBehaviourMethods;
using Assets.Scripts.CSharpClasses.RandomGenerator;
using Assets.Scripts.CSharpClasses.Reactive;
using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class EnemyModel : MonoBehaviour, ICharacter
{
    private Transform m_targetTransform;
    private Rigidbody2D m_rigidbody;
    private AudioSource m_audioSource;

    private Vector2 m_spawnPoint;
    private Vector2 m_moveDirection;

    private float m_attackCooldown;

    private bool m_followPalyer;
    private bool m_inBeforeDeathEvents = false;

    private EnemyScriptableObject m_enemyScriptableObject;

    private DiContainer m_diContainer;

    public event Action<EnemyModel> OnEnemyDeath;
    public ReactiveProperty<float> MaxHealth { get; set; } = new ReactiveProperty<float>();
    public ReactiveProperty<float> CurrentHealth { get; set; } = new ReactiveProperty<float>();
    public int ExperienceForKill => m_enemyScriptableObject.ExperienceForKill;
    private bool CanFollowForTarget => m_targetTransform.IsUnityNull() || m_inBeforeDeathEvents;


    #region Inject
    [Inject]
    public void Init(DiContainer diContainer)
    {
        m_diContainer = diContainer;
    }
    #endregion

    #region Unity Default Methods
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (CanFollowForTarget)
        {
            return;
        }

        Vector2 traget = m_followPalyer? m_targetTransform.position : m_spawnPoint;
        Vector2 direction = traget - (Vector2)transform.position;
        m_moveDirection = direction.normalized;
    }

    private void FixedUpdate()
    {
        if (CanFollowForTarget)
        {
            return;
        }

        if (!m_followPalyer && m_rigidbody.position.RoundEqual(m_spawnPoint))
        {
            return;
        }
        m_rigidbody.MovePosition(m_rigidbody.position.GetMovementVector(m_enemyScriptableObject.MovementSpeed, m_moveDirection));
    }
    #endregion

    #region Collider Methods
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BulletModel bullet))
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

    #region Init Methods
    public void InitEnemyObject(Transform targetTransform, EnemyScriptableObject enemyScriptableObject, Vector2 spawnPoint)
    {
        if (targetTransform.IsUnityNull() || enemyScriptableObject.IsUnityNull())
        {
            Destroy(gameObject);
        }

        m_spawnPoint = spawnPoint;
        m_followPalyer = true;
        m_targetTransform = targetTransform;
        m_enemyScriptableObject = enemyScriptableObject;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = m_enemyScriptableObject.Icon;

        InitHealtProperty();
        InitAudioSorce();
    }

    private void InitHealtProperty()
    {
        MaxHealth.Value = m_enemyScriptableObject.MaxHealth;
        CurrentHealth.Value = m_enemyScriptableObject.MaxHealth;
        CurrentHealth.Subscribe(x => HealthCheck(x));
    }

    private void InitAudioSorce()
    {
        if (m_audioSource.IsUnityNull())
        {
            m_audioSource = GetComponent<AudioSource>();
        }
        m_audioSource.clip = m_enemyScriptableObject.EnemyHitSound;
    }
    #endregion

    #region Enemy Controller Methods
    public void ChangeFollowPlayer(bool follow)
    {
        m_followPalyer = follow;
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
            DoBeforeDeath();
            OnEnemyDeath?.Invoke(this);
            Destroy(gameObject, m_audioSource.clip.length);
        }
    }

    private void DoBeforeDeath()
    {
        m_audioSource?.Play();
        m_inBeforeDeathEvents = true;

        if (m_enemyScriptableObject is EnemyWtihDropScriptableObject enemyWtihDropScriptableObject)
        {
            DropLoot(enemyWtihDropScriptableObject);
        }

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    private void DropLoot(EnemyWtihDropScriptableObject enemy)
    {
        if (TryInstantiateDrop(enemy.GunPrefab, m_rigidbody.position, out DroppedGunModel gunModel, out GameObject gameObject))
        {
            DropableGunScriptableObject gun = RandomGenerator.GetRandomGun(enemy.Drop);
            if (gun == default)
            {
                Destroy(gameObject);
            }
            gunModel.AfterInstantinate(gun);
        }
    }

    private bool TryInstantiateDrop(GameObject gameObject, Vector2 position, out DroppedGunModel gunModel, out GameObject newGameObject)
    {
        GameObject game = new();
        Transform newTransform = game.transform;
        newTransform.localPosition = transform.localPosition;

        return MonoBehaviourMethods.TryInstantiateComponent(m_diContainer, gameObject, position, newTransform, out gunModel, out newGameObject);
    }
    #endregion
}
