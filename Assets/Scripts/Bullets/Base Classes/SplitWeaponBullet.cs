
using UnityEngine;

public class SplitWeaponBullet : SplitBullet
{
    [SerializeField] protected Weapon weaponStats;
    // [SerializeField] private FeverMeterScore feverMeterScore;
    // [SerializeField] private GameConstants gameConstants;

    protected virtual void OnEnable()
    {
        CurrentLifetime = weaponStats.currentBulletLifetime;
        Damage = weaponStats.currentDamage;
    }
}