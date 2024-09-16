using UnityEngine;

[CreateAssetMenu(fileName = "PistolDamagePath", menuName = "Weapon Upgrades/Pistol Damage")]
public class PistolDamagePath : WeaponUpgrade
{
    public float damageMultiplier;
    public float bulletLifetimeMultiplier;
    public int additionalAmmoCapacity;
    public float sizeMultiplier;
    public int newPierceCount;

    public override void Apply()
    {
        base.Apply();
        Pistol pistol = weapon as Pistol;
        pistol.currentDamage *= damageMultiplier;
        pistol.currentAmmoCapacity += additionalAmmoCapacity;
        pistol.currentBulletLifetime *= bulletLifetimeMultiplier;
        pistol.currentSize *= sizeMultiplier;
        pistol.currentPierceCount = newPierceCount;
    }
}