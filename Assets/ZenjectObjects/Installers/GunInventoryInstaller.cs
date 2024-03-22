using Assets.Scripts.CSharpClasses.Inventory;
using Zenject;

public class GunInventoryInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GunInventoryBasedOnLevelSystem>().FromNew().AsSingle().NonLazy();
    }
}
