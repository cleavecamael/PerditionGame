using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] GameConstants gameConstants;
    [SerializeField] HealthSystem healthSystem;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] private Image bar;
    private float currentHealth;

    void Start()
    {
        currentHealth = healthSystem.currentHealth;
    }

    void Update()
    {
        transform.rotation = Quaternion.identity;
        currentHealth = Mathf.MoveTowards(currentHealth, healthSystem.currentHealth, gameConstants.hpFillSpeed * Time.deltaTime);

        if (healthSystem.currentHealth > 0) bar.fillAmount = currentHealth / playerStats.health;
        else bar.fillAmount = 0;
    }
}