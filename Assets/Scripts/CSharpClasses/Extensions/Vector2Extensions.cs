using UnityEngine;

namespace Assets.Scripts.CSharpClasses.Extensions
{
    public static class Vector2Extensions
    {
        public static bool RoundEqual(this Vector2 source, Vector2 compare)
        {
            Vector2 sourceVector = new(Mathf.Round(source.x), Mathf.Round(source.y));
            Vector2 compareVector = new(Mathf.Round(compare.x), Mathf.Round(compare.y));

            return sourceVector == compareVector;
        }

        public static Vector2 GetMovementVector(this Vector2 vector, float speed, Vector2 direction)
        {
            return vector + speed * Time.deltaTime * direction;
        }
    }
}
