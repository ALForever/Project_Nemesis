using UnityEngine;

[CreateAssetMenu(fileName = "New GunObject", menuName = "Gun Object", order = 51)]
public class GunScriptableObject : BaseScriptableObject
{
    #region SerializeField
    [Header("GunScriptableObject fileds")]
    [Header("Attack")]
    [SerializeField] private float m_damage;
    [SerializeField] private float m_force;
    [SerializeField] private float m_maxLife;
    [SerializeField] private float m_maxTimeBetweenShots;
    [Header("Level system")]
    [SerializeField] private int m_minLevel;
    [Header("Sound")]
    [SerializeField] private AudioClip m_gunShootSound;
    #endregion

    public float Damage => m_damage;
    public float Force => m_force;
    public float MaxLife => m_maxLife;
    public float MaxTimeBetweenShots => m_maxTimeBetweenShots;
    public int MinLevel => m_minLevel;
    public AudioClip GunShootSound => m_gunShootSound;
}
