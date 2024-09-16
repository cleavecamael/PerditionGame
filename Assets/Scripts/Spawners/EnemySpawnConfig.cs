using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObjects/EnemySpawnConfig")]
public class EnemySpawnConfig : ScriptableObject
{
    [Serializable]
    public struct EnemySpawnConfigContainer
    {
        public float spawnInterval;
        public int spawnLimit;
    }

    public EnemySpawnConfigContainer[] spawnConfig;

    public float GetSpawnInterval(int lvl)
    {
        if (lvl - 1 < spawnConfig.Length) return spawnConfig[lvl - 1].spawnInterval;
        else return spawnConfig[^1].spawnInterval;
    }

    public int GetSpawnLimit(int lvl)
    {
        if (lvl-1 < spawnConfig.Length) return spawnConfig[lvl - 1].spawnLimit;
        else return spawnConfig[^1].spawnLimit;
    }

}