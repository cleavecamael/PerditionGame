using UnityEngine;

[CreateAssetMenu(fileName = "PistolSpeedPath", menuName = "Weapon Upgrades/Pistol Speed")]
public class PistolSpeedPath : WeaponUpgrade
{
    public float reloadTimeMultiplier;
    public float bulletSpeedMultiplier;
    public float frequencyMultiplier;
    public int additionalAmmoCapacity;
    public DragUpgrade dragUpgrade;

    public override void Apply()
    {
        base.Apply();
        Pistol pistol = weapon as Pistol;
        pistol.currentReloadTime *= reloadTimeMultiplier;
        pistol.currentBulletSpeed *= bulletSpeedMultiplier;
        pistol.currentFrequency *= frequencyMultiplier;
        pistol.currentAmmoCapacity += additionalAmmoCapacity;
        if (dragUpgrade.active) pistol.customShootDrag = dragUpgrade.drag;
    }
}