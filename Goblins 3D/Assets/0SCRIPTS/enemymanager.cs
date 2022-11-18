using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymanager : MonoBehaviour
{
    [SerializeField] private Transform[] enemySpawnPoints;
    private int previousSpawnPoint;

    [SerializeField] private List<GameObject> enemies;
    private GameObject nextEnemy;

    private float maxEnemyResources = 10;
    [Header("Enemy Resources")]
    [SerializeField] private float currentEnemyResources;
    [SerializeField] private float EnemyStartResources;

    [Header("Difficulty Settings")]
    [SerializeField] private float easyResPerSMult;
    [SerializeField] private float easySpawnIntMult;
    [SerializeField] private float hardResPerSMult;
    [SerializeField] private float hardSpawnIntMult;
    private float resourcesPerSMult = 1;
    private float spawnIntervalMult = 1;

    [Header("Stages")]
    [SerializeField] private float stageChangeInterval;
    [SerializeField] private int stageAmount;
    [SerializeField] private float s1ResourcesPerS;
    [SerializeField] private float s1SpawnInterval;
    [SerializeField] private float s2ResourcesPerS;
    [SerializeField] private float s2SpawnInterval;
    [SerializeField] private GameObject[] s2NewEnemies;
    [SerializeField] private float s3ResourcesPerS;
    [SerializeField] private float s3SpawnInterval;
    [SerializeField] private GameObject[] s3NewEnemies;
    [SerializeField] private float s4ResourcesPerS;
    [SerializeField] private float s4SpawnInterval;
    [SerializeField] private GameObject[] s4NewEnemies;
    private float timeBtwSpawns;

    private int stage = 1;

    private float timeBtwStageChanges;

    void Start()
    {
        if (MultiScene.multiScene.difficulty == 0)
        {
            resourcesPerSMult = easyResPerSMult;
            spawnIntervalMult = easySpawnIntMult;
        }
        else if (MultiScene.multiScene.difficulty == 2)
        {
            resourcesPerSMult = hardResPerSMult;
            spawnIntervalMult = hardSpawnIntMult;
        }
        previousSpawnPoint = Random.Range(0, enemySpawnPoints.Length);
        currentEnemyResources = EnemyStartResources;
        timeBtwSpawns = 0;
        PickRandomEnemy();
    }

    void Update()
    {
        if (currentEnemyResources < maxEnemyResources) currentEnemyResources += Time.deltaTime * s1ResourcesPerS * resourcesPerSMult;

        if (timeBtwSpawns >= s1SpawnInterval / spawnIntervalMult)
        {
            SpawnEnemy();
            timeBtwSpawns = 0;
        }
        else timeBtwSpawns += Time.deltaTime;

        if (timeBtwStageChanges >= stageChangeInterval && stage <= stageAmount)
        {
            timeBtwStageChanges = 0;
            NextStage();
        }
        else timeBtwStageChanges += Time.deltaTime;
    }
    void SpawnEnemy()
    {
        float nextEnemyCost = nextEnemy.GetComponent<EnemyUnit>().unitCost;

        if (nextEnemyCost <= currentEnemyResources && gamemanager.state == gamemanager.State.RunTime)
        {
            int randomSpawnPoint = Random.Range(0, enemySpawnPoints.Length);
            while (randomSpawnPoint == previousSpawnPoint) randomSpawnPoint = Random.Range(0, enemySpawnPoints.Length);
            previousSpawnPoint = randomSpawnPoint;
            Instantiate(nextEnemy, enemySpawnPoints[randomSpawnPoint].position, Quaternion.identity);
            currentEnemyResources -= nextEnemyCost;
            PickRandomEnemy();
        }
    }
    void PickRandomEnemy()
    {
        int randomEnemy = Random.Range(0, enemies.Count);
        nextEnemy = enemies[randomEnemy];
    }
    void NextStage()
    {
        stage += 1;

        if (stage == 2)
        {
            s1ResourcesPerS = s2ResourcesPerS * resourcesPerSMult;
            s1SpawnInterval = s2SpawnInterval * spawnIntervalMult;

            foreach (GameObject newEnemy in s2NewEnemies)
            {
                enemies.Add(newEnemy);
            }
        }
        if (stage == 3)
        {
            s1ResourcesPerS = s3ResourcesPerS * resourcesPerSMult;
            s1SpawnInterval = s3SpawnInterval * spawnIntervalMult;

            foreach (GameObject newEnemy in s3NewEnemies)
            {
                enemies.Add(newEnemy);
            }
        }
        if (stage == 4)
        {
            s1ResourcesPerS = s4ResourcesPerS * resourcesPerSMult;
            s1SpawnInterval = s4SpawnInterval * spawnIntervalMult;

            foreach (GameObject newEnemy in s4NewEnemies)
            {
                enemies.Add(newEnemy);
            }
        }
    }
}
