using Assets.Scripts.CSharpClasses.Interfaces;
using TMPro;
using UnityEngine;

public class CharacterKilledCounterViewModel : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] private TextMeshProUGUI m_killedEnemyTmpText;
    [SerializeField] private TextMeshProUGUI m_killedEnemyCounterTmpText;
    [SerializeField] private GameObject m_character;
    #endregion

    private ICharacter m_characterInerface;

    private int m_killedEnemyCounter;

    void Start()
    {
        if (!m_character.TryGetComponent(out m_characterInerface))
        {
            return;
        }
        m_characterInerface.CurrentHealth.OnValueChanged += HealthChanged;
    }

    private void HealthChanged(float value)
    {
        if (value <= 0)
        {
            m_killedEnemyCounter++;
            m_killedEnemyCounterTmpText.text = m_killedEnemyCounter.ToString();
        }
    }
}
