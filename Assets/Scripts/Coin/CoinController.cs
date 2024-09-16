public class CoinController : BasePowerupController
{
    public CoinConstants coinConstants;

    private float value;

    void Start()
    {
        value = coinConstants.coinValue;
        pickupTime = base.terrainConstants.pickupTime;
        // StartCoroutine(base.TimeOut());
    }
    public override void OnPickup()
    {
        // Debug.Log("pickup coin");
        base.eventPowerup.Raise(value);
        if (transform.parent != null) Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }

}

