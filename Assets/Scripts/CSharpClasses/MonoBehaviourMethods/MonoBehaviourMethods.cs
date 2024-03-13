using UnityEngine;
using Zenject;

namespace Assets.Scripts.CSharpClasses.MonoBehaviourMethods
{
    public static class MonoBehaviourMethods
    {
        public static bool TryInstantiateComponent<T>(
            DiContainer diContainer, GameObject gameObject, Vector2 position, 
            Transform transform, out T component, out GameObject newGameObject)
        {
            newGameObject = diContainer.InstantiatePrefab(gameObject, position, Quaternion.identity, transform);
            return TryGetComponent(newGameObject, out component);
        }

        public static bool TryInstantiateComponent<T>(GameObject gameObject, Vector2 position, out T component, out GameObject newGameObject)
        {
            newGameObject = UnityEngine.Object.Instantiate(gameObject, position, Quaternion.identity);
            return TryGetComponent(newGameObject, out component);
        }

        private static bool TryGetComponent<T>(GameObject gameObject, out T component)
        {
            if (!gameObject.TryGetComponent(out component))
            {
                UnityEngine.Object.Destroy(gameObject);
                return false;
            }
            return true;
        }
    }
}
