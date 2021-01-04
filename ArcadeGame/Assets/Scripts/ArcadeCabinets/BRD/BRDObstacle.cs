using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRDObstacle : MonoBehaviour
{
    public Vector2 velocity;

    private Rigidbody2D rigid;

    private float killLocation = -10f;

    private void Start()
    {
        velocity = new Vector2(-5f, 0);

        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigid.velocity = velocity;
        
        if(transform.position.x <= killLocation)
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
