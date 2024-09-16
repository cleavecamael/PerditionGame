using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SplitBullet : Bullet
{
    protected Collider2D col;
    protected int spawnDepth;
    protected int subBulletCount;
    protected float subBulletLifetime;
    protected float bulletSpeed;
    protected GameObject subBulletPrefab;

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    public void Initialize(GameObject prefab, float damage, float lifetime, float speed, int maxDepth, int bulletCount)
    {
        subBulletPrefab = prefab;
        Damage = damage;
        CurrentLifetime = lifetime;
        subBulletLifetime = lifetime;
        spawnDepth = maxDepth;
        subBulletCount = bulletCount;
        bulletSpeed = speed;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (spawnDepth > 0)
        {
            SpawnSubBullets();
        }
    }

    public void DisableCollider(float duration)
    {
        StartCoroutine(DisableColForSeconds(duration));
    }

    IEnumerator DisableColForSeconds(float seconds)
    {
        col.enabled = false;

        yield return new WaitForSeconds(seconds);

        col.enabled = true;
    }

    protected virtual void SpawnSubBullets()
    {
        List<Vector3> vels = WeaponUtils.GetVelocities(subBulletCount, Vector3.up, 360);
        for (int i = 0; i < subBulletCount; i++)
        {
            GameObject b = Instantiate(subBulletPrefab, transform.position, Quaternion.identity);
            SplitBullet bullet = b.GetComponent<SplitBullet>();
            bullet.Initialize(subBulletPrefab, Damage, subBulletLifetime, bulletSpeed, spawnDepth - 1, subBulletCount);
            bullet.DisableCollider(0.1f);
            bullet.Fire(vels[i], bulletSpeed);
        }
    }
}