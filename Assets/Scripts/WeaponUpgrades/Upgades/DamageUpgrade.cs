using UnityEngine;

[CreateAssetMenu(fileName = "DamageUpgrade", menuName = "Weapon Upgrades/Damage")]
public class DamageUpgrade : WeaponUpgrade
{
    public float damageMultiplier;

    public override void Apply()
    {
        base.Apply();
        weapon.currentDamage *= damageMultiplier;
    }
}
