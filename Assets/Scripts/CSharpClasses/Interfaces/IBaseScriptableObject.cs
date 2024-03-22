using UnityEngine;

namespace Assets.Scripts.CSharpClasses.Interfaces
{
    public interface IBaseScriptableObject
    {
        public GameObject Prefab { get; }
        public string Name { get; }
        public string Description { get; }
        public Sprite Icon { get; }
    }
}
