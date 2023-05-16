using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintRush.World
{

    public class Collectable : MonoBehaviour
    {

        protected Renderer _renderer;

        protected bool _colected = false;
        private Vector3 _velocity; 
        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void FixedUpdate()
        {
            if ( _colected )
            {
                CollectedAnimation();
            } else
            {
                transform.eulerAngles += new Vector3(0, 2.5f, 0);
            }
        }
    
        private void CollectedAnimation()
        {
            Destroy(gameObject);
        }

        protected virtual void ChangeColor(List<Color> colors)
        {
            _renderer.material.color = colors[Random.Range(0, colors.Count)];
        }

        protected virtual void SubtractColor(Color color)
        {
            _renderer.material.color -= color;
        }

        public virtual void Collect() {
            _colected = true;
            _velocity = new Vector3(0, 0.1f, 0);
        }
    }
}
