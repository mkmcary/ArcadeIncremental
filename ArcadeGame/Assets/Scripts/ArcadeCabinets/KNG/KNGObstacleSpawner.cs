using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KNGObstacleSpawner : MonoBehaviour
{
    public GameObject rollingObstaclePrefab;

    public float spawnInterval = 3f;

    private float timer;

    private void Start()
    {
        timer = 0;
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if(timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0;
        }
    }

    private void SpawnObstacle()
    {
        GameObject roller = Instantiate(rollingObstaclePrefab, transform.position, Quaternion.identity);
        roller.transform.SetParent(transform);
    }
}
