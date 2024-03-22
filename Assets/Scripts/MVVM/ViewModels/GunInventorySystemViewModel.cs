using Assets.Scripts.CSharpClasses.Interfaces;
using Assets.Scripts.CSharpClasses.Inventory;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GunInventorySystemViewModel : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup m_gridLayoutGroup; 

    private GunInventoryBasedOnLevelSystem m_inventorySystem;

    [Inject]
    public void Init(GunInventoryBasedOnLevelSystem gunInventorySystem)
    {
        m_inventorySystem = gunInventorySystem;
        m_inventorySystem.OnItemAdd += AddGunToUI;
    }

    private void AddGunToUI(IDropableScriptableObject gunScriptableObject)
    {
        GameObject gameObject = new();
        gameObject.name = gunScriptableObject.Name;
        Image image = gameObject.AddComponent<Image>();
        image.sprite = gunScriptableObject.MenuIcon;
        gameObject.transform.SetParent(m_gridLayoutGroup.transform);
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
