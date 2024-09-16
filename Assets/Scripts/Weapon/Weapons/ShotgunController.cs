using System.Collections.Generic;
using UnityEngine;


public class ShotgunController : WeaponController
{
    protected Shotgun shotgunStats;

    void Awake()
    {
        shotgunStats = weapon as Shotgun;
    }

  
    protected override void Shoot()
    {
        FireCameraShake();
        canShoot = false;
        currentAmmo--;
        base.shootMuzzleFlash.Invoke(true);
        Vector3 startingPos = playerPosition.pos + shootDirection.pos * shotgunStats.gunSpriteOffset;
        List<Vector3> vels = WeaponUtils.GetVelocities(shotgunStats.currentBulletCount, shootDirection.pos, shotgunStats.currentMaximumAngle);
        for (int i = 0; i < shotgunStats.currentBulletCount; i++)
        {
            GameObject b = bulletPooler.GetBullet(startingPos);
            Bullet bullet = b.GetComponent<Bullet>();
            bullet.Fire(vels[i], weapon.currentBulletSpeed);
        }
    }
    protected override void FireAudio()
    {
        AudioManager.playClip(GetComponent<AudioSource>(), "shotgunShot");
    }

    protected override void FireCameraShake()
    {
        shootCameraShake.Invoke(shotgunStats.shakeParameters);
    }
}