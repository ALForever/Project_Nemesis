using Assets.Scripts.CSharpClasses.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] List<PlayerScriptableObject> m_levels;
    [SerializeField] PlayerModel m_model;
    #endregion

    private Dictionary<int, PlayerScriptableObject> m_levelDictionary;
    private int m_currentLevel;
    private int m_maxLevel;
    private PlayerScriptableObject m_player;

    public PlayerScriptableObject Player => m_player;
    public int CurrentLevel => m_currentLevel;

    public readonly ReactiveProperty<int> Experience = new();

    public event Action<PlayerScriptableObject> OnNextLevel;

    void Awake()
    {
        m_levelDictionary = m_levels
            .Select((value, index) => (value, index))
            .ToDictionary(x => x.index, x => x.value);

        m_currentLevel = 0;
        m_player = m_levelDictionary[m_currentLevel];
        m_maxLevel = m_levelDictionary.Count - 1;

        Experience.Subscribe(x => ExperienceChanged(x));
    }

    private void ExperienceChanged(int value)
    {
        if (value >= m_player.NextLevelUpValue)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        m_currentLevel++;
        if (m_currentLevel > m_maxLevel)
        {
            Experience.Unsubscribe(x => ExperienceChanged(x));
            return;
        }
        Experience.SetDefault(notify: false);
        m_player = m_levelDictionary[m_currentLevel];
        OnNextLevel?.Invoke(m_player);
    }
}
