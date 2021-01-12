using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KNGObstacle : MonoBehaviour
{
    // movement vars
    public float obstacleMomentum = 3f;
    public float chanceToFall = 0.2f;
    public float ladderFallSpeed = 2f;

    // game status
    private bool goingLeft;
    private bool grounded;

    // falling down ladder
    private bool isFalling;
    private KNGLadder ladder;

    // rigidbody
    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        goingLeft = false;
        grounded = false;
        isFalling = false;
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

        // check ladder fall
        if(isFalling)
        {
            if(transform.position.y <= ladder.bottomOfLadder.position.y)
            {
                isFalling = false;
                rb.gravityScale = 1;
                gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
                ChangeDirection();
            }
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
            KNGLadder newLadder = drop.ladder;

            // we need to decide whether to fall down this ladder or not
            float rand = Random.Range(0f, 1f);
            if (rand < chanceToFall)
            {
                // trigger fall
                this.ladder = newLadder;
                FallDownLadder();

            }
        }
    }

    private void FallDownLadder()
    {
        // initialize values
        rb.gravityScale = 0;
        gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        grounded = false;
        isFalling = true;

        // position and speed
        transform.position = ladder.topOfLadder.position;
        rb.angularVelocity = 0f;
        rb.velocity = new Vector2(0, -ladderFallSpeed);
    }

    private void ChangeDirection()
    {
        goingLeft = !goingLeft;
        grounded = true;

        rb.angularVelocity = -rb.angularVelocity;
    }
}
