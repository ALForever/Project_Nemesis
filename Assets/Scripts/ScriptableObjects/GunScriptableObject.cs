using UnityEngine;

[CreateAssetMenu(fileName = "New GunObject", menuName = "Gun Object", order = 51)]
public class GunScriptableObject : ScriptableObject
{
    #region SerializeField
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private string m_name;
    [SerializeField] private string m_description;
    [SerializeField] private Sprite m_icon;
    [SerializeField] private float m_damage;
    [SerializeField] private int m_minLevel;
    [SerializeField] private float m_maxTimeBetweenShots;
    [SerializeField] private float m_force;
    [SerializeField] private float m_maxLife;
    #endregion

    public GameObject Prefab => m_prefab;
    public string Name => m_name;
    public string Description => m_description;
    public Sprite Icon => m_icon;
    public float Damage => m_damage;
    public int MinLevel => m_minLevel;
    public float MaxTimeBetweenShots => m_maxTimeBetweenShots;
    public float Force => m_force;
    public float MaxLife => m_maxLife;
}
