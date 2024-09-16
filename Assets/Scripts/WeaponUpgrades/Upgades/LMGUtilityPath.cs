using UnityEngine;

[CreateAssetMenu(fileName = "LMGUtilityPath", menuName = "Weapon Upgrades/LMG Utility")]
public class LMGUtilityPath : WeaponUpgrade
{
    public float reloadTimeMultiplier;
    public int additionalAmmoCapacity;
    public float newSpread;
    public DragUpgrade dragUpgrade;
    public float damageMultiplier;

    public override void Apply()
    {
        base.Apply();
        LMG lmg = weapon as LMG;
        lmg.currentReloadTime *= reloadTimeMultiplier;
        lmg.currentSpread = newSpread;
        lmg.currentAmmoCapacity += additionalAmmoCapacity;
        if (dragUpgrade.active) lmg.customShootDrag = dragUpgrade.drag;
        lmg.currentDamage *= damageMultiplier;
    }
}
