using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Sniper")]

public class Sniper : Weapon
{
    public int basePierceCount;
    [NonSerialized] public int currentPierceCount;
    public override void ResetStats()
    {
        base.ResetStats();
        currentPierceCount = basePierceCount;
    }
    public override string ShowStats()
    {
        string baseStats = base.ShowStats();
        string stats = string.Format("{0}\nPierce Count: {1}", baseStats, basePierceCount.ToString());
        return stats;
    }
}