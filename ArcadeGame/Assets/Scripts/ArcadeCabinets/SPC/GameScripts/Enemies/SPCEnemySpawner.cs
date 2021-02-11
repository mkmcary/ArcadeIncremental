using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPCEnemySpawner : MonoBehaviour
{
    public GameObject standardTroop;
    public GameObject heavyTroop;
    public GameObject sniperTroop;
    public GameObject bomberTroop;

    // determine whether we should spawn
    public int EnemiesAlive { get; set; }
    private float timer;
    private float waveTime = 15f;

    // spawning constants
    private float extremeX = 15;
    private float minY = 6;
    private float maxY = 8;

    // Start is called before the first frame update
    void Start()
    {
        EnemiesAlive = 0;
        timer = 0f;
        SpawnNewWave();
    }

    private void Update()
    {
        // update our wave timer
        timer += Time.deltaTime;
        
        // check if we should spawn new
        if(timer >= waveTime || EnemiesAlive == 0)
        {
            SpawnNewWave();
        }
    }

    private void SpawnNewWave()
    {
        SpawnStandardWave();
        timer = 0;
    }

    private void SpawnStandardWave()
    {
        // choose number to spawn
        // change these values for min and max
        int numberToSpawn = Random.Range(7, 15);

        // find increment to space out
        float increment = (2 * extremeX) / ((float)(numberToSpawn - 1));

        // spawn enemies
        for(float xPos = -extremeX; xPos <= extremeX; xPos += increment)
        {
            float yPos = Random.Range(minY, maxY);
            GameObject ship = Instantiate(standardTroop, new Vector3(xPos, yPos, 0), Quaternion.identity);
            ship.transform.SetParent(transform);
            EnemiesAlive++;
        }

        // set wave time
        waveTime = 15f;
    }
}
