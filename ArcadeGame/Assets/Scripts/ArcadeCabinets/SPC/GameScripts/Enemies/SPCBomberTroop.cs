using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SPCBomberTroop : SPCEnemyTroop
{
    public GameObject explosionPrefab;
    private bool inMotion;
    private float speed = 1f;

    // Start is called before the first frame update
    protected override void Start()
    {
        // constants
        pointValue = 1000;
        health = 5;
        inMotion = false;

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inMotion)
        {
            StartCoroutine(Move());
        }
    }

    private IEnumerator Move()
    {
        inMotion = true;

        // start moving
        rb.velocity = new Vector3(0, -speed, 0);
        yield return new WaitForSeconds(0.5f);

        // stop moving
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.75f);

        inMotion = false;
    }

    protected override void Die()
    {
        // spawn explosion
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // destroy
        GameObject.Destroy(gameObject);
    }
}
