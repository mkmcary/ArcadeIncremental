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

    public FireMode FiringMode { get; set; }

    public enum FireMode
    {
        SINGLE, TRIPLE, RAPID
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        leftVelocity = new Vector2(-movementSpeed, 0f);
        rightVelocity = new Vector2(movementSpeed, 0f);
        shouldFire = false;
        FiringMode = FireMode.SINGLE;
    }

    // Update is called once per frame
    void Update()
    {
        if(FiringMode == FireMode.RAPID)
        {
            fireInterval = 0.05f;
        } else
        {
            fireInterval = 0.2f;
        }

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

        // change modes (DEBUG)
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (FiringMode == FireMode.SINGLE)
                FiringMode = FireMode.RAPID;
            else if (FiringMode == FireMode.RAPID)
                FiringMode = FireMode.TRIPLE;
            else if (FiringMode == FireMode.TRIPLE)
                FiringMode = FireMode.SINGLE;
        }

    }

    private IEnumerator Fire()
    {
        shouldFire = true;
        isFiring = true;
        while (shouldFire) {

            // spawn
            if (FiringMode == FireMode.SINGLE || FiringMode == FireMode.RAPID)
                FireSingle();
            else if (FiringMode == FireMode.TRIPLE)
                FireTriple();

            // wait
            yield return new WaitForSeconds(fireInterval);
        }
        isFiring = false;
    }

    private void FireSingle()
    {
        Color col = Color.cyan;
        if (FiringMode == FireMode.RAPID)
            col = Color.red;

        GameObject projectile = Instantiate(projectileToSpawn, transform.position, Quaternion.identity);
        SPCProjectile proj = projectile.GetComponent<SPCProjectile>();
        proj.StartMoving(0f, 5f, col);
        proj.PlayerProjectile = true;
    }

    private void FireTriple()
    {
        //Color col = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        Color col = Color.magenta;

        // 1st
        GameObject projectile1 = Instantiate(projectileToSpawn, transform.position, Quaternion.identity);
        SPCProjectile proj1 = projectile1.GetComponent<SPCProjectile>();
        proj1.PlayerProjectile = true;

        // 2nd
        GameObject projectile2 = Instantiate(projectileToSpawn, transform.position, Quaternion.identity);
        SPCProjectile proj2 = projectile2.GetComponent<SPCProjectile>();
        proj2.PlayerProjectile = true;

        // 3rd
        GameObject projectile3 = Instantiate(projectileToSpawn, transform.position, Quaternion.identity);
        SPCProjectile proj3 = projectile3.GetComponent<SPCProjectile>();
        proj3.PlayerProjectile = true;

        proj1.StartMoving(-0.75f, 5f, col);
        proj2.StartMoving(0, 5f, col);
        proj3.StartMoving(0.75f, 5f, col);
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
