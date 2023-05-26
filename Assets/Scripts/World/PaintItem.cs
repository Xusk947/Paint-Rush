using PaintRush.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintRush.World
{
    /// <summary>
    /// Represents a paint item that can be collected in the game world.
    /// </summary>
    public class PaintItem : Collectable
    {
        [SerializeField]
        private int _value = 1;

        /// <summary>
        /// Gets the color of the paint item.
        /// </summary>
        public Color Color
        {
            get { return _renderer.sharedMaterial.color; }
        }

        /// <summary>
        /// Gets the value of the paint item.
        /// </summary>
        public int Value
        {
            get { return _value; }
        }

        private void Start()
        {
        }

        /// <summary>
        /// Collects the paint item.
        /// </summary>
        public override void Collect()
        {
            if (_collected) return;
            base.Collect();
            PaintScoreText.Instance.AddScore(_value);
        }

        /// <summary>
        /// Changes the color of the paint item and its child renderers.
        /// </summary>
        /// <param name="colors">The available colors to choose from.</param>
        protected override void ChangeColor(List<Color> colors)
        {
            base.ChangeColor(colors);
            for (int i = 0; i < transform.childCount; i++)
            {
                Renderer childRenderer = transform.GetChild(i).GetComponent<Renderer>();
                childRenderer.material.color = _renderer.sharedMaterial.color + new Color(0, 0, 0, 1f);
            }
        }

        /// <summary>
        /// Subtracts a color from the paint item and its child renderers.
        /// </summary>
        /// <param name="color">The color to subtract.</param>
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
