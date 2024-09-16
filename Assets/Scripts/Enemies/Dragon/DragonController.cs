using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class DragonController : BaseEnemyController
{
    //can be object pool or prefab
    public GameObject BasicBullet;
    public AttackPattern machineGunPattern;
    public string attackState;
    public UnityEvent onSpawnBoss;
    public UnityEvent onDespawnBoss;
    public BossHealthSystem healthSystem;
    public Hitbox hitbox;
    public LaserController laser;

    private void Start()
    {
        onSpawnBoss.Invoke();


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
    protected override void Update()
    {
        base.Update();
        healthSystem.SetHealth(currentHealth);
    }

    public void machineGunAttack()
    {
        machineGunPattern.activatePattern(5.0f);
    }
    public void onSpawn()
    {
        healthSystem.SetName("Dragon");
        healthSystem.maxHealth = currentHealth;
        healthSystem.currentHealth = currentHealth;
        healthSystem.active = true;
    }

    public void onDespawn()
    {
        healthSystem.active = false;
    }
    public void chooseAttack()
    {
        int choice = Random.Range(0, 2);
            
        switch (choice){
            case 0:
                attackState = "machineGun";
                break;
            case 1:
                attackState = "circleFire";
                break;
        }
    }
    public void MeleeStart()
    {
      
        hitbox.dealDamagePlayer();
        hitbox.enableHitbox(true);
        hitbox.rotateToDirection(Aim());

    }

    public void MeleeEnd()
    {
        hitbox.enableHitbox(false);
        hitbox.damaged = false;
    }
    IEnumerator SwipeCoroutine()
    {
        MeleeStart();
        yield return new WaitForSeconds(1f);
        MeleeEnd();
    }
    
    public void swipeAction()
    {
        StartCoroutine(SwipeCoroutine());
    }

}
