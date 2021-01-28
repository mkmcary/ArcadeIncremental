using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPCProjectile : MonoBehaviour
{
    public float moveTime = 5f;
    private bool isMoving = false;
    private float timer = 0f;

    public bool PlayerProjectile { get; set; }

    private void FixedUpdate()
    {
        if(isMoving)
        {
            timer += Time.fixedDeltaTime;
            if(timer >= moveTime)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }

    public void StartMoving(float movementSpeed, Color color)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, movementSpeed);
        GetComponent<SpriteRenderer>().color = color;

        isMoving = true;
    }
}
