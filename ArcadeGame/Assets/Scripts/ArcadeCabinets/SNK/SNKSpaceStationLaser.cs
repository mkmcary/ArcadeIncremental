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
}
