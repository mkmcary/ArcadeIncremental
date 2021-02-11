using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPCSniperTroop : SPCEnemyTroop
{
    public GameObject projectilePrefab;
    private Transform player;

    private bool isShooting;

    protected override void Start()
    {
        // constants
        pointValue = 300;
        health = 1;

        isShooting = false;
        player = GameObject.FindObjectOfType<SPCPlayerController>().gameObject.transform;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        if (!isShooting)
        {
            StartCoroutine(ShootAtPlayer());
        }
    }

    private void LookAtPlayer()
    {
        Vector3 diff = player.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private IEnumerator ShootAtPlayer()
    {
        isShooting = true;
        yield return new WaitForSeconds(2f);

        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        SPCProjectile proj = projectile.GetComponent<SPCProjectile>();
        proj.StartMoving(15f, Color.green);
        proj.PlayerProjectile = false;
        isShooting = false;
    }

    protected override void Die()
    {
        GameObject.Destroy(gameObject);
    }
}
