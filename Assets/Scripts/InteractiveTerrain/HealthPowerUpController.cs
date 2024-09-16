using UnityEngine;

public class HealthPowerupController : BasePowerupController
{
    private float health;
    void Start()
    {
        health = base.terrainConstants.healthPowerUp;
        pickupTime = base.terrainConstants.pickupTime;
        StartCoroutine(base.TimeOut());
        audioSourcePlayer = GameObject.Find("Player").GetComponent<AudioSource>();
    }
    public override void OnPickup()
    {
        // Debug.Log("pickup");
        AudioManager.playClip("Pickup", "Heal");
        base.eventPowerup.Raise(health);
        Destroy(gameObject);
    }

}

