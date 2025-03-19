using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class sistemaControles : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputActionMap keyboardMouseMap;
    private InputActionMap vrMap;

    void Start()
    {
        keyboardMouseMap = inputActions.FindActionMap("KeyboardMouse");
        vrMap = inputActions.FindActionMap("VR");

        if (XRSettings.enabled)
        {
            vrMap.Enable();
            keyboardMouseMap.Disable();
        }
        else
        {
            keyboardMouseMap.Enable();
            vrMap.Disable();
        }
    }
}

