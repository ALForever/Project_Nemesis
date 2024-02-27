using Assets.Scripts.Extensions;
using System.Collections.Generic;
using UnityEngine;

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

    #region Unity Default Methods
    void Update()
    {
        if (m_enemyScriptableObjects.IsNullOrEmptyList())
        {
            return;
        }
        SpawnEnemy();
    }
    #endregion

    #region Enemy Spawner Area Methods
    private void SpawnEnemy()
    {
        if (m_currentSpawnUnitsCount > m_spawnLimit)
        {
            return;
        }

        if (m_currentSpawnTimeOut <= 0)
        {
            GameObject enemyGameObject = Instantiate(m_enemyGameObject, Random.insideUnitSphere * m_areaRadius + transform.position, Quaternion.identity);
            if (enemyGameObject.TryGetComponent(out EnemyController enemyController))
            {
                enemyController.InitEnemyObject(m_target.transform, m_enemyScriptableObjects.GetRandomElement());
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