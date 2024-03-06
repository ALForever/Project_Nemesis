using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.CSharpClasses.UI
{
    public sealed class HealthBarUI
    {
        private readonly Slider m_healthBar;

        public HealthBarUI(Slider slider)
        {
            m_healthBar = slider;
            m_healthBar.maxValue = 100;
            m_healthBar.value = 100;
            SetHealthBarColor();
        }

        public void SetMaxValue(float maxValue)
        {
            m_healthBar.maxValue = maxValue;
            SetHealthBarColor();
        }

        public void SetValue(float value)
        {
            m_healthBar.value = value;
            SetHealthBarColor();
        }

        private void SetHealthBarColor()
        {
            Image healthBarFillArea = m_healthBar.gameObject.transform.Find("Fill Area")?.Find("Fill")?.GetComponent<Image>();
            if (healthBarFillArea != null)
            {
                float percent = m_healthBar.value / m_healthBar.maxValue * 100;
                healthBarFillArea.color = percent switch
                {
                    float p when p > 60 => Color.green,
                    float p when p > 40 => Color.yellow,
                    _ => Color.red
                };
            }
        }
    }
}
