using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPCTripleShotCollectible : SPCCollectible
{
    SPCPlayerController player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindObjectOfType<SPCPlayerController>();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 15f * Time.deltaTime));
    }

    protected override void Collect()
    {
        player.FiringMode = SPCPlayerController.FireMode.TRIPLE;
        GameObject.Destroy(gameObject);
    }
}
