using UnityEngine;

[CreateAssetMenu(fileName = "LMGBulletSpamPath", menuName = "Weapon Upgrades/LMG Bullet Spam")]
public class LMGBulletSpamPath : WeaponUpgrade
{
    public float frequencyMultiplier;
    public int additionalAmmoCapacity;
    public bool spreadAndDamageUpgrade;
    public float damageMultiplier;
    public bool hasFrag;

    public override void Apply()
    {
        base.Apply();
        LMG lmg = weapon as LMG;
        lmg.currentFrequency *= frequencyMultiplier;
        lmg.currentAmmoCapacity += additionalAmmoCapacity;
        lmg.spreadAndDamageUpgrade = spreadAndDamageUpgrade;
        lmg.currentDamage *= damageMultiplier;
        lmg.hasFrag = hasFrag;
    }
}
