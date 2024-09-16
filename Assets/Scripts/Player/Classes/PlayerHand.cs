using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHand : MonoBehaviour
{

    private Vector3 mousePosition;
    private GameObject parent;

    void Awake()
    {
        parent = transform.parent.gameObject;
    }
    void UpdateFacingDirection()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 dir = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        if (mousePosition.x < transform.position.x)
        {
            parent.transform.eulerAngles = Vector3.zero;
            transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f);
        }
        else
        {
            parent.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            dir = Quaternion.Euler(0, 180f, 0) * dir;

            Quaternion worldRotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f);

            transform.rotation = Quaternion.Inverse(parent.transform.rotation) * worldRotation;
        }

    }
    void FixedUpdate()
    {
        UpdateFacingDirection();
    }

}

