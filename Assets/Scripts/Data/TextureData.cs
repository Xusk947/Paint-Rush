using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace PaintRush
{
    [Serializable]
    public class TextureData
    {
        private Dictionary<SerializableColor, List<SerializableVector2Int>> _points;
        public readonly string Name;
        public readonly bool Finished;

        public Dictionary<Color, List<Vector2Int>> Points
        {
            get
            {
                Dictionary<Color, List<Vector2Int>> converted = new Dictionary<Color, List<Vector2Int>>();
                var keys = _points.Keys.ToList();
                var values = _points.Values.ToList();
                for ( int i = 0; i < keys.Count; i++ )
                {
                    converted.Add(keys[i].ToColor(), SerializableVector2Int.DeserializeVector2IntList(_points[keys[i]]));
                }
                return converted;
            }
        }

        public TextureData(string name, bool finished)
        {
            Name = name;
            Finished = finished;
            _points = new Dictionary<SerializableColor, List<SerializableVector2Int>>();
        }

        public TextureData(string name, Dictionary<Color, List<Vector2Int>> points)
        {
            Name = name;
            Finished = false;
            _points = new Dictionary<SerializableColor, List<SerializableVector2Int>>();

            var keys = points.Keys.ToList();
            var values = points.Values.ToList();
            for(int i = 0; i < points.Count; i++)
            {
                _points.Add(new SerializableColor(keys[i]), SerializableVector2Int.SerializeVector2IntList(values[i]));
            }
        }
    }

    [Serializable]
    public class SerializableVector2Int
    {
        public int x;
        public int y;

        public SerializableVector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static List<SerializableVector2Int> SerializeVector2IntList(List<Vector2Int> vectorList)
        {
            List<SerializableVector2Int> serializedList = new List<SerializableVector2Int>();

            foreach (Vector2Int vector in vectorList)
            {
                SerializableVector2Int serializedVector = vector;
                serializedList.Add(serializedVector);
            }

            return serializedList;
        }

        public static List<Vector2Int> DeserializeVector2IntList(List<SerializableVector2Int> serializedList)
        {
            List<Vector2Int> vectorList = new List<Vector2Int>();

            foreach (SerializableVector2Int serializedVector in serializedList)
            {
                Vector2Int vector = serializedVector;
                vectorList.Add(vector);
            }

            return vectorList;
        }

        public static implicit operator Vector2Int(SerializableVector2Int serializableVector)
        {
            return new Vector2Int(serializableVector.x, serializableVector.y);
        }

        public static implicit operator SerializableVector2Int(Vector2Int vector)
        {
            return new SerializableVector2Int(vector.x, vector.y);
        }
    }
}

[Serializable]
public class SerializableColor
{
    public float r;
    public float g;
    public float b;
    public float a;

    public SerializableColor(Color color)
    {
        r = color.r;
        g = color.g;
        b = color.b;
        a = color.a;
    }

    public Color ToColor()
    {
        return new Color(r, g, b, a);
    }
}