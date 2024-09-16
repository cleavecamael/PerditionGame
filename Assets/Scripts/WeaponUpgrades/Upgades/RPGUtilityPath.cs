using UnityEngine;

[CreateAssetMenu(fileName = "RPGSUtilityPath", menuName = "Weapon Upgrades/RPG Utility")]
public class RPGUtilityPath : WeaponUpgrade
{
    public int additionalAmmoCapacity;
    public float lifetimeMultiplier;
    public float frequencyMultiplier;
    public float newKnockback;

    public override void Apply()
    {
        base.Apply();
        RPG rpg = weapon as RPG;
        rpg.currentAmmoCapacity += additionalAmmoCapacity;
        rpg.currentBulletLifetime *= lifetimeMultiplier;
        rpg.currentFrequency *= frequencyMultiplier;
        rpg.currentKnockback = newKnockback;
    }
}
