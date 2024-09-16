using UnityEngine;

public class WeaponStatsManager : MonoBehaviour
{
    [SerializeField] private WeaponsList masterWeaponsList;

    public void ResetWeaponStats()
    {
        foreach (Weapon weaponStats in masterWeaponsList.weapons)
        {
            weaponStats.ResetStats();
        }
    }
}