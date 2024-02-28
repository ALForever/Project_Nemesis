using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_tmpText;
    private int m_killedEnamyCounter;

    public int KilledEnamyCounter
    {
        get => m_killedEnamyCounter;
        set
        {
            if (m_killedEnamyCounter == value)
            {
                return;
            }
            m_killedEnamyCounter = value;
            SetText(value);
        }
    }

    private void SetText(int count)
    {        
        m_tmpText.text = count.ToString();
    }    
}
