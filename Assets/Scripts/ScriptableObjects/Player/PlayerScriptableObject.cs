using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerObject", menuName = "Player Object", order = 54)]
public class PlayerScriptableObject : BaseScriptableObject
{
    #region SerializeField
    [Header("PlayerScriptableObject fileds")]
    [Header("Movement")]
    [SerializeField] private float m_playerSpeed;
    [SerializeField] private float m_dashDistance;
    [Header("Health")]
    [SerializeField] private float m_maxHealth;
    [Header("Level system")]
    [SerializeField] private int m_nextLevelUpValue;
    #endregion

    public float PlayerSpeed => m_playerSpeed;
    public float DashDistance => m_dashDistance;
    public float MaxHealth => m_maxHealth;
    public int NextLevelUpValue => m_nextLevelUpValue;
}
