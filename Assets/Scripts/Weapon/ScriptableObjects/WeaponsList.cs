using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Weapons/AllWeapons")]
public class WeaponsList : ScriptableObject
{
    public int Size
    {
        get
        {
            return weapons.Count;
        }
    }

    public List<Weapon> weapons;
}