using UnityEngine;

public class GolemBossAttacks : BaseEnemyAttacks
{
    public GameObject golemPrefab;
    private Vector3 chargeDirection;
    public bool enraged = false;
    public float chargeSpeed;
    public bool charging = false;
    public GameObject shadowShotShooter;
    public GameObject finalAttackShooter;
    public GameObject ringofDeathShooter;
    public GameObject errors;

    // Start is called before the first frame update
    public void Start()
    {

        attacks.Add(new EnemyAttack("charge", 2, chargeCue, chargeAttack, chargeFinish, chargeValid));
        attacks.Add(new EnemyAttack("spawn", 999, null, spawnAttack, null, spawnValid));
        attacks.Add(new EnemyAttack("final", 2, finalCue, finalAttack, null, finalValid));
        attacks.Add(new EnemyAttack("shadow", 2, shadowCue, shadowAttack, null, shadowValid));
        attacks.Add(new EnemyAttack("ring", 2, ringCue, ringAttack, null, ringValid));

    }

    public void chargeCue(BaseEnemyController controller)
    {
        chargeDirection = controller.Aim();
       
    }

    public void chargeAttack(BaseEnemyController controller)
    {

        controller.GetComponent<Rigidbody2D>().velocity = chargeDirection * chargeSpeed;
        charging = true;

    }
    public void chargeFinish(BaseEnemyController controller)
    {

        charging = false;

    }
    public bool chargeValid(BaseEnemyController controller)
    {
        return getDistance() < 40f;
    }
    // Update is called once per frame
    public void spawnGolem()
    {
        GetComponent<GolemBossController>().spawnedGolems = true;
        Vector2 spawnPosition = transform.position;
        float spawnRadius = 8;
        Vector2 startSpawn = Vector2.up;
        for (int i = 0; i < 6; i++)
        {

            GameObject golem = Instantiate(golemPrefab, (Vector3)spawnPosition + Quaternion.Euler(0, 0, i * 60) * (spawnRadius * (Vector3)startSpawn), Quaternion.identity);
            golem.GetComponent<Animator>().SetTrigger("Spawn");
        }
    }

    public void spawnCue(BaseEnemyController controller)
    {
        AudioManager.playClip(this.GetComponent<AudioSource>(), "Roar");
    }
    public void spawnAttack(BaseEnemyController controller)
    {
        controller.GetComponent<GolemBossAttacks>().spawnGolem();
        Debug.Log("spawnign");
        
        turnEnraged();
    }

    void turnEnraged()
    {
        enraged = true;
    }
    public void spawnFinish(BaseEnemyController controller)
    {

    }
    public bool spawnValid(BaseEnemyController controller)
    {
        GolemBossController enemyController = controller as GolemBossController;
        float distanceFromCenter = transform.position.magnitude;

        return !enemyController.spawnedGolems && controller.currentHealth <= 500 && distanceFromCenter < 40f;

    }
    public void finalCue(BaseEnemyController controller)
    {

        AudioManager.playClip(this.GetComponent<AudioSource>(), "chargeUp");
        errors.GetComponent<ErrorBlink>().startBlink();
    }
    public void finalAttack(BaseEnemyController controller)
    {
        finalAttackShooter.GetComponent<AttackPattern>().activatePattern(5f);
    }
    public void finalFinish(BaseEnemyController controller)
    {

    }
    public bool finalValid(BaseEnemyController controller)
    {

        return controller.currentHealth <= 1000 && getDistance() < 10f;
    }
    public void shadowCue(BaseEnemyController controller)
    {

        AudioManager.playClip("golemCry1");
    }
    public void shadowAttack(BaseEnemyController controller)
    {
        shadowShotShooter.GetComponent<AttackPattern>().activatePattern(3f);
    }
    public void shadowFinish(BaseEnemyController controller)
    {

    }
    public bool shadowValid(BaseEnemyController controller)
    {
        return getDistance() < 15f;
    }
    public void ringCue(BaseEnemyController controller)
    {
        AudioManager.playClip(this.GetComponent<AudioSource>(), "golemCry2");
    }
    public void ringAttack(BaseEnemyController controller)
    {
        ringofDeathShooter.GetComponent<AttackPattern>().activatePattern(3f);
    }
    public void ringFinish(BaseEnemyController controller)
    {

    }
    public bool ringValid(BaseEnemyController controller)
    {
        return !enraged && getDistance() < 10f;
    }
}
