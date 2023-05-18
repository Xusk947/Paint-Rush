using System.Collections;
using UnityEngine;

namespace PaintRush.World
{
    public class Block : MonoBehaviour
    {
        [SerializeField]
        public float Difficulty = 1.0f;

        private void Awake()
        {
            gameObject.SetActive(true);
            for(int i = 0; i < transform.childCount; i++) 
            {
                Transform child = transform.GetChild(i);
                child.gameObject.SetActive(true);
            }
        }
    }
}