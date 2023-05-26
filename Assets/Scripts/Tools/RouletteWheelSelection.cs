using UnityEngine;
using System;
using System.Collections.Generic;

namespace PaintRush.Tools
{
    /// <summary>
    /// Utility class for performing roulette wheel selection.
    /// </summary>
    /// <typeparam name="T">The type of the items in the roulette wheel.</typeparam>
    public class RouletteWheelSelection<T>
    {
        private readonly List<RouletteItem<T>> _items = new List<RouletteItem<T>>();
        private float _totalDifficulty = 0f;

        /// <summary>
        /// Initializes a new instance of the RouletteWheelSelection class.
        /// </summary>
        public RouletteWheelSelection() { }

        /// <summary>
        /// Initializes a new instance of the RouletteWheelSelection class with the specified items.
        /// </summary>
        /// <param name="items">The items to populate the roulette wheel with.</param>
        public RouletteWheelSelection(IEnumerable<RouletteItem<T>> items)
        {
            foreach (var item in items)
            {
                Add(item.Value, item.Difficulty);
            }
        }

        /// <summary>
        /// Gets the number of items in the roulette wheel.
        /// </summary>
        public int Count
        {
            get { return _items.Count; }
        }

        /// <summary>
        /// Adds an item with the specified difficulty to the roulette wheel.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="difficulty">The difficulty of the item (should be non-negative).</param>
        /// <exception cref="ArgumentException">Thrown when the difficulty is negative.</exception>
        public void Add(T item, float difficulty)
        {
            if (difficulty < 0)
            {
                throw new ArgumentException("Difficulty cannot be negative");
            }

            _items.Add(new RouletteItem<T>(item, difficulty));
            _totalDifficulty += difficulty;
        }

        /// <summary>
        /// Spins the roulette wheel and selects an item based on their difficulties.
        /// </summary>
        /// <returns>The selected item.</returns>
        /// <exception cref="InvalidOperationException">Thrown when there are no items in the roulette wheel.</exception>
        public T Spin()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("No items to select from");
            }

            // Generate a random threshold
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

    /// <summary>
    /// Represents an item in the roulette wheel with its associated value and difficulty.
    /// </summary>
    /// <typeparam name="T">The type of the item value.</typeparam>
    public class RouletteItem<T>
    {
        /// <summary>
        /// Gets the value of the item.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Gets the difficulty of the item.
        /// </summary>
        public float Difficulty { get; }

        /// <summary>
        /// Initializes a new instance of the RouletteItem class with the specified value and difficulty.
        /// </summary>
        /// <param name="value">The value of the item.</param>
        /// <param name="difficulty">The difficulty of the item.</param>
        public RouletteItem(T value, float difficulty)
        {
            Value = value;
            Difficulty = difficulty;
        }
    }
}