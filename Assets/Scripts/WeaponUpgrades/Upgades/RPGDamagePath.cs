using UnityEngine;

[CreateAssetMenu(fileName = "RPGDamagePath", menuName = "Weapon Upgrades/RPG Damage")]
public class RPGDamagePath : WeaponUpgrade
{
    public float splashMultiplier;
    public float damageMultiplier;
    public bool hasFrag;
    public float splashDamageMultiplier;

    public override void Apply()
    {
        base.Apply();
        RPG rpg = weapon as RPG;
        rpg.currentSplash *= splashMultiplier;
        rpg.currentDamage *= damageMultiplier;
        rpg.hasFrag = hasFrag;
        rpg.currentSplashDamage *= splashDamageMultiplier;
    }
}
