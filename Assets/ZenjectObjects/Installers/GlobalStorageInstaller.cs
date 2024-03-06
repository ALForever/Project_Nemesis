using Assets.Scripts.CSharpClasses.Storage;
using Zenject;

public class GlobalStorageInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GlobalStorage>().FromNew().AsSingle().NonLazy();
    }
}
