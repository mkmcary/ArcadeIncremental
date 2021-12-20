using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNKTrashCan : MonoBehaviour
{ 
    private float timer;
    private float interval;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        interval = 10f;
    }

    private void FixedUpdate()
    {
        if(transform.childCount == 0)
        {
            return;
        }
        timer += Time.deltaTime;
        if(timer >= interval)
        {
            Empty();
        }
    }

    private void Empty()
    {
        int count = transform.childCount;
        for(int i = 0; i < count; i++)
        {
            GameObject segement = transform.GetChild(0).gameObject;
            GameObject.Destroy(segement);
        }
    }
}
