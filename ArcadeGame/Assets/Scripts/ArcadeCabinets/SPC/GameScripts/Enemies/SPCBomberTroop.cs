using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SPCBomberTroop : MonoBehaviour
{
    public long pointValue = 1000;
    public float speed = 1f;
    public float health = 5;
    private Rigidbody2D rb;

    public GameObject explosionPrefab;

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

                    // BLOW UP
                    Instantiate(explosionPrefab, transform.position, Quaternion.identity);

                    GameObject.Destroy(gameObject);
                }
            }
        }
    }
}
