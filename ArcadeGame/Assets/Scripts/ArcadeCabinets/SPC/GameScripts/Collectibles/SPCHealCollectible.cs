using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPCHealCollectible : SPCCollectible
{
    SPCBarrierController barrierController;

    protected override void Start()
    {
        base.Start();
        barrierController = GameObject.FindObjectOfType<SPCBarrierController>();
    }

    protected override void Collect()
    {
        barrierController.SpawnBarriers();
        GameObject.Destroy(gameObject);
    }
}
