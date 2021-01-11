using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KNGObstacle : MonoBehaviour
{
    public float obstacleMomentum = 3f;
    public float chanceToFall = 0.2f;


    private bool goingLeft;
    private bool grounded;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        goingLeft = false;
        grounded = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // limit speed
        if(goingLeft && grounded)
        {
            rb.velocity = new Vector2(-obstacleMomentum, rb.velocity.y);
        } 
        else if(grounded)
        {
            rb.velocity = new Vector2(obstacleMomentum, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.collider.gameObject;
        if (other.GetComponent<KNGKillZone>() != null)
        {
            // we hit a kill zone, destroy this object
            GameObject.Destroy(gameObject);
        }
        else
        {
            KNGPlatform platform = other.GetComponent<KNGPlatform>();

            if (platform != null)
            {
                grounded = true;
                if (platform.isObstacleFlipper)
                {
                    // we hit a platform, flip momentum
                    ChangeDirection();
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        KNGPlatform platform = collision.gameObject.GetComponent<KNGPlatform>();

        if (platform != null)
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        KNGObstacleDropPoint drop = collision.gameObject.GetComponent<KNGObstacleDropPoint>();
        if (drop != null)
        {
            KNGLadder ladder = drop.ladder;

            // we need to decide whether to fall down this ladder or not
            float rand = Random.Range(0f, 1f);
            if (rand < chanceToFall)
            {
                // trigger fall
                StartCoroutine(FallDownLadder(ladder));
            }
        }
    }

    private IEnumerator FallDownLadder(KNGLadder ladder)
    {
        CircleCollider2D coll = gameObject.GetComponent<CircleCollider2D>();

        // set rigidbody to kinematic
        rb.isKinematic = true;
        grounded = false;

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        
        Vector3 startPos = ladder.topOfLadder.position;
        Vector3 endPos = ladder.bottomOfLadder.position;

        // interpolate position between these two
        for(float t = 0f; t <= 1f; t += 0.01f)
        {
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return new WaitForSeconds(0.01f);
        }

        // set back to original values
        rb.isKinematic = false;

        // change direction
        ChangeDirection();
    }

    private void ChangeDirection()
    {
        goingLeft = !goingLeft;
        grounded = true;

        rb.angularVelocity = -rb.angularVelocity;
    }
}
