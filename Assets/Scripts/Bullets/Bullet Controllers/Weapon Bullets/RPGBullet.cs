using UnityEngine;

public class RPGBullet : SplitWeaponBullet
{
    protected RPG rpgStats;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 knockbackDirection;
    public Animator rpgAnimator;
    private Rigidbody2D explosionBody;


    void Awake()
    {
        rpgStats = weaponStats as RPG;
        spawnDepth = rpgStats.fragDepth;
        subBulletCount = rpgStats.fragCount;
        subBulletPrefab = rpgStats.subBulletPrefab;
        subBulletLifetime = rpgStats.fragLifetime;
        bulletSpeed = rpgStats.baseBulletSpeed;
        explosionBody = GetComponent<Rigidbody2D>();
    }

    public override void Fire(Vector3 direction, float speed)
    {
        explosionBody.bodyType = RigidbodyType2D.Dynamic;
        rpgAnimator.SetBool("Idle", true);
        base.Fire(direction, speed);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        rpgAnimator.SetBool("Idle", false);
        explosionBody.bodyType = RigidbodyType2D.Static;
        startPosition = gameObject.transform.position;
        if (rpgStats.hasFrag)
        {
            SpawnSubBullets();
        }

        if (rpgStats.currentSplash > 0)
        {
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, rpgStats.currentSplash);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Enemy"))
                {
                    endPosition = hitCollider.gameObject.transform.position;
                    knockbackDirection = getKnockbackDirection(startPosition, endPosition);
                    if (hitCollider.TryGetComponent<BaseEnemyController>(out BaseEnemyController controller))
                    {
                        controller.TakeDamage(rpgStats.currentSplashDamage);
                        controller.knockbackEnemy(knockbackDirection.normalized, rpgStats.currentKnockback);
                    }
                }
            }
        }
        AudioManager.playClip("Explosion", "Explosion2");
        rpgAnimator.Play("Explosion", -1, 0f);
    }

    protected Vector3 getKnockbackDirection(Vector3 startPosition, Vector3 endPosition)
    {
        return new Vector3(endPosition.x - startPosition.x, endPosition.y - startPosition.y, 1f);
    }
    public void SetInactive()
    {
        explosionBody.bodyType = RigidbodyType2D.Dynamic;
        rpgAnimator.Rebind();
        rpgAnimator.Update(0f);
        gameObject.SetActive(false);
    }
}