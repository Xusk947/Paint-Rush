using System.Collections.Generic;
using UnityEngine;

namespace PaintRush.Utils
{
    /// <summary>
    /// Utility class for sorting textures based on their names.
    /// </summary>
    public class TextureSorter : IComparer<Texture>
    {
        /// <summary>
        /// Compares two textures based on their names and returns a value indicating their relative order.
        /// </summary>
        /// <param name="x">The first texture to compare.</param>
        /// <param name="y">The second texture to compare.</param>
        /// <returns>
        /// A signed integer that indicates the relative order of the textures.
        /// Returns a negative value if x is less than y, zero if they are equal, or a positive value if x is greater than y.
        /// </returns>
        public int Compare(Texture x, Texture y)
        {
            int xNumber = GetNumberFromTextureName(x);
            int yNumber = GetNumberFromTextureName(y);

            return xNumber.CompareTo(yNumber);
        }

        private int GetNumberFromTextureName(Texture texture)
        {
            string name = texture.name;
            int start = name.LastIndexOf('_') + 1;
            int end = name.LastIndexOf('.');

            string numberString = name.Substring(start, end - start);
            int number;

            if (int.TryParse(numberString, out number))
            {
                return number;
            }

            return 0;
        }
    }
}