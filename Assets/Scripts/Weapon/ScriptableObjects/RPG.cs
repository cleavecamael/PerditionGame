using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/RPG")]

public class RPG : Weapon
{

    public float baseSplash;
    public bool hasFrag;
    public float baseKnockback;
    public float baseSplashDamage;

    [NonSerialized] public float currentSplash;
    [NonSerialized] public float currentKnockback;
    [NonSerialized] public float currentSplashDamage;

    [Header("For frags")]
    public GameObject subBulletPrefab;
    public int fragCount;
    public int fragDepth;
    public int fragLifetime;

    public override void ResetStats()
    {
        base.ResetStats();
        currentSplash = baseSplash;
        currentKnockback = baseKnockback;
        hasFrag = false;
        currentSplashDamage = baseSplashDamage;
    }
    public override string ShowStats()
    {
        string baseStats = base.ShowStats();
        string stats = string.Format("{0}\nSplash Radius: {1}\nSplash Damage: {2}", baseStats, baseSplash.ToString(), baseSplashDamage.ToString());
        return stats;
    }
}