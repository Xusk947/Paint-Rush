using System.Collections;
using UnityEngine;

namespace PaintRush.World
{
    /// <summary>
    /// Represents a block in the game world.
    /// </summary>
    public class Block : MonoBehaviour
    {
        /// <summary>
        /// The difficulty level of the block.
        /// </summary>
        [SerializeField]
        public float Difficulty = 1.0f;

        private void Awake()
        {
            // Activate the block and its child objects
            gameObject.SetActive(true);
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                child.gameObject.SetActive(true);
            }
        }
    }
}