using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KNGPlayerController : MonoBehaviour
{
    // factors affecting player movement
    public float runSpeed = 5f;
    public float jumpForce = 5f;
    public float climbSpeed = 2f;
    public float ladderMoveScalar = 0.2f;

    // the player's rigidbody component
    private Rigidbody2D rb;

    // tracks if jumping is allowed
    private bool canJump;

    // ladders
    private KNGLadder currentLadder;
    private bool isClimbing;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentLadder = null;
        isClimbing = false;
    }

    // Update is called once per frame
    void Update()
    {
        // left and right
        if(Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
        } else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
            isClimbing = false;
        }

        // ladder climbing
        if (currentLadder != null)
        {
            // make sure the player can jump while on a ladder
            if(!canJump)
            {
                canJump = true;
            }

            if (Input.GetKey(KeyCode.W))
            {
                // update if needed
                if (!isClimbing)
                {
                    isClimbing = true;
                    rb.isKinematic = true;
                }

                // move up
                rb.velocity = new Vector2(rb.velocity.x * ladderMoveScalar, climbSpeed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                // make sure we arent too low
                if (transform.position.y > currentLadder.bottomOfLadder.position.y)
                {
                    // move down
                    rb.velocity = new Vector2(rb.velocity.x * ladderMoveScalar, -climbSpeed);
                } else
                {
                    // we are at the bottom, detach
                    isClimbing = false;
                }
            }
            else if (isClimbing)
            {
                rb.velocity = new Vector2(rb.velocity.x * ladderMoveScalar, 0);
            }
        }

        // if we arent climbing, act as normal
        if(!isClimbing) 
        {
            rb.isKinematic = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check what type of collision this was
        GameObject other = collision.collider.gameObject;
        if(other.GetComponent<KNGPlatform>() != null)
        {
            // we hit a platform, we can jump
            canJump = true;
        }
        else if(other.GetComponent<KNGObstacle>() != null)
        {
            // we hit an obstacle, game over
            Debug.Log("Hit an obstacle.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger entered.");
        KNGLadder ladder = collision.gameObject.GetComponent<KNGLadder>();
        if (ladder != null)
        {
            // we are on a ladder, we can climb
            currentLadder = ladder;
            Debug.Log("Attached to ladder.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        KNGLadder ladder = collision.gameObject.GetComponent<KNGLadder>();
        if (ladder != null)
        {
            // we just left a ladder
            currentLadder = null;
            isClimbing = false;
        }
    }


}
