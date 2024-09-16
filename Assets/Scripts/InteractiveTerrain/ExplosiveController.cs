using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class ExplosiveController : BaseInteractiveTerrainController
{
    public CircleCollider2D explosiveArea;

    void Start()
    {
        currentHp = terrainConstants.explosiveHp;
        maxHp = terrainConstants.explosiveHp;
        explosiveArea.enabled = false;
        base.Activate();
    }

    void Update()
    {
        if (base.isActive && TestTrigger)
        {
            Hit(1f);
        }
        if (currentHp <= 0 && base.isActive)
        {
            AudioManager.playClip("Explosion", "Explosion2");
            base.Deactivate();
        }

    }
    public override void SpawnPowerup()
    {
        GameObject x = Instantiate(base.powerUp, transform.position, quaternion.identity);
        x.GetComponent<Transform>().SetParent(transform);
        if (!explosiveArea.enabled)
        {
            StartCoroutine(ExplosiveArea());
        }
    }

    IEnumerator ExplosiveArea()
    {
        explosiveArea.enabled = true;
        yield return new WaitForSecondsRealtime(0.5f);
        try
        {
            Transform explosion = transform.Find("Impact03(Clone)");
            Destroy(explosion.gameObject);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        explosiveArea.enabled = false;
    }
    public void ExplosiveActivate()
    {
        base.Activate();
        explosiveArea.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Swarm"))
        {
            Hit(col.gameObject.GetComponent<Swarm>().damage);
        }
        else if (col.gameObject.CompareTag("Bullet"))
        {
            Hit(col.gameObject.GetComponent<Bullet>().Damage);
        }
    }

}