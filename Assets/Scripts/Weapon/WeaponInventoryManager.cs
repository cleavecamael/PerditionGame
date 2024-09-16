using System.Collections.Generic;
using UnityEngine;

public class WeaponInventoryManager : MonoBehaviour
{
    [SerializeField] private WeaponsList masterWeaponsList;
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameConstants gameConstants;

    List<GameObject> equippedWeapons = new List<GameObject>();
    GameObject activeWeapon;

    [SerializeField] private Weapon startingWeapon;
    // [SerializeField] private Weapon otherWeapon;

    void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    public void ResetInventory()
    {
        inventory.ResetInventory();
        AddWeapon(startingWeapon, starting: true);
        // try
        // {
        //     AddWeapon(otherWeapon, starting: false);
        // }
        // catch (System.Exception)
        // {
        //     Debug.Log("empty second weapon");
        // }
    }

    public void InitializeInventory()
    {
        foreach (Weapon weapon in inventory.weapons)
        {
            AddWeapon(weapon);
        }
        equippedWeapons[0].SetActive(true);
        activeWeapon = equippedWeapons[0];
    }

    public void AddWeaponResponse(Weapon weapon)
    {
        AddWeapon(weapon);
    }

    public void AddWeapon(Weapon weapon, bool starting = false)
    {
        if (equippedWeapons.Count > gameConstants.maxInventorySize) Debug.LogError("Trying to add weapon when inventory is full");

        GameObject newWeapon = Instantiate(weapon.prefab, transform);
        newWeapon.transform.localPosition = Vector3.zero;
        if (!starting)
        {
            activeWeapon.SetActive(false);
        }
        newWeapon.SetActive(true);
        activeWeapon = newWeapon;
        equippedWeapons.Add(activeWeapon);
        inventory.AddWeapon(weapon);
    }

    public void SwapToWeapon(int i)
    {
        if (i <= equippedWeapons.Count && activeWeapon != equippedWeapons[i - 1])
        {
            activeWeapon.SetActive(false);
            activeWeapon = equippedWeapons[i - 1];
            activeWeapon.SetActive(true);
        }
    }
}