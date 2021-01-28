using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPCBarrier : MonoBehaviour
{
    public int health = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SPCProjectile proj = collision.gameObject.GetComponent<SPCProjectile>();
        if (proj != null)
        {
            health--;
        }

        if(health == 0)
        {
            GameObject.Destroy(proj.gameObject);
            GameObject.Destroy(gameObject);
        }
    }
}
