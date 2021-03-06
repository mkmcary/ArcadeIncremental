﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KNGCameraFollow : MonoBehaviour
{
    // player location
    public Transform player;

    // offsets for movement
    public float moveUpPoint;
    public float moveDownPoint;
    public float moveUpOffset;
    public float moveDownOffset;

    // used to determine whether the camera should move
    public bool moveCam;

    // tracks whether we are actively moving.
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        moveCam = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (moveCam)
        {
            float yDiff = transform.position.y - player.position.y;
            if (yDiff < moveUpPoint && !isMoving)
            {
                Vector3 start = transform.position;
                Vector3 target = new Vector3(transform.position.x, player.position.y + moveUpOffset, transform.position.z);
                StartCoroutine(MoveCam(start, target));
            }
            if (yDiff > moveDownPoint && !isMoving)
            {
                Vector3 start = transform.position;
                Vector3 target = new Vector3(transform.position.x, player.position.y + moveUpOffset, transform.position.z);
                StartCoroutine(MoveCam(start, target));
            }
        }
    }

    private IEnumerator MoveCam(Vector3 startingPos, Vector3 targetPos)
    {
        isMoving = true;
        for(float i = 0; i <= 1; i+= 0.01f)
        {
            transform.position = Vector3.Lerp(startingPos, targetPos, i);       
            yield return new WaitForSeconds(0.0025f);
        }
        isMoving = false;
    }
}
