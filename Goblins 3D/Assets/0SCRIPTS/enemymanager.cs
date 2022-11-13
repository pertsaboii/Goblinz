using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymanager : MonoBehaviour
{
    [SerializeField] private Transform[] enemySpawnPoints;
    private int previousSpawnPoint;

    [SerializeField] private GameObject[] enemies;
    private GameObject nextEnemy;

    private float maxEnemyResources = 10;
    [Header("Enemy Resources")]
    [SerializeField] private float currentEnemyResources;
    [SerializeField] private float EnemyStartResources;

    [Header("Stages")]
    [SerializeField] private float stageChangeInterval;
    [SerializeField] private int stageAmount;
    [SerializeField] private float s1ResourcesPerS;
    [SerializeField] private float s1SpawnInterval;
    [SerializeField] private float s2ResourcesPerS;
    [SerializeField] private float s2SpawnInterval;
    [SerializeField] private float s3ResourcesPerS;
    [SerializeField] private float s3SpawnInterval;
    private float timeBtwSpawns;

    private int stage = 1;

    private float timeBtwStageChanges;

    void Start()
    {
        previousSpawnPoint = Random.Range(0, enemies.Length);
        currentEnemyResources = EnemyStartResources;
        timeBtwSpawns = 0;
        PickRandomEnemy();
    }

    void Update()
    {
        if (currentEnemyResources < maxEnemyResources) currentEnemyResources += Time.deltaTime * s1ResourcesPerS;

        if (timeBtwSpawns >= s1SpawnInterval)
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
            s1ResourcesPerS = s2ResourcesPerS;
            s1SpawnInterval = s2SpawnInterval;
        }
        if (stage == 3)
        {
            s1ResourcesPerS = s3ResourcesPerS;
            s1SpawnInterval = s3SpawnInterval;
        }
    }
}
