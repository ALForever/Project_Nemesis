using UnityEngine;
using Zenject;

public class InputSystemInstaller : MonoInstaller
{
    #region SerializeField
    [SerializeField] private InputController m_controller;
    #endregion

    public override void InstallBindings()
    {
        Container.Bind<InputController>().FromInstance(m_controller);
    }
}