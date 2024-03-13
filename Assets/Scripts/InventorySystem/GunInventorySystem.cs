using Assets.Scripts.CSharpClasses.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GunInventorySystem : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] List<GunScriptableObject> m_gunScriptableObjects;
    #endregion

    private readonly Dictionary<string, GunScriptableObject> m_guns = new();
    private string m_currentGunKey;

    private LevelSystem m_levelSystem;

    public int Count => m_guns.Count;
    public GunScriptableObject Gun => m_guns[m_currentGunKey];

    public event Action<GunScriptableObject> OnGunChanged;
    public event Action<GunScriptableObject> OnGunRemoved;

    [Inject]
    public void Init(LevelSystem levelSystem)
    {
        m_levelSystem = levelSystem;
    }

    void Awake()
    {
        AddRangeSkipExistAndNoname(m_gunScriptableObjects);
        m_currentGunKey = m_guns.Keys.First();
    }

    public bool TryAddGun(GunScriptableObject gunScriptable)
    {
        if (gunScriptable.Name.IsNullOrWhiteSpace())
        {
            return false;
        }

        return m_guns.TryAdd(gunScriptable.Name, gunScriptable);
    }

    public bool TryAddGunBasedOnLevelSystem(GunScriptableObject gunScriptable)
    {
        if (gunScriptable.MinLevel > m_levelSystem.CurrentLevel)
        {
            return false;
        }

        return TryAddGun(gunScriptable);
    }

    public void AddRangeSkipExistAndNoname(IEnumerable<GunScriptableObject> gunScriptableObjects)
    {
        foreach (GunScriptableObject gunScriptableObject in gunScriptableObjects)
        {
            //TryAddGun(gunScriptableObject);
            TryAddGunBasedOnLevelSystem(gunScriptableObject);
        }
    }

    public void GoToNextGun()
    {
        if (m_guns.Count == 0 || !m_guns.ContainsKey(m_currentGunKey))
        {
            return;
        }

        List<string> keys = m_guns.Keys.ToList();
        int index = keys.IndexOf(m_currentGunKey) + 1;
        m_currentGunKey = index >= Count ? keys[0] : keys[index];

        GunScriptableObject gunScriptableObject = m_guns[m_currentGunKey];

        OnGunChanged?.Invoke(gunScriptableObject);
    }

    public void RemoveCurrentGun()
    {
        m_guns.Remove(m_currentGunKey, out GunScriptableObject gunScriptableObject);
        OnGunRemoved?.Invoke(gunScriptableObject);
    }
}
