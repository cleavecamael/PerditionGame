using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private UnityEvent gameOver;
    [SerializeField] private UnityEvent<float> takeDamage;
    [SerializeField] private UnityEvent playerDeath;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private ShopSystem shopSystem;
    [SerializeField] private UnityEvent initializeShop;
    private AudioSource playerSource;

    void Awake()
    {
        playerStats.ResetPlayerStats();
        initializeShop.Invoke();
        SetupPassiveUpgrades();
        playerSource = GetComponent<AudioSource>();
    }

    void SetupPassiveUpgrades()
    {
        playerStats.health = playerStats.health + shopSystem.buffHealth * shopSystem.ShopData.levelHealth;
        playerStats.magnetDistance = playerStats.magnetDistance + shopSystem.buffMagnet * shopSystem.ShopData.levelMagnetDistance;
        playerStats.movementSpeed = playerStats.movementSpeed + shopSystem.buffMovement * shopSystem.ShopData.levelMovementSpeed;
    }

    public void Die()
    {
        playerDeath.Invoke();
        AudioManager.playClip(playerSource, "playerDeath");
        GetComponent<Animator>().SetBool("Death", true);
        GetComponent<Rigidbody2D>().simulated = false;
    }

    public void OnDamage()
    {
        AudioManager.playRandomClip(playerSource, new string[] { "playerHit1", "playerHit2" });
    }

    // For 
    public void Disable()
    {
        gameOver.Invoke();
        gameObject.SetActive(false);
    }
}