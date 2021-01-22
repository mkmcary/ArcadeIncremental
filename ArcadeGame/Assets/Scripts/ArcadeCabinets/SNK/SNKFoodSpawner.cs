using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNKFoodSpawner : MonoBehaviour
{
    public GameObject asteroidFood;

    private int foodCap;
    public int foodCount;

    // Start is called before the first frame update
    void Start()
    {
        foodCap = 5;
        foodCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(foodCap > foodCount)
        {
            SpawnFood();
        }
    }

    private void SpawnFood()
    {
        int xSpawn = Random.Range(-10, 10);
        int ySpawn = Random.Range(-10, 10);

        GameObject newFood = Instantiate(asteroidFood, new Vector3(xSpawn, ySpawn, 10), Quaternion.identity);
        newFood.transform.SetParent(transform);
        foodCount++;
    }
}
