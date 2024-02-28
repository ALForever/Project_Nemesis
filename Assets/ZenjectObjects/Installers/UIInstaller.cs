using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    #region SerializeField
    [SerializeField] private UIController m_controller;
    #endregion

    public override void InstallBindings()
    {
        Container.Bind<UIController>().FromInstance(m_controller);
    }
}
