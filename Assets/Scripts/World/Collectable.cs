using PaintRush.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintRush.World
{
    /// <summary>
    /// Base class for collectable objects in the game world.
    /// </summary>
    public class Collectable : MonoBehaviour
    {
        protected Renderer _renderer;
        protected bool _collected = false;
        private Vector3 _velocity;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void FixedUpdate()
        {
            if (_collected)
            {
                CollectedAnimation();
            }
            else
            {
                transform.eulerAngles += new Vector3(0, 2.5f, 0);
            }
        }

        /// <summary>
        /// Handles the animation when the collectable object is collected.
        /// </summary>
        private void CollectedAnimation()
        {
            PaintHolder paintHolder = PlayerController.Instance.PaintHolder;
            if (transform.position.z < paintHolder.transform.position.z)
            {
                transform.position = Vector3.MoveTowards(transform.position, paintHolder.transform.position, Time.deltaTime * 60f);
                if (Vector3.Distance(transform.position, paintHolder.transform.position) < 1f)
                    gameObject.SetActive(false);
            }
            else
            {
                transform.position += new Vector3(0, 0.5f * Time.deltaTime * 60f, 0);
            }
        }

        /// <summary>
        /// Changes the color of the collectable object from a list of available colors.
        /// </summary>
        /// <param name="colors">The list of available colors to choose from.</param>
        protected virtual void ChangeColor(List<Color> colors)
        {
            _renderer.material.color = colors[Random.Range(0, colors.Count)] + new Color(0, 0, 0, 1f);
        }

        /// <summary>
        /// Subtracts a color from the current color of the collectable object.
        /// </summary>
        /// <param name="color">The color to subtract.</param>
        protected virtual void SubtractColor(Color color)
        {
            _renderer.material.color -= color;
        }

        /// <summary>
        /// Collects the collectable object.
        /// </summary>
        public virtual void Collect()
        {
            _collected = true;
            _velocity = new Vector3(0, 0.1f, 0);
        }
    }
}
