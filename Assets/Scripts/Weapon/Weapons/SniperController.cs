using System.Collections;
using UnityEngine;

public class SniperController : WeaponController
{
    [SerializeField]
    protected Sniper sniperStats;
    bool enemyKilled = false;
    int combo = 1;
    float lastKilled = 0;
    protected override void FireAudio()
    {
        if (Time.time - lastKilled <= 1.7f && enemyKilled && hitEnemy())
        {
            combo++;
        }
        else
        {
            combo = 1;
        }
        AudioManager.playClip(GetComponent<AudioSource>(), "sniperShot");

        float clip = combo <= 5 ? combo : 5;
        AudioManager.playClip("Gun", "combo" + clip);
        enemyKilled = false;
    }
    bool hitEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, shootDirection.pos, 40, 1 << LayerMask.NameToLayer("Obstacles") | 1 << LayerMask.NameToLayer("Enemy"),
           -100, 100);
       RaycastHit2D hitLeft = Physics2D.Raycast(this.transform.position, Quaternion.Euler(0, 0, 1) * shootDirection.pos, 40, 1 << LayerMask.NameToLayer("Obstacles") | 1 << LayerMask.NameToLayer("Enemy"),
           -100, 100);
        RaycastHit2D hitRight = Physics2D.Raycast(this.transform.position, Quaternion.Euler(0, 0, -1) * shootDirection.pos, 40, 1 << LayerMask.NameToLayer("Obstacles") | 1 << LayerMask.NameToLayer("Enemy"),
           -100, 100);
        if (hit.collider == null && hitLeft.collider == null && hitRight.collider == null)
        {
            return false;
        }
        string hitTag = hit.collider != null ? hit.collider.tag : "null";
        string hitLeftTag = hitLeft.collider != null ? hitLeft.collider.tag : "null";
        string hitRightTag = hitRight.collider != null ? hitRight.collider.tag : "null";
        return (hitTag == "Enemy" || hitLeftTag == "Enemy" || hitRightTag == "Enemy");
    }
    protected override void FireCameraShake()
    {
        shootCameraShake.Invoke(sniperStats.shakeParameters);
    }
    public void setEnemyKilled()
    {
       enemyKilled = true;
       lastKilled = Time.time;
    }
    
}
