using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class SPCCollectible : MonoBehaviour
{

    public float moveSpeed = 2f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, -moveSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SPCPlayerController player = collision.gameObject.GetComponent<SPCPlayerController>();
        if (player != null)
        {
            Collect();
        }
    }

    protected abstract void Collect();
}
