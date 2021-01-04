using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KNGPlayerController : MonoBehaviour
{
    public float runSpeed = 5f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;

    private bool canJump;

    private KNGLadder currentLadder;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentLadder = null;
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
        }

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
        }

        // ladder climbing
        if (currentLadder != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {

            }
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
        else if(other.GetComponent<KNGLadder>() != null)
        {
            // we are on a ladder, we can climb
            currentLadder = other.GetComponent<KNGLadder>();   
        }
        else if(other.GetComponent<KNGObstacle>() != null)
        {
            // we hit an obstacle, game over
            Debug.Log("Hit an obstacle.");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }


}
