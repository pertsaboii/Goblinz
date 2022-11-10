using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymanager : MonoBehaviour
{
    [SerializeField] private Transform[] enemySpawnPoints;
    [SerializeField] private GameObject[] enemies;
    private GameObject nextEnemy;

    private float maxEnemyResources = 10;
    [SerializeField] private float currentEnemyResources;
    [SerializeField] private float EnemyStartResources;
    [SerializeField] private float resourceGainPerS = 1;

    [SerializeField] private float spawnInterval = 1;
    private float timeBtwSpawns;
    void Start()
    {
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
    }
    void SpawnEnemy()
    {
        float nextEnemyCost = nextEnemy.GetComponent<EnemyUnit>().unitCost;

        if (nextEnemyCost <= currentEnemyResources)
        {
            int randomSpawnPoint = Random.Range(0, enemySpawnPoints.Length);
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
}
