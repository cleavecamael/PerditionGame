using UnityEngine;

public class UltimatePowerupController : BasePowerupController
{
    void Start()
    {
        pickupTime = base.terrainConstants.pickupTime;
        StartCoroutine(base.TimeOut());
        audioSourcePlayer = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    public override void OnPickup()
    {
        base.eventPowerup.Raise(1f);
        AudioManager.playClip("Pickup", "UltimatePowerup");
        Destroy(gameObject);
    }

}