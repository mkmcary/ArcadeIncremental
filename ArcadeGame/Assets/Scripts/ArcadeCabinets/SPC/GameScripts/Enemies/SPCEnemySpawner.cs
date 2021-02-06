using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPCEnemySpawner : MonoBehaviour
{
    private bool waveComplete = true;

    public GameObject stdTroopPrefab;
    public GameObject hvyTroopPrefab;
    public GameObject snpTroopPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if(waveComplete)
        {
            SpawnNewWave();
        }
    }

    private void SpawnNewWave()
    {

    }
}
