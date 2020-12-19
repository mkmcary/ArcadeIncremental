using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyNumberController : MonoBehaviour
{
    public Vector2 topLeft;
    public Vector2 bottomRight;

    public int speed;

    private bool isMoving;

    public int xDir;
    public int yDir;

    public int rotSpeed;
    public int rotDir;

    private void Awake()
    {
        beginMoving();
    }

    void beginMoving()
    {
        isMoving = true;
        RectTransform rt = GetComponent<RectTransform>();
        int xPos = (int) Random.Range(topLeft.x, bottomRight.x);
        int yPos = (int) Random.Range(bottomRight.y, topLeft.y);
        speed = (int) Random.Range(1, 6);
        rotSpeed = (int)Random.Range(1, 6);
        rt.localPosition = new Vector3(xPos, yPos);
    }

    void stopMoving()
    {
        isMoving = false;
    }

    private void FixedUpdate()
    {
        if(isMoving)
        {
            RectTransform rt = GetComponent<RectTransform>();
            int xMov = speed * xDir;
            int yMov = speed * yDir;
            if(rt.localPosition.x + xMov >= bottomRight.x || rt.localPosition.x + xMov <= topLeft.x)
            {
                xDir *= -1;
                xMov *= -1;
                speed = (int)Random.Range(1, 6);
                rotSpeed = (int)Random.Range(1, 6);
            }
            if (rt.localPosition.y + yMov <= bottomRight.y || rt.localPosition.y + yMov >= topLeft.y)
            {
                yDir *= -1;
                yMov *= -1;
                speed = (int)Random.Range(1, 6);
                rotSpeed = (int)Random.Range(1, 6);
            }

            rt.localPosition += new Vector3(xMov, yMov, 0);

            Vector3 rotation = new Vector3(0, 0, 1f);
            rotation.z *= rotSpeed * rotDir;

            rt.Rotate(rotation);
        }
    }
}
