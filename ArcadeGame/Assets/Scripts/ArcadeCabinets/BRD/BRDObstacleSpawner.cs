using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRDObstacleSpawner : MonoBehaviour
{
    // Spawn Locations
    public Transform groundSpawn;
    public Transform ceilingSpawn;

    // Obstacle Prefabs
    public GameObject spikeObstacle;
    public GameObject wallObstacle;

    private float timer = 0;
    private float interval = 5f;

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if(timer >= interval)
        {
            int coinFlip = Random.Range(0, 2);
            if(coinFlip == 0)
            {
                StartCoroutine(SpawnObstacles(groundSpawn.position, false));
            } else
            {
                StartCoroutine(SpawnObstacles(ceilingSpawn.position, true));
            }
            
            timer = 0;
        }
    }

    private IEnumerator SpawnObstacles(Vector3 location, bool flipped)
    {
        int obstacleSelector = Random.Range(0, 4);
        if(obstacleSelector == 0)
        {
            // Creates a spike on either floor or ceiling
            int numberOfSpikes = Random.Range(1, 10);
            for (int i = 0; i < numberOfSpikes; i++)
            {
                SpawnSpike(location, flipped);
                yield return new WaitForSeconds(0.2f);
            }
        }
        else if(obstacleSelector == 1)
        {
            // Creates a ceiling and ground spike
            int numberOfTopSpikes = Random.Range(1, 10);
            int numberOfBottomSpikes = Random.Range(1, 10);
            int maxSpikes = Mathf.Max(numberOfTopSpikes, numberOfBottomSpikes);
            int offsetSpikes = Random.Range(0,maxSpikes);
            int topOffset = 0;
            int botOffset = 0;
            if (flipped)
            {
                numberOfBottomSpikes -= offsetSpikes;
                botOffset = offsetSpikes;
            } else
            {
                numberOfTopSpikes -= offsetSpikes;
                topOffset = offsetSpikes;
            }
            for (int i = 0; i < maxSpikes; i++)
            {
                if(i < numberOfTopSpikes && i >= topOffset)
                {
                    SpawnSpike(ceilingSpawn.position, true);
                }
                if(i < numberOfBottomSpikes && i >= botOffset)
                {
                    SpawnSpike(groundSpawn.position, false);
                }
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    private void SpawnSpike(Vector3 location, bool flipped)
    {
        GameObject spike = Instantiate(spikeObstacle, location, Quaternion.identity);
        if (flipped)
        {
            Vector2 vTemp = spike.transform.localScale;
            vTemp.y *= -1;
            spike.transform.localScale = vTemp;
        }
    }

    private void SpawnWall(Vector3 location, bool flipped)
    {

    }
}
