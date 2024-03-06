using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerObject", menuName = "Player Object", order = 53)]
public class PlayerScriptableObject : ScriptableObject
{
    #region SerializeField
    [Header("Standart fields")]
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private string m_name;
    [SerializeField] private string m_description;
    [SerializeField] private Sprite m_icon;

    [Header("PlayerScriptableObject fileds")]
    [SerializeField] private float m_playerSpeed;
    [SerializeField] private float m_dashDistance;
    [SerializeField] private float m_maxHealth;
    #endregion

    public GameObject Prefab => m_prefab;
    public string Name => m_name;
    public string Description => m_description;
    public Sprite Icon => m_icon;
    public float PlayerSpeed => m_playerSpeed;
    public float DashDistance => m_dashDistance;
    public float MaxHealth => m_maxHealth;
}
