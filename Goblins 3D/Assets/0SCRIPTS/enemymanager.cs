using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymanager : MonoBehaviour
{
    [SerializeField] private Transform[] enemySpawnPoints;
    [SerializeField] private int previousSpawnPoint;
    [SerializeField] private GameObject[] enemies;
    private GameObject nextEnemy;

    private float maxEnemyResources = 10;
    [SerializeField] private float currentEnemyResources;
    [SerializeField] private float EnemyStartResources;
    [SerializeField] private float resourceGainPerS;
    [SerializeField] private float spawnInterval = 1;
    [SerializeField] private float l2ResourceGainPerS;
    [SerializeField] private float l2SpawnInterval = 0.8f;
    private float timeBtwSpawns;

    [SerializeField] private int stage = 1;
    [SerializeField] private float stageChangeInterval;
    private float timeBtwStageChanges;
    [SerializeField] private int stageAmount;

    void Start()
    {
        previousSpawnPoint = Random.Range(0, enemies.Length);
        currentEnemyResources = EnemyStartResources;
        timeBtwSpawns = 0;
        PickRandomEnemy();
    }

    void Update()
    {
        if (currentEnemyResources < maxEnemyResources) currentEnemyResources += Time.deltaTime * resourceGainPerS;

        if (timeBtwSpawns >= spawnInterval)
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

        if (nextEnemyCost <= currentEnemyResources)
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
        int randomEnemy = Random.Range(0, enemies.Length);
        nextEnemy = enemies[randomEnemy];
    }
    void NextStage()
    {
        stage += 1;

        if (stage == 2)
        {
            resourceGainPerS = l2ResourceGainPerS;
            spawnInterval = l2SpawnInterval;
        }
    }
}
