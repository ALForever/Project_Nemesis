using Assets.Scripts.CSharpClasses.Interfaces;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealthViewModel : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] private TextMeshProUGUI m_playerHealthCounterTmpText;
    [SerializeField] private Slider m_healthBarSlider;
    [SerializeField] private GameObject m_character;
    #endregion

    private ICharacter m_characterInerface;
    private bool m_isEventsSubscribed;

    private void Awake()
    {
        m_healthBarSlider.interactable = false;
        InitCharacter(m_character);
    }

    private void SubscribeEvents()
    {
        m_characterInerface.CurrentHealth.Subscribe(x => HealthChange(x));
        m_characterInerface.MaxHealth.Subscribe(x => MaxHealthChange(x));
        m_isEventsSubscribed = true;
    }

    private void UnsubscribeEvents()
    {
        m_characterInerface.CurrentHealth.Unsubscribe(x => HealthChange(x));
        m_characterInerface.MaxHealth.Unsubscribe(x => MaxHealthChange(x));
        m_isEventsSubscribed = false;
    }

    void Start()
    {
        SetElementsVisible(true);
    }

    public void SetCharacter(GameObject character)
    {
        InitCharacter(character);
    }

    private void SetElementsVisible(bool visible)
    {
        m_healthBarSlider.gameObject.SetActive(visible);
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
            Destroy(gameObject);
        }
    }
    private void MaxHealthChange(float value)
    {
        m_healthBarSlider.maxValue = value;
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

    private void InitCharacter(GameObject gameObject)
    {
        if (gameObject.IsUnityNull() || !gameObject.TryGetComponent(out m_characterInerface))
        {
            if (m_isEventsSubscribed)
            {
                UnsubscribeEvents();
            }
            return;
        }

        if (!m_isEventsSubscribed)
        {
            SubscribeEvents();
        }
    }
}
