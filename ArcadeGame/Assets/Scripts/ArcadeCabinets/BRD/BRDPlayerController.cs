using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BRDPlayerController : MonoBehaviour
{
    public float jumpHeight = 5f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;

    private bool isFlipped;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        isFlipped = false;
    }

    public void Jump()
    {
        int direction = 1;
        if (isFlipped)
        {
            direction = -1;
        }

        rigid.velocity = new Vector2(rigid.velocity.x, jumpHeight * direction);

    }

    public void Flip()
    {
        isFlipped = !isFlipped;
        Vector2 vTemp = transform.localScale;
        vTemp.y *= -1;
        transform.localScale = vTemp;
        
        rigid.gravityScale *= -1;
    }

    
}
