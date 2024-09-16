using TMPro;
using UnityEngine;

public class AmmoUIManager : MonoBehaviour
{
    private Transform weaponTransform;
    public TextMeshProUGUI text;
    public TextMeshProUGUI gunName;


    void Awake()
    {
        weaponTransform = GameObject.Find("Weapons").transform;
    }

    void Update()
    {
        GameObject weapon = GetGameObject();
        if (weapon != null)
        {
            gunName.text = weapon.GetComponent<WeaponController>().weaponType.ToString();
            text.text = "" + weapon.GetComponent<WeaponController>().CurrentAmmo + "/" + weapon.GetComponent<WeaponController>().Weapon.currentAmmoCapacity;
        }


    }

    GameObject GetGameObject()
    {
        foreach (Transform child in weaponTransform)
        {
            if (child.gameObject.activeSelf)
            {
                return child.gameObject;
            }
        }
        return null;
    }
}