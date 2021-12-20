using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNKRadar : MonoBehaviour
{
    private SNKPlayerHeadSegment head;

    private CircleCollider2D radar;

    public GameObject myTarget;


    // Start is called before the first frame update
    void Start()
    {
        radar = gameObject.GetComponent<CircleCollider2D>();
        head = GameObject.FindObjectOfType<SNKPlayerHeadSegment>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = head.transform.position;
        radar.radius += .1f;

        // Double check we are pointing at the right ship
        if (myTarget != null)
        {
            Vector3 diff = transform.position - myTarget.transform.position;
            float desiredRotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            desiredRotationZ += 180;
            transform.rotation = Quaternion.Euler(0f, 0f, desiredRotationZ);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SNKSpaceStation>() != null)
        {
            if(collision.gameObject != myTarget)
            {
                myTarget = collision.gameObject;
            }
            radar.radius = 0f;
        }
    }
}
