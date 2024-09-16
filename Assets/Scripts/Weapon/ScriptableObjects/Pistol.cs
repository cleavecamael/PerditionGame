using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Pistol")]

public class Pistol : Weapon
{
    public int basePierceCount;
    [NonSerialized] public int currentPierceCount;

    public float customShootDrag;

    public override void ResetStats()
    {
        base.ResetStats();
        customShootDrag = -1;
        currentPierceCount = basePierceCount;
    }
}