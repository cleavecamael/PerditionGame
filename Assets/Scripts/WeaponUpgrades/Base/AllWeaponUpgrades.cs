using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllWeaponUpgrades", menuName = "Weapon Upgrades/All")]
public class AllWeaponUpgrades : ScriptableObject
{
    public List<UpgradeOption> upgradeOptions;

    void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    [System.Serializable]
    public class UpgradeOption
    {
        public WeaponUpgrade weaponUpgrade;
        public bool available;
    }

    public void ResetOptions()
    {
        foreach (var option in upgradeOptions)
        {
            option.available = true;
        }
    }

    public void SetUnavailable(WeaponUpgrade upgrade)
    {
        foreach (var option in upgradeOptions)
        {
            if (option.weaponUpgrade == upgrade)
            {
                option.available = false;
                break;
            }
        }
    }
}