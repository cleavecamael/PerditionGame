using UnityEngine;

public class EyeController : BaseEnemyController
{
    [SerializeField] private EnemyProjectileStats projectileStats;
    EnemyProjectileStats.ProjectileLevelStats projectileLevelStats;

    public override void setLevel(int level)
    {
        base.setLevel(level);
        projectileLevelStats = projectileStats.GetStats(level);
    }
    public override void Die()
    {
        base.Die();
    }

    public void PlayFireSound()
    {
        AudioManager.playClip(EnemySource, "EyeFire");
    }
    public bool hitPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, getPlayerPosition(), 40, 1 << LayerMask.NameToLayer("Obstacles") | 1 << LayerMask.NameToLayer("Player"),
           -100, 100);

        if (hit.collider == null)
        {
            return true;
        }
        string hitTag = hit.collider != null ? hit.collider.tag : "null";

        return (hitTag != "Obstacles");
    }

    public void FireBasic()
    {
        Vector2 direction = Aim();
        var b = Instantiate(projectileLevelStats.projectilePrefab, transform.position, Quaternion.identity);
        EnemyBullet bullet = b.GetComponent<EnemyBullet>();
        bullet.Initialize(projectileLevelStats.projectileLifetime, enemyStats.damage);
        bullet.Fire(direction, projectileLevelStats.projectileSpeed);
        PlayFireSound();
    }
}
