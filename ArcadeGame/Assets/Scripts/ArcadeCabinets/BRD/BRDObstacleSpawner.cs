using System.Collections.Generic;
using UnityEngine;

public class BRDObstacleSpawner : MonoBehaviour
{
    // Spawn Locations
    public Transform groundSpawn;
    public Transform ceilingSpawn;

    // Obstacle Prefabs
    public List<GameObject> spikeObstacles;
    public List<GameObject> wallObstacles;
    public List<GameObject> topSpikedWallObstacles;

    private float timer = 0;
    private float interval = 3f;

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= interval)
        {
            int topOrBottomRoll = Random.Range(0, 2);
            if (topOrBottomRoll == 0)
            {
                //StartCoroutine(SpawnObstacles(groundSpawn.position, false));
                SpawnObstacle(groundSpawn.position);
            } else if (topOrBottomRoll == 1)
            {
                //StartCoroutine(SpawnObstacles(ceilingSpawn.position, true));
                SpawnObstacle(ceilingSpawn.position);
            } else
            {
                SpawnObstacle(groundSpawn.position);
                SpawnObstacle(ceilingSpawn.position);
            }

            timer = 0;
        }
    }

    private void SpawnObstacle(Vector3 location)
    {
        bool flipped = location == ceilingSpawn.position;

        int obstacleSelector = Random.Range(0, 4);
        if (obstacleSelector == 0)
        {
            // Creates a spike on either floor or ceiling
            SpawnSpike(location, flipped);
        }
        else if (obstacleSelector == 1)
        {
            // Creates a ceiling and ground spike
            SpawnCeilingAndGroundSpike(location, flipped);
        } 
        else if (obstacleSelector == 2)
        {
            SpawnWall(location, flipped);
        }
        else if(obstacleSelector == 3)
        {
            SpawnTopSpikedWall(location, flipped);
        }
    }


    private void SpawnSpike(Vector3 location, bool flipped)
    {
        int length = Random.Range(0, spikeObstacles.Count);
        GameObject spike = Instantiate(spikeObstacles[length], location, Quaternion.identity);
        if (flipped)
        {
            Vector2 vTemp = spike.transform.localScale;
            vTemp.y *= -1;
            spike.transform.localScale = vTemp;
        }
    }

    private void SpawnCeilingAndGroundSpike(Vector3 location, bool flipped)
    {
        int offsetSpikes = Random.Range(0, 5);
        float topOffset = 0;
        float botOffset = 0;
        if (flipped)
        {
            botOffset = offsetSpikes;
        }
        else
        {
            topOffset = offsetSpikes;
        }
        Vector3 topLoc = ceilingSpawn.position;
        topLoc.x += topOffset;
        Vector3 botLoc = groundSpawn.position;
        botLoc.x += botOffset;
        SpawnSpike(topLoc, true);
        SpawnSpike(botLoc, false);
    }

    private void SpawnWall(Vector3 location, bool flipped)
    {
        int index = Random.Range(0, wallObstacles.Count);
        GameObject wall = Instantiate(wallObstacles[index], location, Quaternion.identity);
        if (flipped)
        {
            Vector2 vTemp = wall.transform.localScale;
            vTemp.y *= -1;
            wall.transform.localScale = vTemp;
        }
    }

    private void SpawnTopSpikedWall(Vector3 location, bool flipped)
    {
        int index = Random.Range(0, topSpikedWallObstacles.Count);
        GameObject spikedWall = Instantiate(topSpikedWallObstacles[index], location, Quaternion.identity);
        if (flipped)
        {
            Vector2 vTemp = spikedWall.transform.localScale;
            vTemp.y *= -1;
            spikedWall.transform.localScale = vTemp;
        }
    }
}
