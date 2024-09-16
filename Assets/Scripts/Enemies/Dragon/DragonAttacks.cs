using System.Collections;
using UnityEngine;

public class DragonAttacks : BaseEnemyAttacks
{
    public GameObject machineGunPattern;
    public GameObject splitFirePattern;
    public LaserController laser;
    public GameObject sprayPattern;


    // Start is called before the first frame update
    public void Start()
    {

        attacks.Add(new EnemyAttack("machineGun", 1, machineGunCue, machineGunAttack, null, machineGunValid));
        attacks.Add(new EnemyAttack("swipe", 0, swipeCue, swipeAttack, null, swipeValid));
        attacks.Add(new EnemyAttack("splitFire", 1, splitFireCue, splitFireAttack, null, splitFireValid));
        attacks.Add(new EnemyAttack("laser", 0, laserCue, laserAttack, laserFinish, laserValid));
        attacks.Add(new EnemyAttack("spray", 1, sprayCue, sprayAttack, sprayFinish, sprayValid));
    }



    public void machineGunCue(BaseEnemyController controller)
    {

    }
    public void machineGunAttack(BaseEnemyController controller)
    {

        machineGunPattern.GetComponent<AttackPattern>().activatePattern(3f);
    }
    public void machineGunFinish(BaseEnemyController controller)
    {

    }
    public bool machineGunValid(BaseEnemyController controller)
    {
        return (getDistance() < 20f);
    }

    public void swipeCue(BaseEnemyController controller)
    {

    }
    public void swipeAttack(BaseEnemyController controller)
    {

        GetComponent<DragonController>().swipeAction();
    }
    public void swipeFinish(BaseEnemyController controller)
    {

    }
    public bool swipeValid(BaseEnemyController controller)
    {
        return (getDistance() < 10f);
    }

    public void splitFireCue(BaseEnemyController controller)
    {

    }
    public void splitFireAttack(BaseEnemyController controller)
    {

        splitFirePattern.GetComponent<AttackPattern>().activatePattern(4f);
    }
    public void splitFireFinish(BaseEnemyController controller)
    {

    }
    public bool splitFireValid(BaseEnemyController controller)
    {
        return (getDistance() < 20f);
    }
    public void laserCue(BaseEnemyController controller)
    {
        Debug.Log("lasering");
        laser.adjustHead();
        laser.activateLaser();

    }
    public void laserAttack(BaseEnemyController controller)
    {
        StartCoroutine(rotateLaser());

    }
    public void laserFinish(BaseEnemyController controller)
    {
        laser.deactivateLaser();
    }
    public bool laserValid(BaseEnemyController controller)
    {
        return (getDistance() < 20f);
    }
    IEnumerator rotateLaser()
    {
        int direction = laser.clockwiseToPlayer() ? 1 : -1;
        for (int i = 0; i < 90; i++)
        {
            laser.rotate(1 * direction);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void sprayCue(BaseEnemyController controller)
    {

    }
    public void sprayAttack(BaseEnemyController controller)
    {

        sprayPattern.GetComponent<AttackPattern>().activatePattern(3f);
    }
    public void sprayFinish(BaseEnemyController controller)
    {

    }
    public bool sprayValid(BaseEnemyController controller)
    {
        return (getDistance() < 20f);
    }
}

