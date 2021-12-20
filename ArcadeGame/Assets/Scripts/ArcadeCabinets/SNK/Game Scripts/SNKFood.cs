using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNKFood : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<SNKPlayerHeadSegment>() != null)
        {
            FindObjectOfType<SNKFoodSpawner>().foodCount--;
            FindObjectOfType<SNKPlayerController>().AddBodySegment();
            FindObjectOfType<SNKGameController>().CollectPoints(50);
            GameObject.Destroy(gameObject);
        }
    }
}
