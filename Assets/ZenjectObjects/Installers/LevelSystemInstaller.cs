using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelSystemInstaller : MonoInstaller
{
    [SerializeField] private LevelSystem m_controller;

    public override void InstallBindings()
    {
        Container.Bind<LevelSystem>().FromInstance(m_controller).NonLazy();
    }
}
