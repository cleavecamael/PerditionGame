using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

using Random = UnityEngine.Random;
public class BaseEnemyController : MonoBehaviour, IEnemy
{
    [SerializeField] private EnemyStats stats;
    [SerializeField] private UnityEvent<float> feverOnEnemyKilled;
    [SerializeField] private UnityEvent<Vector3, int> spawnSoul;
    [SerializeField] private UnityEvent<Vector2> spawnCoin;
    [SerializeField] private UnityEvent<Vector3, int> spawnDamageNumber;
    [SerializeField] private Vector3Position playerPosition;
    [SerializeField] private UnityEvent<float> damagePlayer;
    [SerializeField] private UnityEvent onEnemyDestroyed;
    [SerializeField] private float movementSpread = 0;
    public bool victory = false;
    public string currentAttackName;
    public EnemyAttack currentAttack;

    protected EnemyStats.levelStats enemyStats;
    protected AudioSource EnemySource;
    public float currentHealth;
    protected Rigidbody2D rb;
    public bool alive =  false;
    private bool canDamage;
    private float dmgFreqTimer = 0;
    private bool damagingPlayer;
    private Vector3 velocity;
    private bool knockedBack;
    private float currentSpeed;
    private bool slowMovement = false;
    private float slowDownRate = 0.5f;
    public float timeLastAttacked = 0f;
    public bool damageOnContact = false;
    void Awake()
    {
        setLevel(1);
        alive = true;
        rb = GetComponent<Rigidbody2D>();

        canDamage = false;
        dmgFreqTimer = 0;
        EnemySource = GetComponent<AudioSource>();
        knockedBack = false;
    }

    public virtual void setLevel(int level)
    {

        enemyStats = stats.getLevelStats(level);
        currentHealth = enemyStats.health;
        dmgFreqTimer = 0;
        currentSpeed = enemyStats.speed;
    }

   
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            if (alive)
            {
                AudioManager.playClip(EnemySource, "EnemyHit");
                float damage = col.gameObject.GetComponent<Bullet>().DealDamage();
                TakeDamage(damage);
            }
        }
        if (col.CompareTag("Water"))
        {
            {
                slowMovement = true;
            }
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Water"))
        {
            {
                slowMovement = false;
            }
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Player" )
        {
            if (damageOnContact)
            {
                damagingPlayer = true;
            }
           
        }
    }
    protected virtual void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "PlayerDodge")
        {
            damagingPlayer = false;
        }
            
    }

    public virtual void Die()
    {
        // DebugLogger.Log("Dying");
        if (alive)
        {
            spawnCoin.Invoke(transform.position);
            alive = false;
            GetComponent<Collider2D>().enabled = false;
            feverOnEnemyKilled.Invoke(enemyStats.feverGiven);
            spawnSoul.Invoke(transform.position, enemyStats.xpGiven);
            AudioManager.playClip(EnemySource, "EnemyDeath");
            onEnemyDestroyed.Invoke();
            StartCoroutine(GetComponent<BaseEnemyVFX>().fadeDeath());
        }


    }

   protected virtual void Update()
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
    public void setVictory()
    {
        victory = true;
    }
    public virtual void CheckMove()
    {
        if (!knockedBack)
        {
            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(-movementSpread, movementSpread), 0);
            Vector3 velocityVector = randomRotation * GetComponent<EnemyMoveCalc>().enemyMove() * currentSpeed;
            if (slowMovement)
            {
                velocityVector *= slowDownRate;
            }
            rb.velocity = velocityVector;
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
    }

    public void victoryMove()
    {

        rb.velocity = (this.transform.position - playerPosition.pos).normalized * 2f;
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
        spawnDamageNumber.Invoke(transform.position, Mathf.CeilToInt(damage));
        StartCoroutine(GetComponent<BaseEnemyVFX>().blinkRed());
        currentHealth -= damage;
        if (currentHealth <= 0 && alive) Die();
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



    IEnumerator Knockback(Vector3 bulletDir, float knockback)
    {
        knockedBack = true;
        float i = 0.01f;
        while (knockback > i)
        {
            rb.velocity = new Vector2(bulletDir.x / i, bulletDir.y / i);
            i += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        knockedBack = false;
        yield return null;
    }
    IEnumerator Stun(float duration)
    {
        while (knockedBack)
        {
            yield return null;
        }
        float i = 0.1f;
        while (duration > i)
        {
            rb.velocity = Vector2.zero;
            i += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
    IEnumerator Slow(float duration)
    {
        currentSpeed = currentSpeed / 2;
        yield return new WaitForSeconds(duration);
        currentSpeed = enemyStats.speed;
    }

    public void knockbackEnemy(Vector3 bulletDir, float knockback)
    {
        // Debug.Log("enemy knockback");
        StartCoroutine(Knockback(bulletDir, knockback));
    }

    public void slowEnemy(float duration)
    {
        StartCoroutine(Slow(duration));
    }

    public void stunEnemy(float duration)
    {
        StartCoroutine(Stun(duration));
    }
    public float timeElapsedfromAttack()
    {
        return Time.time - timeLastAttacked;
    }

    public void resetLastAttacked()
    {
        timeLastAttacked = Time.time;
    }

    public void orient()
    {
        if (playerPosition.pos.x < this.transform.position.x)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (playerPosition.pos.x > this.transform.position.x)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

}
