using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossHealthBarController : MonoBehaviour
{
    [SerializeField] GameConstants gameConstants;
    [SerializeField] BossHealthSystem healthSystem;
    [SerializeField] GameObject healthGroup;
    [SerializeField] private Image bar;
    [SerializeField] TextMeshProUGUI bossName;
    

    private float currentHealth;
    private bool filled = false;
    private bool initialized = false;

    void Awake()
    {
        
       
    }

    public void initialize()
    {
        currentHealth = healthSystem.currentHealth;
        bar.fillAmount = 1;
    }
    void Update()
    {
        if (initialized)
        {
            if (!healthSystem.active)
            {
                initialized = false;
                return;
            }
            transform.rotation = Quaternion.identity;
            currentHealth = Mathf.MoveTowards(currentHealth, healthSystem.currentHealth, 100 * Time.deltaTime);

            if (healthSystem.currentHealth > 0) bar.fillAmount = currentHealth / healthSystem.maxHealth;
            else bar.fillAmount = 0;
        }
        else
        {
            if (healthSystem.active)
            {
                initialize();
                initialized = true;
            }
            
        }
           
      
    }

    public void setText(string name)
    {
        bossName.text = name;
    }

    public void activateHealthBar()
    {
        bar.fillAmount = 1;
        currentHealth = healthSystem.currentHealth;
        healthGroup.SetActive(true);
        bossName.text = healthSystem.bossName;
    }
    public void deactivateHealthBar()
    {
        healthGroup.SetActive(false);
    }
}