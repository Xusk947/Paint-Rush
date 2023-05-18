using PaintRush.Painting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintRush.Data
{

    public class GameData
    {
        public static GameData Instance;

        public Texture2D CurrentTexture, Texture;
        public Dictionary<Color, List<Vector2Int>> Pixels;
        public int CountUseModifier;
        public bool Finished;

        public GameData() {
            Instance = this;
        }

        public void ImportPaintCanvasData(PaintCanvas canvas)
        {
            CurrentTexture = canvas.CurrentTexture;
            Texture = canvas.Texture;
            Pixels = canvas.Pixels;
            CountUseModifier = canvas.CountUseModifier + 1;
            Finished = canvas.Finished;
        }

        public void ExportPaintCanvasData(PaintCanvas to)
        {
            to.Texture = Texture;
            to.CurrentTexture = CurrentTexture;
            to.Pixels = Pixels;
            to.CountUseModifier = CountUseModifier;
            to.Finished = Finished;
        }
    }
}