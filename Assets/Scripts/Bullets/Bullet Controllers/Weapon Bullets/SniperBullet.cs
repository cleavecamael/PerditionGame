using UnityEngine;
public class SniperBullet : WeaponBullet
{
    protected Sniper sniperStats;
    private int pierceCount;
    [SerializeField] ParticleSystem spark;
    void Awake()
    {
        sniperStats = weaponStats as Sniper;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        pierceCount = sniperStats.currentPierceCount;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        PlaySpark();
        if (collision.CompareTag("Obstacles"))
        {
            gameObject.SetActive(false);
        }
        if (pierceCount > 0)
        {
            if (collision.CompareTag("Enemy") || collision.CompareTag("Interactive"))
            {
                Damage = Damage * pierceCount * 0.2f;
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