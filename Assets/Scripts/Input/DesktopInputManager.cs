using System.Collections;
using UnityEngine;

namespace PaintRush.Input
{
    public class DesktopInputManager : InputManager
    {
        private void Update()
        {
            _axis = new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
        }
    }
}