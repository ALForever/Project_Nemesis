using Assets.Scripts.CSharpClasses.Extensions;
using Assets.Scripts.CSharpClasses.MonoBehaviourMethods;
using Assets.Scripts.CSharpClasses.Reactive;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class EnemySpawnerArea : MonoBehaviour
{
    #region SerializeField
    [Header("GameObjects")]
    [SerializeField] private GameObject m_target;
    [SerializeField] private GameObject m_targetUI;
    [SerializeField] private GameObject m_enemyGameObject;
    [Header("Spawn area settings")]
    [SerializeField] private float m_areaRadius;
    [SerializeField] private float m_spawnTimeOut = 0.001f;
    [SerializeField] private int m_spawnLimit = 10;
    [Header("Enemy list")]
    [SerializeField] private List<EnemyScriptableObject> m_enemyScriptableObjects;
    #endregion

    #region Fileds form Inject
    private LevelSystem m_levelSystem;
    private DiContainer m_diContainer;
    #endregion

    private float m_currentSpawnTimeOut;
    private int m_currentSpawnUnitsCount;

    private readonly List<EnemyModel> m_enemyModels = new();
    private readonly ReactiveProperty<bool> m_playerInArea = new();

    public float AreaRadius => m_areaRadius;
    private bool CanSpawnEnemy => m_target != null && !m_enemyScriptableObjects.IsNullOrEmptyCollection() && m_playerInArea.Value;

    #region Inject
    [Inject]
    public void Init(LevelSystem levelSystem, DiContainer diContainer)
    {
        m_levelSystem = levelSystem;
        m_diContainer = diContainer;
    }
    #endregion

    #region Unity Default Methods
    void Awake()
    {
        InitCollider2D();
        m_playerInArea.Subscribe(x => PlayerInAreaValueChanged(x));
    }

    void Update()
    {
        if (!CanSpawnEnemy)
        {
            return;
        }
        SpawnEnemy();
    }
    #endregion

    #region Collider Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerModel>(out _))
        {
            m_playerInArea.Value = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerModel>(out _))
        {
            m_playerInArea.Value = false;
            if (m_currentSpawnUnitsCount == m_spawnLimit)
            {
                m_currentSpawnUnitsCount = 0;
            }
        }
    }
    #endregion

    #region Enemy Spawner Area Methods
    private void InitCollider2D()
    {
        if (!this.TryGetComponent<CircleCollider2D>(out _))
        {
            gameObject.AddComponent<CircleCollider2D>();
        }
        CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.radius = m_areaRadius;
        circleCollider2D.isTrigger = true;
    }

    private void SpawnEnemy()
    {
        if (m_currentSpawnUnitsCount >= m_spawnLimit)
        {
            return;
        }

        if (m_currentSpawnTimeOut <= 0)
        {
            Vector2 position = Random.insideUnitCircle * m_areaRadius + (Vector2)transform.position;
            
            if (TryInstantiateEnemy(position, out EnemyModel enemy, out GameObject enemyGameObject))
            {
                enemy.InitEnemyObject(m_target.transform, m_enemyScriptableObjects.GetRandomElement(), position);
                enemy.OnEnemyDeath += EnemyDeathHandler;

                m_enemyModels.Add(enemy);

                if (TryInstantiateHealthBar(position, enemyGameObject, out CharacterHealthViewModel characterHealthViewModel))
                {
                    characterHealthViewModel.SetCharacter(enemyGameObject);
                }

                m_currentSpawnTimeOut = m_spawnTimeOut;
                m_currentSpawnUnitsCount++;
            }
        }
        else
        {
            m_currentSpawnTimeOut -= Time.deltaTime;
        }
    }

    private void EnemyDeathHandler(EnemyModel enemyModel)
    {
        m_levelSystem.Experience.Value += enemyModel.ExperienceForKill;
        enemyModel.OnEnemyDeath -= EnemyDeathHandler;
        m_enemyModels.Remove(enemyModel);
    }

    private void PlayerInAreaValueChanged(bool value)
    {
        foreach (EnemyModel enemyModel in m_enemyModels)
        {
            enemyModel.ChangeFollowPlayer(value);
        }
    }
    #endregion

    #region TryInstantiate
    private bool TryInstantiateEnemy(Vector2 position, out EnemyModel enemyModel, out GameObject enemyGameObject)
    {
        return MonoBehaviourMethods.TryInstantiateComponent(m_diContainer, m_enemyGameObject, position, transform, out enemyModel, out enemyGameObject);
    }

    private bool TryInstantiateHealthBar(Vector2 position, GameObject gameObject, out CharacterHealthViewModel characterHealthViewModel)
    {
        // Сделал чтобы можно было отключать спавн UI для врагов
        if (m_targetUI.IsUnityNull())
        {
            characterHealthViewModel = null;
            return false;
        }

        if (!MonoBehaviourMethods.TryInstantiateComponent(gameObject, position, out characterHealthViewModel, out GameObject healtbarGameObject))
        {
            return false;
        }

        healtbarGameObject.transform.position = position;
        healtbarGameObject.transform.SetParent(gameObject.transform, true);
        return true;
    }
    #endregion
}