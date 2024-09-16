using UnityEngine;

[CreateAssetMenu(fileName = "ShotgunBulletHellPath", menuName = "Weapon Upgrades/Shotgun Bullet Hell")]
public class ShotgunBulletHellPath : WeaponUpgrade
{
    public float lifetimeMultiplier;
    public int newBulletCount;
    public float newMaximumAngle;
    public int additionalAmmoCapacity;
    public float frequencyMultiplier;
    public float damageMultiplier;

    public override void Apply()
    {
        base.Apply();
        Shotgun shotgun = weapon as Shotgun;
        shotgun.currentBulletLifetime *= lifetimeMultiplier;
        shotgun.currentFrequency *= frequencyMultiplier;
        shotgun.currentAmmoCapacity += additionalAmmoCapacity;
        shotgun.currentBulletCount = newBulletCount;
        shotgun.currentMaximumAngle = newMaximumAngle;
        shotgun.currentDamage *= damageMultiplier;
    }
}