using System.Collections;
using UnityEngine;

public abstract class BasePowerupController : MonoBehaviour
{
    public FloatGameEvent eventPowerup;
    public TerrainConstants terrainConstants;
    protected float pickupTime;
    protected AudioSource audioSourcePlayer;

    public abstract void OnPickup();

    protected IEnumerator TimeOut()
    {
        yield return new WaitForSecondsRealtime(terrainConstants.timeOut);
        Destroy(gameObject);
    }

    public IEnumerator Animate()
    {
        float currentMovementTime = 0f;
        var origin = transform.position;

        while (currentMovementTime < pickupTime)
        {
            currentMovementTime += Time.deltaTime;
            transform.position = Vector2.Lerp(origin, getPlayerPos(), currentMovementTime / pickupTime);
            yield return null;
        }
        transform.position = getPlayerPos();
    }

    public Vector3 getPlayerPos()
    {
        return GameObject.Find("Player").transform.position;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Magnet"))
        {
            StartCoroutine(Animate());
        }
        if (col.gameObject.CompareTag("Player"))
        {
            OnPickup();
        }
    }


}