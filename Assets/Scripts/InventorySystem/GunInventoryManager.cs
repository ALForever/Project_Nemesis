using Assets.Scripts.CSharpClasses.Inventory;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GunInventoryManager : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] List<DropableGunScriptableObject> m_gunScriptableObjects;
    #endregion

    private LevelSystem m_levelSystem;
    private GunInventoryBasedOnLevelSystem m_inventoryBasedOnLevelSystem;

    [Inject]
    public void Init(LevelSystem levelSystem, GunInventoryBasedOnLevelSystem gunInventoryBasedOnLevelSystem)
    {
        m_levelSystem = levelSystem;
        m_inventoryBasedOnLevelSystem = gunInventoryBasedOnLevelSystem;
    }

    void Awake()
    {
        m_inventoryBasedOnLevelSystem.Configurate(m_levelSystem, m_gunScriptableObjects);
    }
}
