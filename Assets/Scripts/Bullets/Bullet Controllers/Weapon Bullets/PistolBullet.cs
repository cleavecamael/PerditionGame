using Unity.Mathematics;
using UnityEngine;

public class PistolBullet : WeaponBullet
{
    protected Pistol pistolStats;
    [SerializeField] ParticleSystem spark;


    void Awake()
    {
        pistolStats = weaponStats as Pistol;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        PlaySpark();
        if (collision.CompareTag("Obstacles"))
        {
            gameObject.SetActive(false);
        }
        if (pistolStats.currentPierceCount > 0)
        {
            if (collision.CompareTag("Enemy") || collision.CompareTag("Interactive"))
            {
                pistolStats.currentPierceCount--;
                if (pistolStats.currentPierceCount == 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        else if (pistolStats.currentPierceCount == 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void PlaySpark()
    {
        Instantiate(spark, transform.position, Quaternion.identity);
    }
}