using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponInventoryUI : MonoBehaviour
{

    public Inventory inventory;
    string goldColor = "#FFB915";
    Color selectColor;

    private List<string> spriteList;
    void Awake()
    {
        spriteList = new List<string>();
        if (ColorUtility.TryParseHtmlString(goldColor, out selectColor)) { }
        var WeaponsList = inventory.weapons;
        foreach (Weapon weapon in WeaponsList)
        {
            // Debug.Log(weapon.name);
            OnAddWeapon(weapon);
        }
        // foreach (string i in spriteList)
        // {
        //     Debug.Log(i);
        // }
    }

    public void OnAddWeapon(Weapon weapon)
    {
        spriteList.Add(weapon.name);
        var num = spriteList.Count;
        // Debug.Log(num);
        GameObject child = GameObject.Find("Weapon" + num).gameObject;
        child.GetComponent<Image>().sprite = weapon.Icon;
        OnSelectWeapon(spriteList.Count);
    }
    public void OnSelectWeapon(int weaponIndex)
    {
        // Debug.Log("WEapon INDex" + weaponIndex);
        // Debug.Log("sprite List" + spriteList.Count);
        if (spriteList.Count < weaponIndex)
            return;
        var i = 0;
        foreach (Transform child in transform)
        {
            i++;
            if (i == weaponIndex)
                child.gameObject.GetComponent<Outline>().effectColor = selectColor;
            else
                child.gameObject.GetComponent<Outline>().effectColor = Color.white;
        }
    }
}

