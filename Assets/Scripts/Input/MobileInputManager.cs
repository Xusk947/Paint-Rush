using PaintRush.Input;
using System.Collections;
using UnityEngine;

namespace PaintRush
{

    public class MobileInputManager : InputManager
    {
        static bool touchSupported => UnityEngine.Input.touchSupported;

        private Vector2 screenSize;

        private void Awake()
        {
            screenSize = new Vector2(Screen.width, Screen.height);
        }

        private void Update()
        {
            _axis.x = 0;
            Vector2 touchPosition = Vector2.zero;
            if (UnityEngine.Input.touchCount <= 0) return;

            Touch touch = UnityEngine.Input.touches[0];

            if (touch.phase == TouchPhase.Moved)
            {
                _axis = touch.deltaPosition / 10f;
            }

        }
    }
}