using System.Collections;
using UnityEngine;

namespace PaintRush.Input
{
    public abstract class InputManager : MonoBehaviour {
        public static InputManager Instance;

        protected Vector2 _axis = Vector2.zero;

        public Vector2 Axis
        {
            get { return _axis; }
            set { _axis = value; }
        }
        public InputManager()
        {
            Instance = this;
        }
    }
}