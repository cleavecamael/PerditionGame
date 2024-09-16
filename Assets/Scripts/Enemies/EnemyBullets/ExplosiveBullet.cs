using Unity.VisualScripting;
using UnityEngine;
public class ExplosiveBullet : EnemyBullet
{
    [SerializeField] private GameObject spawnBullet;
    [SerializeField] private float spawnLifetime;
    [SerializeField] private float spawnSpeed;
    [SerializeField] private float spawnDamage;

    protected override void destroyBullet()
    {
        
        for (int i = 0; i < 6; i++)
        {
            AudioManager.playRandomClip(AudioManager.getChannel("Explosion"), new string[] { "firework1", "firework2", "firework3" });
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GameObject tmp = Instantiate(spawnBullet, transform.position, Quaternion.identity);
            EnemyBullet bullet = tmp.GetComponent<EnemyBullet>();
            bullet.Initialize(spawnLifetime, spawnDamage);
            bullet.Fire(Quaternion.Euler(0, 0, i * 60) * (Vector2.up), spawnSpeed);
         
        }
        Destroy(this.gameObject);
    }
}
