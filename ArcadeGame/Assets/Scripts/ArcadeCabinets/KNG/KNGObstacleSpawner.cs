using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KNGObstacleSpawner : MonoBehaviour
{
    public GameObject rollingObstaclePrefab;

    public float spawnInterval = 3f;

    private float timer;
    private KNGGameController gameController;

    private void Start()
    {
        gameController = GameObject.FindObjectOfType<KNGGameController>();
        timer = spawnInterval - 1f;
    }

    void FixedUpdate()
    {
        if (gameController.IsPlaying)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= spawnInterval)
            {
                SpawnObstacle();
                timer = 0;
            }
        } 
        else
        {
            timer = spawnInterval - 1f;
        }
    }

    private void SpawnObstacle()
    {
        GameObject roller = Instantiate(rollingObstaclePrefab, transform.position, Quaternion.identity);
        roller.transform.SetParent(transform);
    }
}
