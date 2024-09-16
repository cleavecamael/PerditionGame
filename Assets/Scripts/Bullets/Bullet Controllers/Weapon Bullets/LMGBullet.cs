using UnityEngine;

public class LMGBullet : SplitWeaponBullet
{
    [SerializeField] ParticleSystem spark;
    protected LMG lmgStats;
    private int pierceCount;

    void Awake()
    {
        lmgStats = weaponStats as LMG;
        spawnDepth = lmgStats.fragDepth;
        subBulletCount = lmgStats.fragCount;
        subBulletPrefab = lmgStats.subBulletPrefab;
        bulletSpeed = lmgStats.baseBulletSpeed;
        subBulletLifetime = lmgStats.fragLifetime;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        pierceCount = lmgStats.currentPierceCount;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        PlaySpark();
        if (lmgStats.hasFrag)
        {
            SpawnSubBullets();
        }
        if (collision.CompareTag("Obstacles"))
        {
            gameObject.SetActive(false);
        }
        if(pierceCount > 0){
            if (collision.CompareTag("Enemy") || collision.CompareTag("Interactive"))
            {
                pierceCount--;
                if (pierceCount == 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        else if (pierceCount == 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void PlaySpark()
    {
        Instantiate(spark, transform.position, Quaternion.identity);
    }
}