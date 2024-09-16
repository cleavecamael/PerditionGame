using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/LMG")]

public class LMG : Weapon
{
    public float baseSpread;
    [NonSerialized] public float currentSpread;
    public bool spreadAndDamageUpgrade;

    public float customShootDrag;
    public bool hasFrag;
    public int basePierceCount;
    [NonSerialized] public int currentPierceCount;

    [Header("For frags")]
    public GameObject subBulletPrefab;
    public int fragCount;
    public int fragDepth;
    public float fragLifetime;

    public override void ResetStats()
    {
        base.ResetStats();
        currentSpread = baseSpread;
        spreadAndDamageUpgrade = false;
        customShootDrag = -1;
        hasFrag = false;
        currentPierceCount = basePierceCount;
    }
    public override string ShowStats()
    {
        string baseStats = base.ShowStats();
        string stats = string.Format("{0}\nRecoil: {1}", baseStats, baseSpread.ToString());
        return stats;
    }
}