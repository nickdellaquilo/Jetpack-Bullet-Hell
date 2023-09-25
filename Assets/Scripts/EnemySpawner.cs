using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Drag and drop the enemy prefab here in the Unity Editor
    public Vector2 minSpawnPosition; // Define the minimum x and y coordinates for spawning
    public Vector2 maxSpawnPosition; // Define the maximum x and y coordinates for spawning
    public int initialEnemyCount = 4;
    public float timeBetweenWaves = 30f; // Time in seconds between waves
    public float timeToDifficulty = 20f;
    public int minWave = 1;
    public int maxWave = 5;
    private float timer = 0;
    public string level = "Level 1";
    private int levelInt = 1;

    private void Start()
    {
        SpawnEnemies(initialEnemyCount);
        StartCoroutine(SpawnWave());
    }

    void Update(){
        timer += Time.deltaTime;
        if(timer > timeToDifficulty && maxWave < 10){
            levelInt = 2;
            level = "Level " + levelInt.ToString();
            maxWave++;
            if(maxWave % 2 == 0){
                minWave++;
            }
            timer = 0;
            timeToDifficulty +=10f;
            if(maxWave == 4)
                timeBetweenWaves +=2;
        }
    }


    private void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
                Random.Range(minSpawnPosition.y, maxSpawnPosition.y),
                0); 

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private IEnumerator SpawnWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            SpawnEnemies(Random.Range(minWave, maxWave)); // Increase the number of enemies with each wave
        }
    }
}

