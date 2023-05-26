using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace PaintRush.World
{
    /// <summary>
    /// Represents the paint holder in the game world.
    /// </summary>
    public class PaintHolder : MonoBehaviour
    {
        private Stack<GameObject> _paintBalls;
        [SerializeField, ReadOnly(true)]
        private Vector3 _size;
        private int _horizontalRow = 3, _currentHorizontalRow, _currentVerticalRow = 0;

        /// <summary>
        /// Gets the stack of paint balls in the paint holder.
        /// </summary>
        public Stack<GameObject> PaintBalls
        {
            get { return _paintBalls; }
        }

        private void Start()
        {
            _size = Content.PlayerPaintBall.GetComponent<Renderer>().bounds.max;
            _paintBalls = new Stack<GameObject>();
            _currentHorizontalRow = -_horizontalRow;
        }

        /// <summary>
        /// Adds a paint item to the paint holder.
        /// </summary>
        /// <param name="paintItem">The paint item to add.</param>
        public void AddItem(PaintItem paintItem)
        {
            for (int i = 0; i < paintItem.Value; i++)
            {
                _paintBalls.Push(paintItem.gameObject);
            }
        }

        /// <summary>
        /// Removes a paint ball from the paint holder.
        /// </summary>
        public void RemoveItem()
        {
            GameObject paintBall = _paintBalls.Pop();
            Destroy(paintBall);
        }

        private GameObject SpawnPaintBall()
        {
            return Instantiate(Content.PlayerPaintBall);
        }
    }
}
