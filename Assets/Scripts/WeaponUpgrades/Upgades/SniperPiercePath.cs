using UnityEngine;

[CreateAssetMenu(fileName = "SniperPiercePath", menuName = "Weapon Upgrades/Sniper Pierce")]
public class SniperPiercePath : WeaponUpgrade
{
    public int newPierceCount;
    public float frequencyMultiplier;
    public float damageMultiplier;

    public override void Apply()
    {
        base.Apply();
        Sniper sniper = weapon as Sniper;
        sniper.currentPierceCount = newPierceCount;
        sniper.currentFrequency *= frequencyMultiplier;
        sniper.currentDamage *= damageMultiplier;
    }
}