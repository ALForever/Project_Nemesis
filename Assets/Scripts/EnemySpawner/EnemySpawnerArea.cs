using Assets.Scripts.Extensions;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawnerArea : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private List<EnemyScriptableObject> m_enemyScriptableObjects;
    [SerializeField] private GameObject m_target;
    [SerializeField] private float m_areaRadius;
    [SerializeField] private GameObject m_enemyGameObject;
    [SerializeField] private float m_spawnTimeOut = 0.001f;
    [SerializeField] private int m_spawnLimit = 10;
    #endregion

    private float m_currentSpawnTimeOut;
    private int m_currentSpawnUnitsCount;

    public float AreaRadius => m_areaRadius;

    private bool CanSpawnEnemy => m_target != null && !m_enemyScriptableObjects.IsNullOrEmptyList();

    #region Unity Default Methods
    void Update()
    {
        if (!CanSpawnEnemy)
        {
            return;
        }
        SpawnEnemy();
    }
    #endregion

    #region Enemy Spawner Area Methods
    private void SpawnEnemy()
    {
        if (m_currentSpawnUnitsCount >= m_spawnLimit)
        {
            return;
        }

        if (m_currentSpawnTimeOut <= 0)
        {
            GameObject enemyGameObject = Instantiate(m_enemyGameObject, Random.insideUnitCircle * m_areaRadius + (Vector2)transform.position, Quaternion.identity);
            if (enemyGameObject.TryGetComponent(out EnemyModel enemy))
            {
                enemy.InitEnemyObject(m_target.transform, m_enemyScriptableObjects.GetRandomElement());
            }
            m_currentSpawnTimeOut = m_spawnTimeOut;
            m_currentSpawnUnitsCount++;
        }
        else
        {
            m_currentSpawnTimeOut -= Time.deltaTime;
        }
    }
    #endregion
}