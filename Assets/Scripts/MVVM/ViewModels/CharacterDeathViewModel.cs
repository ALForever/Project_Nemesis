using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.CSharpClasses.Interfaces;

public class CharacterDeathViewModel : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] private Button m_restartButton;
    [SerializeField] private TextMeshProUGUI m_deadPlayerTmpText;
    [SerializeField] private GameObject m_character;
    #endregion

    private ICharacter m_characterInerface;

    private TextMeshProUGUI m_restartButtonText;

    void Start()
    {
        if (!m_character.TryGetComponent(out m_characterInerface))
        {
            return;
        }

        m_characterInerface.CurrentHealth.OnValueChanged += HealthChange;

        InitRestartButton();
        SetElementsVisible(false);
    }

    private void InitRestartButton()
    {
        m_restartButton.onClick.AddListener(RestartScene);

        m_restartButtonText = m_restartButton.GetComponentInChildren<TextMeshProUGUI>(true);

        m_restartButtonText.text = "Restart";
        m_restartButtonText.color = m_restartButton.colors.normalColor;
    }

    private void SetElementsVisible(bool visible)
    {
        m_restartButton.gameObject.SetActive(visible);
        m_deadPlayerTmpText.gameObject.SetActive(visible);
    }

    private void HealthChange(float value)
    {
        if (value <= 0)
        {
            SetElementsVisible(true);
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
