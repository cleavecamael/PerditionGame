using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponUpgradeManager : MonoBehaviour
{
    [SerializeField] private AllWeaponUpgrades allWeaponUpgrades;
    [SerializeField] private WeaponsList inventory;
    [SerializeField] private WeaponsList masterWeaponsList;
    [SerializeField] private GameConstants gameConstants;

    public void ResetUpgrades()
    {
        allWeaponUpgrades.ResetOptions();
    }

    public void OnUpgradeSelected(WeaponUpgrade weaponUpgrade)
    {
        allWeaponUpgrades.SetUnavailable(weaponUpgrade);
    }

    public List<ILevelUpOption> GetLevelUpOptions()
    {
        List<ILevelUpOption> options = new List<ILevelUpOption>();

        if (inventory.Size < gameConstants.maxInventorySize) options.AddRange(GetAvailableWeapons().Cast<ILevelUpOption>().ToList());

        foreach (Weapon weapon in inventory.weapons)
        {
            options.AddRange(GetAvailableUpgradesForWeapon(weapon).Cast<ILevelUpOption>().ToList());
        }

        return options;
    }
    private List<Weapon> GetAvailableWeapons()
    {
        return masterWeaponsList.weapons.Where(weapon =>
            !inventory.weapons.Contains(weapon)
        ).ToList();
    }

    private List<WeaponUpgrade> GetAvailableUpgradesForWeapon(Weapon weapon)
    {
        return allWeaponUpgrades.upgradeOptions.Where(option =>
            option.available && option.weaponUpgrade.weapon.weaponType == weapon.weaponType && IsPrerequisiteFulfilled(option.weaponUpgrade, weapon)
        ).Select(option => option.weaponUpgrade).ToList();
    }

    private bool IsPrerequisiteFulfilled(WeaponUpgrade upgrade, Weapon weapon)
    {
        if (upgrade.prerequisite == null)
        {
            return true;
        }

        return weapon.currentUpgrades.Contains(upgrade.prerequisite);
    }
}
