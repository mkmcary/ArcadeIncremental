using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SPCStandardTroop : MonoBehaviour
{
    public long pointValue = 100;
    public GameObject projectilePrefab;
    public float speed = 1f;
    private Rigidbody2D rigidbody;

    private bool inMotion;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SPCProjectile proj = collision.gameObject.GetComponent<SPCProjectile>();
        if (proj != null)
        {
            if (proj.PlayerProjectile)
            {
                // give points (game controller)
                Debug.Log("You earned " + pointValue + " points");

                // destroy (maybe a custom method to play animation?)
                GameObject.Destroy(proj.gameObject);
                GameObject.Destroy(gameObject);
            }
        }
    }

    private IEnumerator MoveAndShoot()
    {
        inMotion = true;

        // start moving
        rigidbody.velocity = new Vector3(0, -speed, 0);
        yield return new WaitForSeconds(0.5f);
        
        // stop moving
        rigidbody.velocity = Vector3.zero;
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
}
