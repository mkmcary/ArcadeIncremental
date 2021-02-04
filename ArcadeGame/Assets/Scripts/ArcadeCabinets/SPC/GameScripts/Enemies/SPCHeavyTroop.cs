using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SPCHeavyTroop : MonoBehaviour
{
    public long pointValue = 500;
    public GameObject projectilePrefab;
    public float speed = 1f;
    public int health = 3;
    private Rigidbody2D rb;

    private bool inMotion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!inMotion)
        {
            StartCoroutine(MoveAndShoot());
        }
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
        Color col = Color.yellow;

        // 1st
        GameObject projectile1 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        SPCProjectile proj1 = projectile1.GetComponent<SPCProjectile>();
        proj1.PlayerProjectile = false;

        // 2nd
        GameObject projectile2 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        SPCProjectile proj2 = projectile2.GetComponent<SPCProjectile>();
        proj2.PlayerProjectile = false;

        // 3rd
        GameObject projectile3 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        SPCProjectile proj3 = projectile3.GetComponent<SPCProjectile>();
        proj3.PlayerProjectile = false;

        proj1.StartMoving(-0.75f, -5f, col);
        proj2.StartMoving(0, -5f, col);
        proj3.StartMoving(0.75f, -5f, col);
    }
}
