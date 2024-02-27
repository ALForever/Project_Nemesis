using System;
using System.Collections.Generic;

namespace Assets.Scripts.Extensions
{
    public static class ListExtensions
    {
        public static T GetRandomElement<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new ArgumentNullException("list");
            }

            Random random = new();
            int randomIndex = random.Next(list.Count);
            return list[randomIndex];
        }

        public static bool IsNullOrEmptyList<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }
    }
}
