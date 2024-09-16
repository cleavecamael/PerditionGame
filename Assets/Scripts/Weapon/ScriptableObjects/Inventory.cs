using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Weapons/Inventory")]
public class Inventory : WeaponsList
{
    void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    public void ResetInventory()
    {
        weapons = new List<Weapon>();
    }

    public bool AddWeapon(Weapon weapon)
    {
        if (!weapons.Contains(weapon))
        {
            weapons.Add(weapon);
            return true;
        }
        return false;
    }
}