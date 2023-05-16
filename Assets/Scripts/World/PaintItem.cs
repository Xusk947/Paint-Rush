using PaintRush.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintRush.World
{
    public class PaintItem : Collectable
    {
        [SerializeField]
        private int _value = 1;

        public Color Color
        {
            get { return _renderer.sharedMaterial.color; }
        }

        public int Value
        {
            get { return _value; }
        }
        private void Start()
        {
            ChangeColor(GameManager.Instance.PaintCanvas.Colors);
        }

        public override void Collect()
        {
            if (_colected) return;
            base.Collect();
            PaintScoreText.Instance.AddScore(_value);
        }

        protected override void ChangeColor(List<Color> colors)
        {
            base.ChangeColor(colors);
            for (int i = 0; i < transform.childCount; i++)
            {
                Renderer childRenderer = transform.GetChild(i).GetComponent<Renderer>();
                childRenderer.material.color = _renderer.sharedMaterial.color;
            }
        }

        protected override void SubtractColor(Color color)
        {
            base.SubtractColor(color);
            for (int i = 0; i < transform.childCount; i++)
            {
                Renderer childRenderer = transform.GetChild(i).GetComponent<Renderer>();
                childRenderer.material.color -= color;
            }
        }
    }
}