using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerActionManager : MonoBehaviour
{
    public UnityEvent<Vector2> move;
    public UnityEvent<bool> fireTrigger;
    public UnityEvent reload;
    public UnityEvent<int> swapWeapon;
    public UnityEvent<Vector2> ultimateCast;
    public UnityEvent<bool> ultimateTargetting;
    private bool ultimateTargetingMode = false;
    public UltiConstants ultiConstants;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            move.Invoke(context.ReadValue<Vector2>());
        }
        else if (context.canceled)
        {
            move.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!ultimateTargetingMode)
        {
            if (context.performed)
            {
                fireTrigger.Invoke(true);
            }
            else if (context.canceled)
            {
                fireTrigger.Invoke(false);
            }
        }
        else
        {
            ultimateTargetingMode = false;
            ultimateTargetting.Invoke(ultimateTargetingMode);
            ultimateCast.Invoke(Mouse.current.position.ReadValue());
        }

    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            reload.Invoke();
        }
    }

    public void OnSwapWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            swapWeapon.Invoke((int)context.ReadValue<float>());
        }
    }

    public void OnUltimateCast(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (ultimateTargetingMode) 
            {
                ultimateTargetingMode = false;
                ultimateTargetting.Invoke(ultimateTargetingMode);
            }
            else if (ultiConstants.charge)
            {
                ultimateTargetingMode = true;
                ultimateTargetting.Invoke(ultimateTargetingMode);
            }
        }
    }
}