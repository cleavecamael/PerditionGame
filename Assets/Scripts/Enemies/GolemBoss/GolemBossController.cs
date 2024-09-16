using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GolemBossController : BaseEnemyController
{
    //can be object pool or prefab
    public GameObject BasicBullet;
    public BossHealthSystem healthSystem;

    public bool spawnedGolems = false;
    public UnityEvent onClearLevel;
    public UnityEvent onSpawnBoss;
    public UnityEvent onDespawnBoss;
    public UnityEvent<Vector2> knockback;
    [SerializeField] private float knockbackAmount;
    public void Start()
    {
        onGameStart();
        onSpawnBoss.Invoke();

    }

    protected override void Update()
    {
        base.Update();
        healthSystem.SetHealth(currentHealth);
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {

        base.OnCollisionEnter2D(col);

        if (GetComponent<GolemBossAttacks>().charging)
        {
            Vector2 currDirection = rb.velocity;
            Vector2 colPoint = col.transform.position;
            bool clockwise;
            //Incredible piece of code
            if ((colPoint + (Vector2)(Quaternion.Euler(0, 0, 90) * (Vector3)currDirection) - (Vector2)transform.position).magnitude
                > (colPoint - (Vector2)transform.position).magnitude)
            {
                clockwise = true;
            }
            else
            {
                clockwise = false;
            }
            Vector2 knockbackDirection = clockwise ? (Vector2)(Quaternion.Euler(0, 0, 90) * (Vector3)currDirection) : (Vector2)(Quaternion.Euler(0, 0, -90) * (Vector3)currDirection);
            Vector2 knockbackNormalized = knockbackDirection.normalized;
            knockback.Invoke(knockbackNormalized * knockbackAmount);
        }
    }

    public void onSpawn()
    {
        healthSystem.SetName("Super Golem");
        healthSystem.currentHealth = currentHealth;
        healthSystem.maxHealth = currentHealth;
        healthSystem.active = true;
    }
    public void onDespawn()
    {
        healthSystem.active = false;
    }
    public void onGameStart()
    {
        spawnedGolems = false;
    }
    public override void Die()
    {
        onDespawnBoss.Invoke();

        //base.Die();


    }

    public void setInactive()
    {
        this.gameObject.SetActive(false);
    }
    IEnumerator delayedDeath()
    {
        yield return new WaitForSeconds(5f);
        onClearLevel.Invoke();
    }
}
