using System;
using System.Collections;
using UnityEngine;
using static EnemySpawnerFunctions;
//manage spawning events
public class EnemySpawnManager : MonoBehaviour
{
    private bool spawning = false;
    private int enemyCount;
    [SerializeField] private enemyWeight[] enemyWeights;
    [SerializeField] private XPSystem xpLevel;
    [SerializeField] int levelScale;
    [SerializeField] int enemyLevelIncreaseInterval;
    EnemySpawnerFunctions spawner;
    public Vector3Position playerPosition;
    public string bossId;
    float spawnInterval;
    int spawnLimit;

    [Header("Spawn configurations")]
    [SerializeField] private EnemySpawnConfig enemySpawnConfigs;


    void Start()
    {
        SetSpawnConfig(xpLevel.CurrentLevel);
        setEnemyCount(0);
        spawner = GetComponent<EnemySpawnerFunctions>();
        StartCoroutine(autoSpawner());
        StartCoroutine(updateLevel());
        spawning = true;

    }


    void spawnEnemy(string id)
    {
        if ((enemyCount < spawnLimit || id == "BossGolem" || id == "Dragon") && spawning)
        {
            Vector2 spawnPosition = spawner.findSpawnPointCircle(50, playerPosition.pos);
            while (spawnPosition == new Vector2(999f, 999f))
            {
                spawnPosition = spawner.findSpawnPointCircle(50, playerPosition.pos);
            }
            spawner.spawn(id, spawnPosition, levelScale + 1);
            incrementEnemyCount(1);
            // Debug.Log("spawnEnemy " + enemyCount);
        }
    }

    IEnumerator updateLevel()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemyLevelIncreaseInterval);

            levelScale = levelScale + 1;
        }


    }
    void spawnBoss()
    {
        spawnEnemy(bossId);
        spawning = false;
    }

    public void levelUpHandler()
    {
        SetSpawnConfig(xpLevel.CurrentLevel);
        if (xpLevel.CurrentLevel == xpLevel.CurrentLevelCap)
        {
            spawnBoss();
            spawning = false;
        }
    }

    void SetSpawnConfig(int lvl)
    {
        spawnInterval = enemySpawnConfigs.GetSpawnInterval(lvl);
        spawnLimit = enemySpawnConfigs.GetSpawnLimit(lvl);
    }

    public void setEnemyCount(int value)
    {
        enemyCount = value;
    }

    public void incrementEnemyCount(int value)
    {
        enemyCount += value;
    }

    IEnumerator autoSpawner()
    {
        yield return new WaitForSeconds(spawnInterval);
        Vector2 spawnPosition = spawner.findSpawnPointCircle(30, playerPosition.pos);
        string pickedEnemy = spawner.getRandomEnemy(enemyWeights);
        spawnEnemy(pickedEnemy);
        StartCoroutine(autoSpawner());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
