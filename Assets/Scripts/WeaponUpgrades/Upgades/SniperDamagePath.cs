using UnityEngine;

[CreateAssetMenu(fileName = "SniperDamagePath", menuName = "Weapon Upgrades/Sniper Damage")]
public class SniperDamagePath : WeaponUpgrade
{
    public float frequencyMultiplier;
    public int additionalAmmoCapacity;
    public float sizeMultiplier;
    public float damageMultiplier;

    public override void Apply()
    {
        base.Apply();
        Sniper sniper = weapon as Sniper;
        sniper.currentFrequency *= frequencyMultiplier;
        sniper.currentAmmoCapacity += additionalAmmoCapacity;
        sniper.currentSize *= sizeMultiplier;
        sniper.currentDamage *= damageMultiplier;
    }
}