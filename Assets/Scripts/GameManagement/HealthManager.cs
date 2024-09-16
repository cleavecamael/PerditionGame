using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerVariables;
    [SerializeField] float iFrameDuration;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private UnityEvent zeroHealth;
    [SerializeField] private Vector3Position playerPosition;
    // [SerializeField] private bool dodgeState = false;

    [Header("For numbers")]
    [SerializeField] private UnityEvent<int> onSpawnDamageNumbers;
    [SerializeField] private UnityEvent<int> onSpawnHealNumbers;
    bool iFrame;

    public void ResetHealth()
    {
        healthSystem.currentHealth = playerVariables.health;
    }

    // public void OnDodgeEvent(bool val)
    // {
    //     dodgeState = val;
    // }

    public void Damage(float amt)
    {
        // if (!dodgeState)
        // {
        if (!iFrame)
        {
            StartCoroutine(IFrameCounter());
            healthSystem.ReduceHealth(amt);
            onSpawnDamageNumbers.Invoke(Mathf.CeilToInt(amt));
            if (healthSystem.currentHealth <= 0) zeroHealth.Invoke();
        }
        // }
    }

    IEnumerator IFrameCounter()
    {
        iFrame = true;
        yield return new WaitForSeconds(iFrameDuration);
        iFrame = false;
    }

    public void Heal(float amt)
    {
        onSpawnHealNumbers.Invoke(Mathf.CeilToInt(amt));
        healthSystem.AddHealth(amt);
    }
}