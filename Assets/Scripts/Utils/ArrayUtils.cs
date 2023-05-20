using System;
using System.Collections.Generic;

namespace PaintRush.Utils
{
    public static class ArrayUtils
    {
        private static Random random = new Random();

        public static T GetRandomValue<T>(T[] array)
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentException("Array is null or empty");
            }

            int index = random.Next(0, array.Length);
            return array[index];
        }

        public static TValue GetRandomValue<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null || dictionary.Count == 0)
            {
                throw new ArgumentException("Dictionary is null or empty");
            }

            int index = random.Next(0, dictionary.Count);
            int currentIndex = 0;

            foreach (KeyValuePair<TKey, TValue> kvp in dictionary)
            {
                if (currentIndex == index)
                {
                    return kvp.Value;
                }

                currentIndex++;
            }

            // This line should never be reached, but added for compilation purposes
            throw new InvalidOperationException("Random value retrieval failed");
        }
    }
}