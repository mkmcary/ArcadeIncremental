using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SPCPlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 leftVelocity;
    private Vector2 rightVelocity;

    public float fireInterval = 0.2f;
    public bool shouldFire;
    private bool isFiring;
    public GameObject projectileToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        leftVelocity = new Vector2(-movementSpeed, 0f);
        rightVelocity = new Vector2(movementSpeed, 0f);
        shouldFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        // left and right
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = rightVelocity;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = leftVelocity;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        // fire
        if(Input.GetKeyDown(KeyCode.Space) && !isFiring)
        {
            StartCoroutine(Fire());
        } 
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            shouldFire = false;
        }

    }

    private IEnumerator Fire()
    {
        shouldFire = true;
        isFiring = true;
        while (shouldFire) {
            // spawn
            GameObject projectile = Instantiate(projectileToSpawn, transform.position, Quaternion.identity);
            SPCProjectile proj = projectile.GetComponent<SPCProjectile>();
            proj.StartMoving(5f, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
            proj.PlayerProjectile = true;

            // wait
            yield return new WaitForSeconds(fireInterval);
        }
        isFiring = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SPCProjectile proj = collision.gameObject.GetComponent<SPCProjectile>();
        if (proj != null)
        {
            if (!proj.PlayerProjectile)
            {
                // lose (animation?)
                Debug.Log("YOU LOST");
                GameObject.Destroy(proj.gameObject);
            }
        }
    }
}
