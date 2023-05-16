using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintRush.World
{
    public class FinishBlock : Block
    {
        public static FinishBlock Instance;

        public GameObject PlayerPosition;
        public GameObject PaintCanvasSpawn;

        private void Awake()
        {
            Instance = this;
            PlayerPosition = transform.Find("PlayerPosition").gameObject;
            PaintCanvasSpawn = transform.Find("PaintCanvasSpawn").gameObject;
        }
    }
}
