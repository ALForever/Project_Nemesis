using Assets.Scripts.CSharpClasses.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] private Button m_restartButton;
    [SerializeField] private TextMeshProUGUI m_killedEnemyTmpText;
    [SerializeField] private TextMeshProUGUI m_killedEnemyCounterTmpText;
    [SerializeField] private TextMeshProUGUI m_deadPlayerTmpText;
    [SerializeField] private TextMeshProUGUI m_playerHealthTmpText;
    [SerializeField] private TextMeshProUGUI m_playerHealthCounterTmpText;
    [SerializeField] private Slider m_healthBarSlider;
    #endregion

    private TextMeshProUGUI m_restartButtonText;

    private HealthBarUI m_healthBarUI;

    private int m_killedEnemyCounter;

    public int KilledEnemyCounter
    {
        get => m_killedEnemyCounter;
        set
        {
            if (m_killedEnemyCounter == value)
            {
                return;
            }
            m_killedEnemyCounter = value;
            SetText(value);
        }
    }

    void Start()
    {
        m_healthBarUI = new(m_healthBarSlider);
        InitRestartButton();
        SetUiElementsdEnabled(false);
    }

    private void SetText(int count)
    {
        m_killedEnemyCounterTmpText.text = count.ToString();
    }

    private void InitRestartButton()
    {
        m_restartButton.onClick.AddListener(RestartScene);

        m_restartButtonText = m_restartButton.GetComponentInChildren<TextMeshProUGUI>(true);

        m_restartButtonText.text = "Restart";
        m_restartButtonText.color = m_restartButton.colors.normalColor;
    }

    private void SetUiElementsdEnabled(bool enable)
    {
        m_deadPlayerTmpText.enabled = m_restartButton.interactable = m_restartButtonText.enabled = enable;

        m_killedEnemyTmpText.enabled = m_killedEnemyCounterTmpText.enabled = !enable;
        m_playerHealthTmpText.enabled = m_playerHealthCounterTmpText.enabled = !enable;
        m_healthBarSlider.gameObject.SetActive(!enable);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowPlayerDeathText()
    {
        SetUiElementsdEnabled(true);
    }

    public void SetPlayerHealthValue(float value)
    {
        m_playerHealthCounterTmpText.text = value.ToString();
        m_healthBarUI.SetValue(value);
    }

    public void SetMaxHealth(float value)
    {
        m_healthBarUI.SetMaxValue(value);
    }
}
