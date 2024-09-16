using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Shotgun")]
public class Shotgun : Weapon
{
    public int baseBulletCount;
    public float baseMaximumAngle;
    public float baseKnockback;
    public float baseStunDuration;
    public float baseSlowDuration;
    public int basePierceCount;

    [NonSerialized] public int currentBulletCount;
    [NonSerialized] public float currentMaximumAngle;
    [NonSerialized] public float currentKnockback;
    [NonSerialized] public float currentStunDuration;
    [NonSerialized] public float currentSlowDuration;
    [NonSerialized] public int currentPierceCount;
    public override void ResetStats()
    {
        base.ResetStats();
        currentBulletCount = baseBulletCount;
        currentMaximumAngle = baseMaximumAngle;
        currentKnockback = baseKnockback;
        currentStunDuration = baseStunDuration;
        currentSlowDuration = baseSlowDuration;
        currentPierceCount = basePierceCount;
    }
    public override string ShowStats()
    {
        string baseStats = base.ShowStats();
        string stats = string.Format("{0}\nPellet Count: {1}\nSpread Angle: {2}", baseStats, baseBulletCount.ToString(), baseMaximumAngle.ToString());
        return stats;
    }
}