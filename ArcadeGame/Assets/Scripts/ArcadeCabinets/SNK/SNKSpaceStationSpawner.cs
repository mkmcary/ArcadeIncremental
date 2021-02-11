using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNKSpaceStationSpawner : MonoBehaviour
{
    private const int DIMENSION = 50;
    private const int SECTOR_SIZE = 5;

    public GameObject spaceStationPrefab;

    private int[,] sectorMap;

    private List<SNKSpaceStation> spaceStations;

    // Start is called before the first frame update
    void Start()
    {
        spaceStations = new List<SNKSpaceStation>();
        GenerateMap();
    }

    private void GenerateMap()
    {
        int stations = 0;
        sectorMap = new int[DIMENSION, DIMENSION];
        for (int i = 0; i < DIMENSION; i++)
        {
            if(stations > DIMENSION)
            {
                break;
            }
            for (int j = 0; j < DIMENSION; j++)
            {
                int flip = Random.Range(0, (int) (DIMENSION * 1.2f));
                if(flip == 0)
                {
                    sectorMap[i, j] = 1;
                    stations++;
                }
                else
                {
                    sectorMap[i, j] = 0;
                }
            }
        }
        SpawnSpaceStations();
    }

    public void SpawnSpaceStations()
    {
        for (int i = 0; i < DIMENSION; i++)
        {
            for (int j = 0; j < DIMENSION; j++)
            {
                if(sectorMap[i,j] == 1)
                {
                    int xPos = (j - (DIMENSION / 2)) * SECTOR_SIZE;
                    int yPos = (i - (DIMENSION / 2)) * SECTOR_SIZE;
                    GameObject spaceStation = Instantiate(spaceStationPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    spaceStation.transform.SetParent(transform);
                    spaceStations.Add(spaceStation.GetComponent<SNKSpaceStation>());
                }
            }
        }
    }

    public SNKSpaceStation GetRandomStation()
    {
        int index = Random.Range(0, spaceStations.Count - 1);
        return spaceStations[index];
    }
}
