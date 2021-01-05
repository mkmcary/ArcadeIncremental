using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KNGObstacle : MonoBehaviour
{
    public float obstacleMomentum = 3f;
    public bool goingLeft;

    // Start is called before the first frame update
    void Start()
    {
        goingLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.collider.gameObject;
        if (other.GetComponent<KNGKillZone>() != null)
        {
            // we hit a kill zone, destroy this object
            Debug.Log("Destroying self.");
            GameObject.Destroy(gameObject);
        }
        else
        {
            KNGPlatform platform = other.GetComponent<KNGPlatform>();

            if (platform != null)
            {
                if (platform.isObstacleFlipper)
                {
                    // we hit a platform, flip momentum
                    Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

                    Debug.Log(rb.velocity.x);

                    if (goingLeft)
                    {
                        rb.velocity = new Vector2(obstacleMomentum, 0);
                        goingLeft = false;
                    }
                    else
                    {
                        rb.velocity = new Vector2(-obstacleMomentum, 0);
                        goingLeft = true;
                    }
                    
                    rb.angularVelocity = -rb.angularVelocity;
                }
            }
        }
    }
}
