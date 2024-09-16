using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/HealthSystem")]
public class HealthSystem : ScriptableObject
{
    public float currentHealth;

    public void AddHealth(float amt)
    {
        currentHealth += amt;
    }

    public void ReduceHealth(float amt)
    {
        currentHealth -= amt;
    }
    public void SetHealth(float amt)
    {
        currentHealth = amt;
    }
}