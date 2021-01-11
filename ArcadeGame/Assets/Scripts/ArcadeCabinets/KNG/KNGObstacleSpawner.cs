using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KNGObstacleSpawner : MonoBehaviour
{
    [Header("Rolling Obstacles")]
    public GameObject rollingObstaclePrefab;
    public GameObject fastRollingObstaclePrefab;
    public float chanceForFastObstacle;

    [Header("Falling Obstacles")]
    public GameObject fallingObstaclePrefab;

    [Header("Spawn Interval")]
    public float spawnInterval = 3f;
    public float fallingInterval = 4f;

    // timing
    private float timer;
    private float fallingTimer;

    // game controller
    private KNGGameController gameController;

    private void Start()
    {
        gameController = GameObject.FindObjectOfType<KNGGameController>();
        timer = spawnInterval - 1f;
        fallingTimer = 0f;
    }

    void FixedUpdate()
    {
        if (gameController.IsPlaying)
        {
            timer += Time.fixedDeltaTime;
            fallingTimer += Time.fixedDeltaTime;
            if (timer >= spawnInterval)
            {
                SpawnRollingObstacle();
                timer = 0;
            }
            if(fallingTimer >= fallingInterval)
            {
                SpawnFallingObstacle();
                fallingTimer = 0;
            }
        } 
        else
        {
            timer = spawnInterval - 1f;
        }
    }

    private void SpawnRollingObstacle()
    {
        GameObject prefabToUse = rollingObstaclePrefab;
        if (Random.Range(0f, 1f) < chanceForFastObstacle)
            prefabToUse = fastRollingObstaclePrefab;
        GameObject roller = Instantiate(prefabToUse, transform.position, Quaternion.identity);
        roller.transform.SetParent(transform);
    }

    private void SpawnFallingObstacle()
    {
        GameObject faller = Instantiate(fallingObstaclePrefab, transform.position, Quaternion.identity);
        faller.transform.SetParent(transform);
    }
}
