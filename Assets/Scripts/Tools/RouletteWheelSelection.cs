using UnityEngine;
using System;
using System.Collections.Generic;

namespace PaintRush.Tools
{

    public class RouletteWheelSelection<T>
    {
        private readonly List<RouletteItem<T>> _items = new List<RouletteItem<T>>();
        private float _totalDifficulty = 0f;

        public RouletteWheelSelection() { }

        public int Count
        {
            get { return _items.Count; }
        }

        public RouletteWheelSelection(IEnumerable<RouletteItem<T>> items)
        {
            foreach (var item in items)
            {
                Add(item.Value, item.Difficulty);
            }
        }

        public void Add(T item, float difficulty)
        {
            if (difficulty < 0)
            {
                throw new ArgumentException("Difficulty cannot be negative");
            }

            _items.Add(new RouletteItem<T>(item, difficulty));
            _totalDifficulty += difficulty;
        }

        public T Spin()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("No items to select from");
            }

            float threshold = UnityEngine.Random.value * _totalDifficulty;
            float sum = 0f;

            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                sum += item.Difficulty;
                if (sum >= threshold)
                {
                    // Decrease the difficulty of the selected item so that it has a higher chance of being selected next time
                    float newDifficulty = item.Difficulty / 2f;
                    if (newDifficulty < 0.01f)
                    {
                        newDifficulty = 0.01f;
                    }
                    _totalDifficulty -= item.Difficulty - newDifficulty;
                    _items[i] = new RouletteItem<T>(item.Value, newDifficulty);
                    return item.Value;
                }
            }

            // Should never happen, but just in case
            return default(T);
        }
    }

    public class RouletteItem<T>
    {
        public T Value { get; }
        public float Difficulty { get; }

        public RouletteItem(T value, float difficulty)
        {
            Value = value;
            Difficulty = difficulty;
        }
    }
}