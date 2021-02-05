using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SPCStandardTroop : SPCEnemyTroop
{
    public GameObject projectilePrefab;
    private float speed = 1f;
    private bool inMotion;

    // Start is called before the first frame update
    protected override void Start()
    {
        // constants
        pointValue = 300;
        health = 1;

        rb = GetComponent<Rigidbody2D>();
        inMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inMotion)
        {
            StartCoroutine(MoveAndShoot());
        }
    }

    private IEnumerator MoveAndShoot()
    {
        inMotion = true;

        // start moving
        rb.velocity = new Vector3(0, -speed, 0);
        yield return new WaitForSeconds(0.5f);
        
        // stop moving
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.75f);

        // shoot and wait
        Shoot();
        yield return new WaitForSeconds(0.75f);

        inMotion = false;
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        SPCProjectile proj = projectile.GetComponent<SPCProjectile>();
        proj.StartMoving(0f, -5f, Color.green);
        proj.PlayerProjectile = false;
    }

    protected override void Die()
    {
        GameObject.Destroy(gameObject);
    }
}
