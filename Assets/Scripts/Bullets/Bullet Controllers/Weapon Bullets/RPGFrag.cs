using UnityEngine;

public class RPGFrag : SplitBullet
{
    [SerializeField] ParticleSystem spark;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        PlaySpark();
        if (spawnDepth > 0)
        {
            SpawnSubBullets();
        }

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Obstacles" || collision.gameObject.tag == "Interactive")
        {
            Destroy(this.gameObject);
        }
    }
    private void PlaySpark()
    {
        Instantiate(spark, transform.position, Quaternion.identity);
    }
}