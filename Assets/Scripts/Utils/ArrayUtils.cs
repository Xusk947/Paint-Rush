using System;
using System.Collections.Generic;

namespace PaintRush.Utils
{
    /// <summary>
    /// Utility class for array operations.
    /// </summary>
    public static class ArrayUtils
    {
        private static Random random = new Random();

        /// <summary>
        /// Retrieves a random value from the specified array.
        /// </summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="array">The array to retrieve a random value from.</param>
        /// <returns>A random value from the array.</returns>
        /// <exception cref="ArgumentException">Thrown when the array is null or empty.</exception>
        public static T GetRandomValue<T>(T[] array)
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentException("Array is null or empty");
            }

            int index = random.Next(0, array.Length);
            return array[index];
        }

        /// <summary>
        /// Retrieves a random value from the specified dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary keys.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary values.</typeparam>
        /// <param name="dictionary">The dictionary to retrieve a random value from.</param>
        /// <returns>A random value from the dictionary.</returns>
        /// <exception cref="ArgumentException">Thrown when the dictionary is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown when random value retrieval fails (should never be reached).</exception>
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