using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KNGObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.collider.gameObject;
        if (other.GetComponent<KNGKillZone>() != null)
        {
            // we hit a kill zone, destroy this object
            Debug.Log("Destroying self.");
            GameObject.Destroy(gameObject);
        }
    }
}
