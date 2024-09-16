using UnityEngine;
using UnityEngine.Events;

public class EnemyControllerKinematic : MonoBehaviour, IEnemy
{
    [SerializeField] private EnemyStats stats;
    [SerializeField] private UnityEvent<float> feverOnEnemyKilled;
    [SerializeField] private UnityEvent<int> xpOnEnemyKilled;
    [SerializeField] private Vector3Position playerPosition;
    [SerializeField] private UnityEvent<float> damagePlayer;
    [SerializeField] private float movementSpread = 0;
    EnemyStats.levelStats enemyStats;
    protected AudioSource EnemySource;
    public float currentHealth;
    protected Rigidbody2D rb;
    public bool alive;
    private bool canDamage;
    private float dmgFreqTimer;
    private bool damagingPlayer;
    private Vector3 velocity;


    void Awake()
    {
        enemyStats = stats.getLevelStats(1);
        alive = true;
        currentHealth = enemyStats.health;
        rb = GetComponent<Rigidbody2D>();
        canDamage = false;
        dmgFreqTimer = enemyStats.damageFreq;
        EnemySource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Player")
        {

            damagingPlayer = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            AudioManager.playClip(EnemySource, "EnemyHit");
            TakeDamage(col.gameObject.GetComponent<Bullet>().Damage);
            StartCoroutine(GetComponent<BaseEnemyVFX>().blinkRed());
        }

    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player") damagingPlayer = false;
    }

    public virtual void Die()
    {
        // DebugLogger.Log("Dying");
        alive = false;
        feverOnEnemyKilled.Invoke(enemyStats.feverGiven);
        xpOnEnemyKilled.Invoke(enemyStats.xpGiven);
        AudioManager.playClip(EnemySource, "EnemyDeath");

        StartCoroutine(GetComponent<BaseEnemyVFX>().fadeDeath());
    }

    void Update()
    {

        CheckDamagePlayer();

    }

    void CheckDamagePlayer()
    {
        if (damagingPlayer)
        {
            if (!canDamage)
            {
                dmgFreqTimer -= Time.deltaTime;
                if (dmgFreqTimer < 0)
                {
                    canDamage = true;
                    dmgFreqTimer = enemyStats.damageFreq;
                }
            }
            else
            {
                canDamage = false;

                damagePlayer.Invoke(enemyStats.damage);
            }
        }
    }

    public virtual void CheckMove()
    {
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(-movementSpread, movementSpread), 0);
        rb.velocity = randomRotation * GetComponent<EnemyMoveCalc>().enemyMove() * enemyStats.speed;
        velocity = rb.velocity;

        if (rb.velocity.x < 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

    }
    public void StopMove()
    {
        rb.velocity = Vector2.zero;

    }

    public void TakeDamage(float damage)
    {
        // DebugLogger.Log("Taking Damage: " + damage.ToString());
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    //Returns a 2D vector (normalized) pointing at the player
    public Vector2 Aim()
    {
        Vector2 res = playerPosition.pos - transform.position;
        return res.normalized;
    }
    public Vector2 getPlayerPosition()
    {

        return playerPosition.pos;
    }



}
