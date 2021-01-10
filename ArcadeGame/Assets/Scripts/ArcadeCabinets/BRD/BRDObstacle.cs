using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRDObstacle : MonoBehaviour
{
    private BRDGameController gameController;

    public Vector2 velocity;

    private Rigidbody2D rigid;

    private float killLocation = -20f;

    private void Start()
    {
        velocity = new Vector2(-5f, 0);

        gameController = GameObject.FindObjectOfType<BRDGameController>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!gameController.isPlaying)
        {
            rigid.velocity = new Vector2(0, 0);
            return;
        }

        rigid.velocity = velocity;
        
        if(transform.position.x <= killLocation)
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<BRDPlayerController>() != null)
        {
            BRDBonusPoints bp = gameObject.GetComponent<BRDBonusPoints>();
            if(bp != null)
            {
                gameController.GainScore(bp.points);
                GameObject.Destroy(bp.gameObject);
            }
            else
            {
                gameController.EndGame();
            }
            
        }
    }
}
