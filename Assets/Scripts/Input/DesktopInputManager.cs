using System.Collections;
using UnityEngine;

namespace PaintRush.Input
{
    public class DesktopInputManager : InputManager
    {
        private void Update()
        {
            // Read input from horizontal and vertical axes
            float horizontalInput = UnityEngine.Input.GetAxis("Horizontal");
            float verticalInput = UnityEngine.Input.GetAxis("Vertical");

            // Create a vector from the input values
            _axis = new Vector2(horizontalInput, verticalInput);
        }
    }
}