using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
   public levelStats[] levels;
    [System.Serializable]
    public struct levelStats
    {
        public float health;
        public float speed;
        public int damage;
        public float feverGiven;
        public int xpGiven;
        public float damageFreq;
    }

    public levelStats getLevelStats(int level)
    {

        if (level > levels.Length)
        {
            return levels[levels.Length - 1];
        }
        return levels[level - 1];
    }
    
}