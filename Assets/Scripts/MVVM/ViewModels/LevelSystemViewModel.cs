using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelSystemViewModel : MonoBehaviour
{
    [SerializeField] private Slider m_expSlider;

    private LevelSystem m_levelSystem;

    [Inject]
    public void Init(LevelSystem levelSystem)
    {
        m_levelSystem = levelSystem;
    }

    void Start()
    {
        m_expSlider.interactable = false;

        SetDefaultValues();

        m_expSlider.maxValue = m_levelSystem.Player.NextLevelUpValue;

        m_levelSystem.Experience.Subscribe(x => ExperienceChanged(x));
        m_levelSystem.OnNextLevel += NextLevel;

        SetElementsVisible(true);
    }

    private void NextLevel(PlayerScriptableObject playerScriptableObject)
    {
        SetDefaultValues();
        m_expSlider.maxValue = playerScriptableObject.NextLevelUpValue;
    }

    private void SetDefaultValues()
    {
        m_expSlider.maxValue = 0;
        m_expSlider.value = 0;
        m_expSlider.minValue = 0;
    }

    private void ExperienceChanged(int value)
    {
        m_expSlider.value = value;
    }

    private void SetElementsVisible(bool visible)
    {
        m_expSlider.gameObject.SetActive(visible);
    }
}
