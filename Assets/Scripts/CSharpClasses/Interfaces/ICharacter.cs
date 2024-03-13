using Assets.Scripts.CSharpClasses.Reactive;

namespace Assets.Scripts.CSharpClasses.Interfaces
{
    public interface ICharacter
    {
        ReactiveProperty<float> MaxHealth { get; set; }
        ReactiveProperty<float> CurrentHealth { get; set; }
    }
}
