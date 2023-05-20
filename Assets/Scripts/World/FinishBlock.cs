using PaintRush;
using PaintRush.World;
using UnityEngine;

namespace World
{
    public class FinishBlock : Block
    {
        private Renderer _renderer;

        public Renderer Renderer
        {
            get { return _renderer; }
            set
            {
                _renderer = value;
            }
        }

        private void Awake()
        {
            _renderer = transform.Find("Plane").GetComponent<Renderer>();
        }
    }
}