using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNKSpaceStationFood : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SNKPlayerHeadSegment>() != null)
        {
            transform.GetComponentInParent<SNKSpaceStation>().Die();
        }
    }
}
