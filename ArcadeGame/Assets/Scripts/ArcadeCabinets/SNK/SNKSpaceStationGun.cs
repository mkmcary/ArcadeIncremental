using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNKSpaceStationGun : MonoBehaviour
{
    public GameObject laserPrefab;
    private Color myColor;

    private Transform player;

    private float timer;
    private float shootSpeed;

    private float range;
    private float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<SNKPlayerHeadSegment>().transform;
        range = 15f;
        rotationSpeed = 60f;
        timer = 0;
        shootSpeed = 3f;

        myColor = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (LookAtTarget())
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
        if(timer >= shootSpeed)
        {
            Shoot();
        }
    }

    // Investigate why moving on 180/-180 barrier.
    private bool LookAtTarget()
    {
        // Find the vector that points to the player and return if its magnitude is out of range.
        Vector3 diff = player.position - transform.position;
        if(diff.magnitude > range)
        {
            return false;
        }
        diff.Normalize();

        float desiredRotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        // Correct for orientation.
        desiredRotationZ -= 90;
        if(desiredRotationZ < -180f)
        {
            desiredRotationZ += 360;
        }

        // Constrain movement to the rail.
        if (desiredRotationZ < -135f && desiredRotationZ >= -180f)
        {
            desiredRotationZ = -135f;
        }
        else if (desiredRotationZ > 135f && desiredRotationZ <= 180f)
        {
            desiredRotationZ = 135f;
        }

        // Limit rotational speed.
        float currentRotationZ = transform.localEulerAngles.z;

        if(currentRotationZ > 180)
        {
            currentRotationZ -= 360;
        }

        float rotDiff = desiredRotationZ - currentRotationZ;
        float absRotDiff = Mathf.Abs(rotDiff);
        float targetRotationZ = desiredRotationZ;

        if (absRotDiff < rotationSpeed * Time.deltaTime)
        {
            // Case 1: Our target is within speed range.
            targetRotationZ = desiredRotationZ;
        }
        else if (rotDiff < 0)
        {
            // Case 2: Our target is out of speed range clockwise.
            targetRotationZ = currentRotationZ - rotationSpeed * Time.deltaTime;
        }
        else
        {
            // Case 3: Our target is out of speed range counter-clockwise.
            targetRotationZ = currentRotationZ + rotationSpeed * Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, targetRotationZ);

        return true;
    }

    public void Shoot()
    {
        Transform location = transform.GetChild(0);

        GameObject laser = Instantiate(laserPrefab, location.position, transform.rotation);
        laser.GetComponent<SpriteRenderer>().color = myColor;
        timer = 0;
    }
}
