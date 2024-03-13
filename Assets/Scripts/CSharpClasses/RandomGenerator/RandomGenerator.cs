using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.CSharpClasses.RandomGenerator
{
    public static class RandomGenerator
    {
        public static Random s_random = new();

        public static GunScriptableObject GetRandomGun(IEnumerable<GunScriptableObject> guns)
        {
            Dictionary<GunScriptableObject, float> gunsWithProbability = guns.ToDictionary(x => x, x => x.DropProbability);
            return GetRandomWithProbability(gunsWithProbability);
        }

        public static T GetRandomWithProbability<T>(Dictionary<T, float> dictWithProbability)
        {
            float totalProbability = dictWithProbability.Sum(x => x.Value);
            double randomValue = s_random.NextDouble() * totalProbability;

            foreach (KeyValuePair<T, float> keyValuePair in dictWithProbability)
            {
                randomValue -= keyValuePair.Value;
                if (randomValue <= 0f)
                {
                    return keyValuePair.Key;
                }
            }

            return default;
        }
    }
}
