using System.Collections;
using UnityEngine;

namespace PaintRush.Input
{
    public abstract class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        protected Vector2 _axis = Vector2.zero;

        // Property to get or set the input axis
        public Vector2 Axis
        {
            get { return _axis; }
            set { _axis = value; }
        }

        public InputManager()
        {
            // Set the instance to this input manager
            Instance = this;
        }
    }
}