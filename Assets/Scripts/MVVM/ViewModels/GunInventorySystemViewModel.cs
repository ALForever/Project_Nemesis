using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GunInventorySystemViewModel : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup m_gridLayoutGroup; 

    private GunInventorySystem m_inventorySystem;

    [Inject]
    public void Init(GunInventorySystem gunInventorySystem)
    {
        m_inventorySystem = gunInventorySystem;
        m_inventorySystem.OnGunAdded += AddGunToUI;
    }

    private void AddGunToUI(GunScriptableObject gunScriptableObject)
    {
        GameObject gameObject = new();
        gameObject.name = gunScriptableObject.Name;
        Image image = gameObject.AddComponent<Image>();
        image.sprite = gunScriptableObject.MenuIcon;
        gameObject.transform.SetParent(m_gridLayoutGroup.transform);
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
