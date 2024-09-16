using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Vector3Position shootDirection;
    [SerializeField] private UnityEvent<bool> onShootTrigger;

    void Update()
    {
        UpdateShootDirection();
    }

    public void OnShootTrigger(bool trigger)
    {
        onShootTrigger.Invoke(trigger);
    }

    public void UpdateShootDirection()
    {
        // Compute direction of crosshair
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 direction = mousePosition - transform.position;

        shootDirection.pos = new Vector2(direction.x, direction.y).normalized;

    }

}