using UnityEngine;

public class BaseScriptableObject : ScriptableObject
{
    [Header("Standart fields")]
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private string m_name;
    [SerializeField] private string m_description;
    [SerializeField] private Sprite m_icon;

    public GameObject Prefab => m_prefab;
    public string Name => m_name;
    public string Description => m_description;
    public Sprite Icon => m_icon;
}
