using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MenuActionManager : MonoBehaviour
{
    public UnityEvent toggleEscape;

    public void OnToggleEscape(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            toggleEscape.Invoke();
        }
    }
}