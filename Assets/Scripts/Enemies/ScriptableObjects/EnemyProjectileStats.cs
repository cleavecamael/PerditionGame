using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/ProjectileStats")]
public class EnemyProjectileStats : ScriptableObject
{
    public ProjectileLevelStats[] levels;

    [System.Serializable]
    public struct ProjectileLevelStats
    {
        public GameObject projectilePrefab;
        public float projectileSpeed;
        public float projectileLifetime;
    }

    public ProjectileLevelStats GetStats(int level)
    {
        return level > levels.Length ? levels[levels.Length - 1] : levels[level-1];
    }

}