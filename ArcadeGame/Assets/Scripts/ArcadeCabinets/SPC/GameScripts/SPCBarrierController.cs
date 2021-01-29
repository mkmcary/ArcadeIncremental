using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPCBarrierController : MonoBehaviour
{
    // maybe make this a list with different tiers?
    public GameObject barrierPrefab;

    public int numBarriers = 3;
    public float extremeX = 10;
    public float yPos = -1f;

    private List<GameObject> barriers;

    // Start is called before the first frame update
    void Start()
    {
        barriers = new List<GameObject>();
        SpawnBarriers();
    }

    public void SpawnBarriers()
    {
        DestroyBarriers();

        float separation = (2 * extremeX) / (numBarriers - 1);

        Vector3 spawnPos = new Vector3(-extremeX, yPos, 0);
        for (int i = 0; i < numBarriers; i++)
        {
            GameObject bar = Instantiate(barrierPrefab, spawnPos, Quaternion.identity);
            bar.transform.SetParent(transform);

            barriers.Add(bar);

            spawnPos.x += separation;
        }
    }

    public void DestroyBarriers()
    {
        for(int i = 0; i < barriers.Count; i++)
        {
            GameObject.Destroy(barriers[i]);
        }
    }
}
