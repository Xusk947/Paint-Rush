using System.Collections;
using UnityEngine;

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
        if (touchSupported)
        {
            touchPosition = Input.GetTouch(0).position;
        } else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            touchPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        touchPosition.x = Input.mousePosition.x / screenSize.x;

        if (touchPosition.x < 0.33f)
        {
            // Touch on the left third of the screen
            // Set axis value to -1
            _axis.x = -1f;
        }
        else if (touchPosition.x > 0.66f)
        {
            // Touch on the right third of the screen
            // Set axis value to 1
            _axis.x = 1f;
        }
    }
}