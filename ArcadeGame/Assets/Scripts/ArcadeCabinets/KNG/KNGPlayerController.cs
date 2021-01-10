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

    // camera movement
    public KNGCameraFollow cam;

    // the player's rigidbody component
    private Rigidbody2D rb;

    // tracks if jumping is allowed
    private bool canJump;

    // ladders
    private KNGLadder currentLadder;
    private bool isClimbing;

    // game controller
    private KNGGameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindObjectOfType<KNGGameController>();
        rb = GetComponent<Rigidbody2D>();
        currentLadder = null;
        isClimbing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.IsPlaying)
        {
            // left and right
            if (Input.GetKey(KeyCode.D) && !isClimbing)
            {
                rb.velocity = new Vector2(runSpeed, rb.velocity.y);
            }
            else if (Input.GetKey(KeyCode.A) && !isClimbing)
            {
                rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            // jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            // ladder climbing
            if (currentLadder != null)
            {
                // make sure the player can jump while on a ladder
                if (!canJump)
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
                        transform.position = new Vector3(currentLadder.transform.position.x, transform.position.y, transform.position.z);
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
                    }
                    else
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
            if (!isClimbing)
            {
                rb.isKinematic = false;
            }
        }
    }

    public void Jump()
    {
        if (gameController.IsPlaying && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
            isClimbing = false;
            cam.moveCam = false;
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
            cam.moveCam = true;
        }
        else if(other.GetComponent<KNGObstacle>() != null)
        {
            // we hit an obstacle
            gameController.HitObstacle();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        KNGLadder ladder = collision.gameObject.GetComponent<KNGLadder>();
        KNGObjective objective = collision.gameObject.GetComponent<KNGObjective>();
        if (ladder != null)
        {
            // we are on a ladder, we can climb
            currentLadder = ladder;
        } else if(objective != null)
        {
            // we reached the end of this level, figure out what to do next
            gameController.ReachedObjective();
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
