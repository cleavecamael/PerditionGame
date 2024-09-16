using UnityEngine;

public class ShotgunBullet : WeaponBullet
{
    protected Shotgun shotgunStats;
    [SerializeField] ParticleSystem spark;
    private int pierceCount;
    void Awake()
    {
        shotgunStats = weaponStats as Shotgun;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        pierceCount = shotgunStats.currentPierceCount;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        PlaySpark();
        if (pierceCount >= 0)
        {
            if (collision.CompareTag("Enemy"))
            {
                pierceCount--;
                if (collision.TryGetComponent<BaseEnemyController>(out BaseEnemyController controller))
                {
                    controller.knockbackEnemy(GetComponent<Rigidbody2D>().velocity.normalized, shotgunStats.currentKnockback);
                    controller.stunEnemy(shotgunStats.currentStunDuration);
                    controller.slowEnemy(shotgunStats.currentSlowDuration);
                }
                if (pierceCount < 0)
                {
                    gameObject.SetActive(false);
                }
            }
            else if (collision.CompareTag("Interactive"))
            {
                pierceCount--;
                if (pierceCount < 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        else if (pierceCount < 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void PlaySpark()
    {
        Instantiate(spark, transform.position, Quaternion.identity);
    }
}