using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KNGFallingObstacle : MonoBehaviour
{
    public float fallSpeed = 5f;

    public int minX = -8;
    public int maxX = 8;

    // Start is called before the first frame update
    private void Start()
    {
        // choose a random x position in range
        float xPos = Random.Range(minX, maxX + 1);
        transform.position = new Vector3(xPos, transform.position.y, 5f);

        // start falling
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -fallSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.GetComponent<KNGKillZone>() != null)
        {
            // we hit a kill zone, destroy this object
            GameObject.Destroy(gameObject);
        }
    }
}
