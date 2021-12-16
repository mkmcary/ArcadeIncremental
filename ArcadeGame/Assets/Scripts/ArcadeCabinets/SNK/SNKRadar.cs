using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNKRadar : MonoBehaviour
{

    private CircleCollider2D radar;

    private ContactFilter2D filter;


    // Start is called before the first frame update
    void Start()
    {
        radar = gameObject.GetComponent<CircleCollider2D>();

        filter = new ContactFilter2D();
        filter.NoFilter();
    }

    // Update is called once per frame
    void Update()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        radar.OverlapCollider(filter, colliders);
        float smallestDist = 100f;
        SNKSpaceStation closestSpaceStation = null;
        for(int i = 0; i < colliders.Count; i++)
        {
            GameObject go = colliders[i].gameObject;
            SNKSpaceStation ss = go.GetComponent<SNKSpaceStation>();
            if(ss == null)
            {
                continue;
            }
            float dist = Vector3.Distance(ss.transform.position, transform.position);
            if(smallestDist > dist)
            {
                closestSpaceStation = ss;
            }
        }

        // Double check we are pointing at the right ship
        Vector3 diff = transform.position - closestSpaceStation.transform.position;
        float desiredRotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        desiredRotationZ -= 90;
        transform.rotation = Quaternion.Euler(0f, 0f, desiredRotationZ);
    }
}
