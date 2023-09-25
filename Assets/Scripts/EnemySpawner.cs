using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject canvas;
    public GameObject enemyPrefab; // Drag and drop the enemy prefab here in the Unity Editor
    public Vector2 minSpawnPosition; // Define the minimum x and y coordinates for spawning
    public Vector2 maxSpawnPosition; // Define the maximum x and y coordinates for spawning
    public int initialEnemyCount = 4;
    public float timeBetweenWaves = 30f; // Time in seconds between waves
    public float timeToDifficulty = 20f;
    public int minWave = 1;
    public int maxWave = 5;
    private float timer = 0;
    public int levelInt = 1;
    public string level = "Wave 1";
    public TMP_Text wave;
    public TMP_Text explain;
    public float shootRate = 2;
    public float chaseSpeed = 2;

    private void Start()
    {
        SpawnEnemies(initialEnemyCount);
        StartCoroutine(SpawnWave());
    }

    void Update(){
        timer += Time.deltaTime;
        if(timer > timeToDifficulty && maxWave < 10){
            levelInt++;
            level = "Wave " + levelInt.ToString();
            StartCoroutine(LevelDisplay());
            // maxWave++;
            // if(maxWave % 2 == 0){
            //     minWave++;
            // }
            timer = 0;
            if(shootRate >= 0.5f){
                shootRate -= 0.3f;
                chaseSpeed += 0.5f;
            }
            
            timeToDifficulty +=10f;
            // if(maxWave == 4)
            //     timeBetweenWaves +=2;
            timeBetweenWaves *= 0.7f;
        }
    }

    private IEnumerator LevelDisplay(){
        TMP_Text display = Instantiate(wave, new Vector3(0f,0f,0f), Quaternion.identity);
        display.transform.SetParent(canvas.transform, false);

        TMP_Text ex = Instantiate(explain, new Vector3(0f,-35f,0f), Quaternion.identity);
        ex.transform.SetParent(canvas.transform, false);

        display.text = level;
        ex.text = "Enemy Spawn Rate + \nFuel Consumption + \nEnemy Aggression +";
        ex.color = Color.red;
        ex.fontSize = 10;
        yield return new WaitForSeconds(3f);
        Destroy(display);
        Destroy(ex);
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

