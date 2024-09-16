using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
//functions for spawning enemies. 
public class EnemySpawnerFunctions : MonoBehaviour
{
    // Start is called before the first frame update

    //game bounds
    public Troops troopsSO;
    public Transform upperBound;
    public Transform lowerBound;
    public Transform rightBound;
    public Transform leftBound;
    public Vector2 cameraHalfSize;
    public Vector3Position playerPosition;
    public  Dictionary<string, GameObject> troopsMap = new Dictionary<string, GameObject>();
    private float buffer =5.0f;

    void Awake()
    {
       
        Troops.Troop[] troops = troopsSO.troops;
        for (int i = 0; i < troops.Length; i++)
        {
            troopsMap.Add(troops[i].id, troops[i].prefab);
        }
  
        
    }

    public void onSpawnMinion(string minionId)
    {
        onSpawnMinion(minionId, 1);
    }

    public void onSpawnMinion(string minionId, int level)
    {
        Vector2 spawnPosition = findSpawnPointCircle(50, playerPosition.pos);
        while (spawnPosition == new Vector2(999f, 999f))
        {
            spawnPosition = findSpawnPointCircle(50, playerPosition.pos);
        }
        spawn(minionId, spawnPosition, level);
    }

    //find a place to spawn within a circle with a center
    public Vector2 findSpawnPointCircle(float radius, Vector2 center)
    {
       Vector2 spawnPoint =  Random.insideUnitCircle * radius + center;
       int maxRetries = 0;
       while (!checkValidSpawn(spawnPoint) )
        {
            spawnPoint = Random.insideUnitCircle * radius + center;
            maxRetries = maxRetries + 1;
            if (maxRetries > 30)
            {
                //note: this represents a null vector, so do comparisons beforehand
                return new Vector2(999f, 999f);
            }
        }
       // Debug.Log("spawn at" + spawnPoint);
        return spawnPoint;
    }

    public Vector2 findSpawnPointCircleMinion(float radius, Vector2 center)
    {
        Vector2 spawnPoint = Random.insideUnitCircle * radius + center;
        int maxRetries = 0;
        while (!checkValidSpawnMinion(spawnPoint))
        {
            spawnPoint = Random.insideUnitCircle * radius + center;
            maxRetries = maxRetries + 1;
            if (maxRetries > 30)
            {
                //note: this represents a null vector, so do comparisons beforehand
                return new Vector2(999f, 999f);
            }
        }
        // Debug.Log("spawn at" + spawnPoint);
        return spawnPoint;
    }

    
    public bool checkValidSpawnMinion(Vector2 position)
    {
        //check if in any obstacles
        if (checkObstacles(position))
        {
            //Debug.Log("overlap exists");
            return false;

        }
        //check if in map bounds
        if (position.x + buffer > rightBound.position.x || position.x - buffer < leftBound.position.x || buffer + position.y > upperBound.position.y
            || position.y - buffer < lowerBound.position.y)
        {
            //Debug.Log("out of boundss");
            return false;
        }
        Vector3 playerPos = playerPosition.pos;
       
        return true;
    }


        public bool checkValidSpawn(Vector2 position)
    {
        //check if in any obstacles
        if (checkObstacles(position))
        {
            //Debug.Log("overlap exists");
            return false;
            
        }
        //check if in map bounds
        if (position.x + buffer > rightBound.position.x || position.x  - buffer < leftBound.position.x ||buffer +  position.y > upperBound.position.y 
            || position.y - buffer < lowerBound.position.y) 
        {
            //Debug.Log("out of boundss");
            return false;
        }
        Vector3 playerPos = playerPosition.pos;
        //check if spawned in camera
        if ((position.x < playerPos.x + cameraHalfSize.x && position.x > playerPos.x - cameraHalfSize.x )|| 
            (position.y < playerPos.y + cameraHalfSize.y && position.y > playerPos.y - cameraHalfSize.y))
        {
            //Debug.Log("incamera");
            return false;
        }
         return true;
    }

    //probably the worst way to write this, will refactor later. //TODO
    private bool checkObstacles(Vector2 position)
    {
       if (Physics2D.OverlapPoint(position, LayerMask.GetMask("Obstacles")) != null)
        {
            return true;
        }
        if (Physics2D.OverlapPoint(position + Vector2.up * 4, LayerMask.GetMask("Obstacles")) != null)
        {
            return true;
        }
        if (Physics2D.OverlapPoint(position + Vector2.right * 4, LayerMask.GetMask("Obstacles")) != null)
        {
            return true;
        }
        if (Physics2D.OverlapPoint(position + Vector2.left * 4, LayerMask.GetMask("Obstacles")) != null)
        {
            return true;
        }
        if (Physics2D.OverlapPoint(position + Vector2.down * 4, LayerMask.GetMask("Obstacles")) != null)
        {
            return true;
        }
        return false;
    }
    public void spawn(string id, Vector2 position)
    {
        //incrementEnemyCount(1);
        Instantiate(troopsMap[id], position, Quaternion.identity);
    }
    public void spawn(string id, Vector2 position, int level)
    {

        GameObject enemy = Instantiate(troopsMap[id], position, Quaternion.identity);
        enemy.GetComponent<BaseEnemyController>().setLevel(level);
    }
    private int getRandomWeighted(int[] weights)
    {
        // Get the total sum of all the weights.
        int weightSum = 0;
        for (int i = 0; i < weights.Length; ++i)
        {
            weightSum += weights[i];
        }

        // Step through all the possibilities, one by one, checking to see if each one is selected.
        int index = 0;
        int lastIndex = weights.Length - 1;
        while (index < lastIndex)
        {
            // Do a probability check with a likelihood of weights[index] / weightSum.
            if (Random.Range(0, weightSum) < weights[index])
            {
                return index;
            }

            // Remove the last item from the sum of total untested weights and try again.
            weightSum -= weights[index++];
        }

        // No other item was selected, so return very last index.
        return index;
    }
    [Serializable]
    public struct enemyWeight
    {
        public string enemy;
        public int weight;
    }
    public string getRandomEnemy(enemyWeight[] enemyWeights)
    {
        int[] weights = new int[enemyWeights.Length];
        string[] enemies = new string[enemyWeights.Length];
         for (int i = 0; i < enemyWeights.Length; i++)
        {
            enemyWeight currEnemyWeight = enemyWeights[i];
            weights[i] = currEnemyWeight.weight;
            enemies[i] = currEnemyWeight.enemy;
        }
        int enemyId = getRandomWeighted(weights);
        return enemies[enemyId];
}
}
