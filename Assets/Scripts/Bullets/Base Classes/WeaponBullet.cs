using UnityEngine;

public class WeaponBullet : Bullet
{
    [SerializeField] protected Weapon weaponStats;
    // [SerializeField] private FeverMeterScore feverMeterScore;
    // [SerializeField] private GameConstants gameConstants;

    protected virtual void OnEnable()
    {
        CurrentLifetime = weaponStats.currentBulletLifetime;
        Damage = weaponStats.currentDamage;
        base.SetScale(new Vector3(weaponStats.currentSize, weaponStats.currentSize, weaponStats.currentSize));
    }

    void OnDisable()
    {
        if (TryGetComponent<TrailRenderer>(out TrailRenderer trailRenderer)) trailRenderer.Clear();
    }
}