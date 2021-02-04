using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPCSniperTroop : MonoBehaviour
{
    public long pointValue = 300;
    public GameObject projectilePrefab;
    public float health = 1;

    private Transform player;

    private bool isShooting;

    private void Start()
    {
        isShooting = false;
        player = GameObject.FindObjectOfType<SPCPlayerController>().gameObject.transform;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SPCProjectile proj = collision.gameObject.GetComponent<SPCProjectile>();
        if (proj != null)
        {
            if (proj.PlayerProjectile)
            {
                GameObject.Destroy(proj.gameObject);
                health--;

                if (health <= 0)
                {
                    // give points (game controller)
                    Debug.Log("You earned " + pointValue + " points");

                    // destroy (maybe a custom method to play animation?)
                    GameObject.Destroy(gameObject);
                }
            }
        }
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

}
