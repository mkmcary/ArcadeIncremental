using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNKPlayerBodySegment : MonoBehaviour
{
    public SNKPlayerBodySegment Ahead { get; set; }
    public SNKPlayerBodySegment Behind { get; set; }

    public Vector3 DesiredPosition { get; set; }

    public float Speed { get; set; }

    Rigidbody2D rigid;

    public int BodyIndex { get; set; }

    // Flags for spawning and updating movement
    private bool initialized;
    public bool IsMoving { get; set; }


    protected Quaternion upRotation;
    protected Quaternion leftRotation;
    protected Quaternion rightRotation;
    protected Quaternion downRotation;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        Speed = 5f;
        initialized = false;
        IsMoving = true;

        upRotation = Quaternion.Euler(0, 0, 0);
        leftRotation = Quaternion.Euler(0, 0, 90);
        rightRotation = Quaternion.Euler(0, 0, 270);
        downRotation = Quaternion.Euler(0, 0, 180);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(Ahead != null)
        {
            if (!initialized)
            {
                UpdateDesiredPosition();
            }
            if (transform.position == DesiredPosition)
            {
                IsMoving = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, DesiredPosition, Speed * Time.deltaTime);
        }
        
    }

    public virtual void UpdateDesiredPosition()
    {
        if (!initialized)
        {
            DesiredPosition = transform.position;
            initialized = true;
        }
        else
        {
            DesiredPosition = Ahead.transform.position;
            IsMoving = true;
        }

        //Rotates the sprites
        if (DesiredPosition.y > transform.position.y)
        {
            transform.rotation = upRotation;
        }
        else if (DesiredPosition.x < transform.position.x)
        {
            transform.rotation = leftRotation;
        }
        else if (DesiredPosition.x > transform.position.x)
        {
            transform.rotation = rightRotation;
        }
        else if (DesiredPosition.y < transform.position.y)
        {
            transform.rotation = downRotation;
        }

    }
}
