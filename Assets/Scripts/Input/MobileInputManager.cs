using PaintRush.Input;
using System.Collections;
using UnityEngine;

namespace PaintRush
{
    public class MobileInputManager : InputManager
    {
        private Vector2 screenSize;

        private void Awake()
        {
            screenSize = new Vector2(Screen.width, Screen.height);
        }

        private void Update()
        {
            // Reset the horizontal axis value
            _axis.x = 0;

            // Check if touch input is supported
            if (!UnityEngine.Input.touchSupported) return;

            // Check if there is at least one touch
            if (UnityEngine.Input.touchCount <= 0) return;

            // Get the first touch
            Touch touch = UnityEngine.Input.touches[0];

            // Check if the touch is in the moved phase
            if (touch.phase == TouchPhase.Moved)
            {
                // Update the horizontal axis value based on touch delta position
                _axis = touch.deltaPosition / 10f;
            }
        }
    }
}