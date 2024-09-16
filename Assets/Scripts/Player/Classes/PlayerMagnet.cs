using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{

    private CircleCollider2D magnetCollider;
    public PlayerStats playerVariables;

    void Start()
    {
        magnetCollider = GetComponent<CircleCollider2D>();
        magnetCollider.radius = playerVariables.magnetDistance;
        magnetCollider.isTrigger = true;
    }
}

