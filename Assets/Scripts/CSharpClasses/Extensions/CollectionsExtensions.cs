using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.CSharpClasses.Extensions
{
    public static class CollectionsExtensions
    {
        public static T GetRandomElement<T>(this IEnumerable<T> collection)
        {
            if (collection.IsNullObject() || !collection.Any())
            {
                throw new ArgumentNullException("collection");
            }
            T[] array = collection.ToArray();

            Random random = new();
            int randomIndex = random.Next(array.Length);
            return array[randomIndex];
        }

        public static bool IsNullOrEmptyCollection<T>(this IEnumerable<T> collection)
        {
            return collection.IsNullObject() || !collection.Any();
        }
    }
}
