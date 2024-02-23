using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GunObject", menuName = "Gun Object", order = 51)]
public class GunSO : ScriptableObject
{
    [SerializeField]
    private GameObject m_prefab;
    [SerializeField]
    private string m_name;
    [SerializeField]
    private string m_description;
    [SerializeField]
    private Sprite m_icon;
    [SerializeField]
    private float m_damage;
    [SerializeField]
    private int m_minLevel;

    public GameObject Prefab => m_prefab;
    public string Name => m_name;
    public string Description => m_description;
    public Sprite Icon => m_icon;
    public float Damage => m_damage;
    public int MinLevel => m_minLevel;
}
