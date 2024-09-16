using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/BossHealthSystem")]
public class BossHealthSystem : ScriptableObject
{
    [HideInInspector] public float currentHealth;
    [HideInInspector] public string bossName;
    [HideInInspector] public float maxHealth = 1;
    [HideInInspector] public bool active = false;
    public void AddHealth(float amt)
    {
        currentHealth += amt;
    }

    public void ReduceHealth(float amt)
    {
        currentHealth -= amt;
    }
    public void SetHealth(float amt){
        currentHealth = amt;
    }

    public void SetName(string name)
    {
        this.bossName = name;
    }
}