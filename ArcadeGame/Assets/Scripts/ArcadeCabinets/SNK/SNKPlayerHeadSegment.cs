using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNKPlayerHeadSegment : SNKPlayerBodySegment
{
    SNKPlayerController playerController;

    public Vector3 NextPosition { get; set; }

    private Vector3 currentDirection;
    private Vector3 nextDirection;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        nextDirection = Vector3.up;
        currentDirection = Vector3.up;
        DesiredPosition = transform.position + currentDirection;
        NextPosition = DesiredPosition + nextDirection;

        playerController = FindObjectOfType<SNKPlayerController>();

        BodyIndex = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (gameController.IsPlaying)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && currentDirection != Vector3.right)
            {
                nextDirection = Vector3.left;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && currentDirection != Vector3.left)
            {
                nextDirection = Vector3.right;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && currentDirection != Vector3.down)
            {
                nextDirection = Vector3.up;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && currentDirection != Vector3.up)
            {
                nextDirection = Vector3.down;
            }

            if (transform.position == DesiredPosition)
            {
                IsMoving = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, DesiredPosition, Speed * Time.deltaTime);
        }
    }

    public override void UpdateDesiredPosition()
    {
        NextPosition = DesiredPosition + nextDirection;
        DesiredPosition = NextPosition;

        currentDirection = nextDirection;
        IsMoving = true;

        //Rotates the sprites
        if (currentDirection == Vector3.up)
        {
            transform.rotation = upRotation;
        }
        else if (currentDirection == Vector3.left)
        {
            transform.rotation = leftRotation;
        }
        else if (currentDirection == Vector3.right)
        {
            transform.rotation = rightRotation;
        }
        else if (currentDirection == Vector3.down)
        {
            transform.rotation = downRotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SNKPlayerBodySegment collidedSegment = collision.gameObject.GetComponent<SNKPlayerBodySegment>();
        if (collidedSegment != null && collidedSegment.BodyIndex > 3 && !playerController.isSelfColliding)
        {
            playerController.SelfCollide(collidedSegment);
        }
    }
}
