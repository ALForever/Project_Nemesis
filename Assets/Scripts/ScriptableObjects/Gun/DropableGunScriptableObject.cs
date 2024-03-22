using Assets.Scripts.CSharpClasses.Interfaces;
using UnityEngine;

[CreateAssetMenu(fileName = "New DropableGunObject", menuName = "Dropable Gun Object", order = 55)]
public class DropableGunScriptableObject : GunScriptableObject, IDropableScriptableObject
{
    [Header("Drop system")]
    [SerializeField] private float m_dropProbability;
    [SerializeField] private Sprite m_menuIcon;

    public float DropProbability => m_dropProbability;
    public Sprite MenuIcon => m_menuIcon;
}
