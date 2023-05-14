using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;
using System;

public class RouletteWheelSelectionTests : MonoBehaviour
{
    private RouletteWheelSelection<string> _selection = new RouletteWheelSelection<string>();

    [Test]
    public void Add_WithNegativeDifficulty_ThrowsArgumentException()
    {
        // Arrange
        string item = "test";
        float difficulty = -1f;

        // Act + Assert
        Assert.Throws<System.ArgumentException>(() => _selection.Add(item, difficulty));
    }

    [Test]
    public void Spin_WithNoItems_ThrowsInvalidOperationException()
    {
        // Act + Assert
        Assert.Throws<System.InvalidOperationException>(() => _selection.Spin());
    }

    [Test]
    public void Spin_WithSingleItem_ReturnsItem()
    {
        // Arrange
        string item = "test";
        float difficulty = 1f;
        _selection.Add(item, difficulty);

        // Act
        string result = _selection.Spin();

        // Assert
        Assert.AreEqual(item, result);
    }

    [Test]
    public void Spin_WithMultipleItems_ReturnsItem()
    {
        // Arrange
        string item1 = "test1";
        float difficulty1 = 1f;
        _selection.Add(item1, difficulty1);
        string item2 = "test2";
        float difficulty2 = 2f;
        _selection.Add(item2, difficulty2);
        string item3 = "test3";
        float difficulty3 = 3f;
        _selection.Add(item3, difficulty3);

        // Act
        string result = _selection.Spin();

        // Assert
        Assert.That(result, Is.EqualTo(item1).Or.EqualTo(item2).Or.EqualTo(item3));
    }

    [Test]
    public void Spin_MultipleTimes_ReturnsItemsWithLowerDifficulty()
    {
        // Arrange
        string item1 = "test1";
        float difficulty1 = 1f;
        _selection.Add(item1, difficulty1);
        string item2 = "test2";
        float difficulty2 = 2f;
        _selection.Add(item2, difficulty2);

        // Act
        string result1 = _selection.Spin();
        string result2 = _selection.Spin();
        string result3 = _selection.Spin();

        // Assert
        Assert.That(result1, Is.EqualTo(item1).Or.EqualTo(item2));
        Assert.That(result2, Is.EqualTo(item1).Or.EqualTo(item2));
        Assert.That(result3, Is.EqualTo(item1).Or.EqualTo(item2));
        Assert.That(result1, Is.Not.EqualTo(result2));
        Assert.That(result1, Is.Not.EqualTo(result3));
        Assert.That(result2, Is.Not.EqualTo(result3));
    }
    [Test]
    public void TestSelection()
    {
        var roulette = new RouletteWheelSelection<string>();
        roulette.Add("A", 10);
        roulette.Add("B", 20);
        roulette.Add("C", 30);
        roulette.Add("D", 40);

        var counts = new Dictionary<string, int>();

        for (int i = 0; i < 100000; i++)
        {
            string item = roulette.Spin();
            if (!counts.ContainsKey(item))
            {
                counts[item] = 0;
            }
            counts[item]++;
            Assert.AreNotEqual(item, default(string));
        }

        Assert.AreEqual(4, counts.Count);
        Assert.GreaterOrEqual(counts["D"], counts["C"]);
        Assert.GreaterOrEqual(counts["C"], counts["B"]);
        Assert.GreaterOrEqual(counts["B"], counts["A"]);
    }

}

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