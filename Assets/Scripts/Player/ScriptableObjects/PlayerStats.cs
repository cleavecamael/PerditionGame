using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Base stats")]
    public int baseHealth;
    public int baseMovementSpeed;
    public int baseMagnetDistance;
    public float baseShootDrag;
    public float dodgeSpeed;
    public float dodgeDuration;
    public float dodgeCooldown;

    [Header("Current stats")]
    public int health;
    public int movementSpeed;
    public int magnetDistance;
    public float shootDrag;

    public void ResetPlayerStats()
    {
        health = baseHealth;
        movementSpeed = baseMovementSpeed;
        magnetDistance = baseMagnetDistance;
        ResetShootDrag();
    }

    public void SetShootDrag(float drag)
    {
        shootDrag = drag;
    }

    public void ResetShootDrag()
    {
        shootDrag = baseShootDrag;
    }
}
