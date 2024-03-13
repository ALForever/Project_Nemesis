using UnityEngine;
using Zenject;

public class GunInventorySystemInstaller : MonoInstaller
{
    #region SerializeFields
    [SerializeField] GunInventorySystem m_controller;
    #endregion

    public override void InstallBindings()
    {
        Container.Bind<GunInventorySystem>().FromInstance(m_controller).NonLazy();
    }
}
