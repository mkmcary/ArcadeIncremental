using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRDFinishLine : MonoBehaviour
{
    BRDGameController gameController;
    Vector2 velocity = new Vector2(-5f, 0);

    private void Start()
    {
        gameController = FindObjectOfType<BRDGameController>();
    }

    private void FixedUpdate()
    {
        Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
        if (!gameController.isPlaying)
        {
            rigid.velocity = new Vector2(0, 0);
            return;
        }
        
        rigid.velocity = velocity;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BRDPlayerController>() != null)
        {
            gameController.Win();
        }
    }
}
