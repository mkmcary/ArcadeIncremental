using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNKCameraFollow : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
