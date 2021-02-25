using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNKSpaceStationLaser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        float speed = 5f;
        rigid.velocity = new Vector2(transform.up.x * speed, transform.up.y * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SNKPlayerController playerController = FindObjectOfType<SNKPlayerController>();
        GameObject collidedObject = collision.gameObject;
        SNKPlayerHeadSegment head = collidedObject.GetComponent<SNKPlayerHeadSegment>();
        SNKPlayerBodySegment body = collidedObject.GetComponent<SNKPlayerBodySegment>();
        if(head != null || body != null)
        {
            playerController.health.IncrementCurrentHealth(-50);
            GameObject.Destroy(gameObject);
        }
    }
}
