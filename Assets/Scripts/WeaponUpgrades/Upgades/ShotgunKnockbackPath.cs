using UnityEngine;

[CreateAssetMenu(fileName = "ShotgunKnockbackPath", menuName = "Weapon Upgrades/Shotgun Knockback")]
public class ShotgunKnockbackPath : WeaponUpgrade
{
    public float knockbackValue;
    public float stunValue;
    public float slowValue;

    public override void Apply()
    {
        base.Apply();
        Shotgun shotgun = weapon as Shotgun;
        shotgun.currentKnockback = knockbackValue;
        shotgun.currentStunDuration = stunValue;
        shotgun.currentSlowDuration = slowValue;
    }
}