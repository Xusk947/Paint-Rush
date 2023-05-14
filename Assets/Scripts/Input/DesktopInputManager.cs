using System.Collections;
using UnityEngine;

public class DesktopInputManager : InputManager
{
    private void Update()
    {
        _axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}