using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNKSpaceStationGun : MonoBehaviour
{
    public GameObject laserPrefab;

    private Transform player;

    private float range;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<SNKPlayerHeadSegment>().transform;
        range = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtTarget();
    }

    // WIP
    private void LookAtTarget()
    {
        Vector3 diff = player.position - transform.position;
        if(diff.magnitude > range)
        {
            return;
        }
        diff.Normalize();

        float desiredRotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        desiredRotationZ -= 90;
        Debug.Log("Rotation: " + desiredRotationZ);
        if (desiredRotationZ < -135f && desiredRotationZ > -180f)
        {
            desiredRotationZ = -135f;
        }
        else if (desiredRotationZ > -225f && desiredRotationZ < -180f)
        {
            desiredRotationZ = -225f;
        }
        float currentRotationZ = transform.rotation.z;
        float rotDiff = desiredRotationZ - currentRotationZ;

        transform.rotation = Quaternion.Euler(0f, 0f, desiredRotationZ);
    }
}
