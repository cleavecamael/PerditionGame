using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "ScriptableObjects/WeaponStats", order = 5)]
public class Weapon : ScriptableObject, ILevelUpOption
{
    [Header("Details")]
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;
    [SerializeField] protected string _futureUpgrades;
    [SerializeField] protected Sprite _icon;
    public GameObject prefab;

    [Header("Stats")]
    public float baseFrequency;
    public float baseDamage;
    public float baseBulletSpeed;
    public float gunSpriteOffset;
    public float baseReloadTime;
    public float baseAmmoCapacity;
    public WeaponType weaponType;
    public float reloadSpinSpeed; // In degrees per second
    public float baseBulletLifetime;
    public float baseSize;
    public Vector2 shakeParameters; // x is amount y is duration

    [Header("Weapon Upgrade")]
    public WeaponUpgradePath upgradePath;

    public string Name
    {
        get
        {
            return _name;
        }
    }

    public string Description
    {
        get
        {
            return _description;
        }
    }
    public string futureUpgrades
    {
        get
        {
            return _futureUpgrades;
        }
    }

    public Sprite Icon
    {
        get
        {
            return _icon;
        }
    }


    // Modifiable during runs
    [NonSerialized] public float currentDamage;
    [NonSerialized] public float currentFrequency;
    [NonSerialized] public float currentAmmoCapacity;
    [NonSerialized] public float currentReloadTime;
    [NonSerialized] public float currentBulletLifetime;
    [NonSerialized] public float currentBulletSpeed;
    [NonSerialized] public float currentSize;
    [NonSerialized] public List<WeaponUpgrade> currentUpgrades;

    void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    public virtual void ResetStats()
    {
        currentDamage = baseDamage;
        currentFrequency = baseFrequency;
        currentAmmoCapacity = baseAmmoCapacity;
        currentReloadTime = baseReloadTime;
        currentBulletLifetime = baseBulletLifetime;
        currentBulletSpeed = baseBulletSpeed;
        currentSize = baseSize;
        currentUpgrades = new List<WeaponUpgrade>();
    }

    public void AddUpgrade(WeaponUpgrade upgrade)
    {
        currentUpgrades.Add(upgrade);
    }

    public virtual string ShowStats()
    {
        string stats = string.Format("Damage: {0}\nFire rate: {1}\nMagazine: {2}\nReload Speed: {3}\nRange: {4}\nBullet Speed: {5}", baseDamage.ToString(), baseFrequency.ToString(), baseAmmoCapacity.ToString(), baseReloadTime.ToString(), baseBulletLifetime.ToString(), baseBulletSpeed.ToString());
        return stats;
    }
    public string ShowUpgradeStats()
    {
        string upgradeStats = string.Format("Damage: {0}\nFire rate: {1}\nReload Speed: {2}\nRange: {3}", baseDamage.ToString(), baseFrequency.ToShortString(), baseReloadTime.ToString(), baseBulletLifetime.ToString());
        return upgradeStats;
    }
}