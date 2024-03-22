using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyWtihDropObject", menuName = "Enemy wtih drop Object", order = 53)]
public class EnemyWtihDropScriptableObject : EnemyScriptableObject
{
    [Header("Drop system")]
    [SerializeField] private GameObject m_gunPrefab;
    [SerializeField] private List<DropableGunScriptableObject> m_drop;

    public List<DropableGunScriptableObject> Drop => m_drop;
    public GameObject GunPrefab => m_gunPrefab;
}
