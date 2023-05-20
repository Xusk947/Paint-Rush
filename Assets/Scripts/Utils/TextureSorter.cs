using System.Collections.Generic;
using UnityEngine;

namespace PaintRush.Utils
{
    public class TextureSorter : IComparer<Texture>
    {
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