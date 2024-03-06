using Assets.Scripts.CSharpClasses.EditorExtensions;
using Assets.Scripts.CSharpClasses.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealthViewModel : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] private TextMeshProUGUI m_playerHealthTmpText;
    [SerializeField] private TextMeshProUGUI m_playerHealthCounterTmpText;
    [SerializeField] private Slider m_healthBarSlider;
    [SerializeField] private GameObject m_character;
    #endregion

    private ICharacter m_characterInerface;

    void Start()
    {
        if (!m_character.TryGetComponent(out m_characterInerface))
        {
            return;
        }

        m_characterInerface.CurrentHealth.OnValueChanged += HealthChange;

        SetElementsVisible(true);

        m_healthBarSlider.maxValue = 100;
        m_healthBarSlider.value = 100;

        SetHealthBarColor();
    }

    private void SetElementsVisible(bool visible)
    {
        m_healthBarSlider.gameObject.SetActive(visible);
        m_playerHealthTmpText.gameObject.SetActive(visible);
        m_playerHealthCounterTmpText.gameObject.SetActive(visible);
    }

    private void HealthChange(float value)
    {
        m_healthBarSlider.value = value;
        m_playerHealthCounterTmpText.text = value.ToString();
        SetHealthBarColor();

        if (value <= 0)
        {
            SetElementsVisible(false);
        }
    }

    private void SetHealthBarColor()
    {
        Image healthBarFillArea = m_healthBarSlider.gameObject.transform.Find("Fill Area")?.Find("Fill")?.GetComponent<Image>();
        if (healthBarFillArea != null)
        {
            float percent = m_healthBarSlider.value / m_healthBarSlider.maxValue * 100;
            healthBarFillArea.color = percent switch
            {
                float p when p > 60 => Color.green,
                float p when p > 40 => Color.yellow,
                _ => Color.red
            };
        }
    }
}
