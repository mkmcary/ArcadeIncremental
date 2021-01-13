using System.Collections.Generic;
using UnityEngine;

public class BRDObstacleSpawner : MonoBehaviour
{
    private BRDGameController gameController;

    // Spawn Locations
    public Transform groundSpawn;
    public Transform ceilingSpawn;

    // Obstacle Prefabs
    public List<GameObject> spikeObstacles;
    public List<GameObject> wallObstacles;
    public List<GameObject> topSpikedWallObstacles;
    public GameObject bonusPoints;
    public GameObject finishLine;

    private bool spawnPoints = false;

    private float timer = 0;
    private float interval = 1.5f;

    private void Start()
    {
        gameController = GameObject.FindObjectOfType<BRDGameController>();
    }

    private void FixedUpdate()
    {
        if (!gameController.isPlaying)
        {
            return;
        }

        timer += Time.fixedDeltaTime;
        if (timer >= interval && gameController.isSpawning)
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

            if (gameController.winCondition)
            {
                gameController.isSpawning = false;
                SpawnFinishLine();
            }

            timer = 0;
        }
    }

    // Could be beneficial to refactor parent setting to this method.
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
        float randomHeight = Random.Range(1, 5);
        SpawnBonusPoints(location, flipped, randomHeight + .5f , length / 2);
        if (flipped)
        {
            Vector2 vTemp = spike.transform.localScale;
            vTemp.y *= -1;
            spike.transform.localScale = vTemp;
        }

        spike.transform.SetParent(transform);
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
        SpawnBonusPoints(location, flipped, index, 2);
        if (flipped)
        {
            Vector2 vTemp = wall.transform.localScale;
            vTemp.y *= -1;
            wall.transform.localScale = vTemp;
        }
        wall.transform.SetParent(transform);
    }

    private void SpawnTopSpikedWall(Vector3 location, bool flipped)
    {
        int index = Random.Range(0, topSpikedWallObstacles.Count);
        GameObject spikedWall = Instantiate(topSpikedWallObstacles[index], location, Quaternion.identity);
        SpawnBonusPoints(location, flipped, index + 3, 0);
        if (flipped)
        {
            Vector2 vTemp = spikedWall.transform.localScale;
            vTemp.y *= -1;
            spikedWall.transform.localScale = vTemp;
        }
        spikedWall.transform.SetParent(transform);
    }

    private void SpawnBonusPoints(Vector3 location, bool flipped, float heightOffset, float xOffset)
    {
        if (spawnPoints)
        {
            heightOffset += .5f;
            if (flipped)
            {
                heightOffset *= -1;
                xOffset *= -1;
            }
            Vector2 temp = location;
            temp.y += heightOffset;
            temp.x += xOffset;
            location = temp;
            GameObject bonus = Instantiate(bonusPoints, location, Quaternion.identity);
            bonus.transform.SetParent(transform);
        }
        spawnPoints = !spawnPoints;
    }

    private void SpawnFinishLine()
    {
        Vector3 spawn = groundSpawn.position;
        spawn.x += 10;
        spawn.y = 0;
        Instantiate(finishLine, spawn, Quaternion.identity);
    }
}
