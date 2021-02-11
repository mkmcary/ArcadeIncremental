using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPCTarget : SPCEnemyTroop
{
    private bool inMotion;
    private float speed = 0.5f;

    // Start is called before the first frame update
    protected override void Start()
    {
        pointValue = 50;
        health = 1;
        inMotion = false;
        base.Start();
    }

    // Update is called once per frame
    protected void Update()
    {
        if(!inMotion)
            StartCoroutine(Move());
    }

    protected virtual IEnumerator Move()
    {
        inMotion = true;

        // start moving down
        rb.velocity = new Vector3(0, -speed, 0);
        yield return new WaitForSeconds(0.5f);

        // stop moving
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.75f);

        // start moving up
        rb.velocity = new Vector3(0, speed, 0);
        yield return new WaitForSeconds(0.5f);

        // stop moving
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.75f);

        inMotion = false;
    }

    protected override void Die()
    {
        GameObject.Destroy(gameObject);
    }
}
