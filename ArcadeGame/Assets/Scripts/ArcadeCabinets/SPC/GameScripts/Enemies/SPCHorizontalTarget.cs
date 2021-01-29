using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPCHorizontalTarget : SPCTarget
{
    public float minX;
    public float maxX;
    public float yMov;
    public float speed;

    private float direction;

    // Start is called before the first frame update
    protected override void Start()
    {
        direction = 1f;
        base.Start();
    }

    protected override IEnumerator Move()
    {
        isMoving = true;

        transform.position += new Vector3(speed * 0.02f * direction, 0 , 0);
        if (transform.position.x >= maxX || transform.position.x <= minX)
        {
            direction *= -1f;
            transform.position += new Vector3(0, -yMov, 0);
        }

        yield return new WaitForSeconds(0.02f);

        isMoving = false;
    }

    protected override IEnumerator Shoot()
    {
        isShooting = true;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        SPCProjectile proj = projectile.GetComponent<SPCProjectile>();
        proj.StartMoving(0f, -5f, Color.green);
        proj.PlayerProjectile = false;

        yield return new WaitForSeconds(2f);
        isShooting = false;
    }
}
