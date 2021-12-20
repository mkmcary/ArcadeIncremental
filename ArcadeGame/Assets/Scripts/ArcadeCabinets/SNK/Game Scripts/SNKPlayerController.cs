using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SNKPlayerController : MonoBehaviour
{
    public SNKHealthBar health;
    public BigInteger healthPerSegment = 100;

    public SNKPlayerHeadSegment headSegment;
    public GameObject bodySegment;
    private SNKPlayerBodySegment tailSegment;

    public int bodyLength = 1;

    public float Speed;

    public bool isSelfColliding = false;

    public SNKTrashCan trash;

    private void Update()
    {
        bool startNewMovement = true;
        SNKPlayerBodySegment temp = tailSegment;
        while(temp != null)
        {
            if (temp.IsMoving)
            {
                startNewMovement = false;
                break;
            }
            temp = temp.Ahead;
        }

        temp = tailSegment;
        if (startNewMovement)
        {
            while(temp != null)
            {
                temp.UpdateDesiredPosition();
                temp = temp.Ahead;
            }
        }
    }

    // New Segments are being added too close to the head. Figure out when it should start moving
    public void AddBodySegment()
    {
        GameObject newPiece = Instantiate(bodySegment, tailSegment.transform.position, UnityEngine.Quaternion.identity);
        newPiece.transform.SetParent(transform);
        SNKPlayerBodySegment newSegment = newPiece.GetComponent<SNKPlayerBodySegment>();
        SNKPlayerBodySegment prevTail = tailSegment;

        // Set the previously last body segment before the tail to be ahead of the newest body segment
        prevTail.Behind = newSegment;
        newSegment.Ahead = prevTail;

        if(prevTail != headSegment)
        {
            prevTail.GetComponent<SpriteRenderer>().sprite = GameOperations.LoadSpriteFromPath("Sprites/ArcadeCabinets/SNK/CenterSegment3");
        }
        
        // Set the tail segment to be the new segment
        tailSegment = newSegment;
        newSegment.BodyIndex = bodyLength++;

        health.IncrementMaxHealth(healthPerSegment);

        // update speed
        Speed *= 1.01f;
    }

    public void SelfCollide(SNKPlayerBodySegment collidedSegment)
    {
        // TODO: Implement breaking off part that collides.
        // Set Ahead of the collided to the tail
        // for loop through body and destroy gameobjects
        // make sure length if correct.

        /*
        isSelfColliding = true;
        tailSegment = collidedSegment.Ahead;
        tailSegment.Behind = null;
        int segementsLost = bodyLength - collidedSegment.BodyIndex;
        tailSegment.GetComponent<SpriteRenderer>().sprite = GameOperations.LoadSpriteFromPath("Sprites/ArcadeCabinets/SNK/TailSegment");
        int segmentNum = collidedSegment.BodyIndex;
        SNKPlayerBodySegment nextSegment = collidedSegment;
        if (!isDeleting)
        {
            isDeleting = true;
            for (int i = segmentNum; i < bodyLength; i++)
            {
                SNKPlayerBodySegment currentSegment = nextSegment;
                nextSegment = currentSegment.Behind;
                GameObject.Destroy(currentSegment.gameObject, 1f);
            }

            bodyLength = tailSegment.BodyIndex + 1;

            health.IncrementMaxHealth(-healthPerSegment * segementsLost);
            isSelfColliding = false;
        }
        isDeleting = false;
        */

        isSelfColliding = true;

        // Sever connections between last remaining segment and collided segment.
        if(collidedSegment.Ahead == null)
        {
            isSelfColliding = false;
            return;
        }
        tailSegment = collidedSegment.Ahead;
        tailSegment.Behind = null;
        tailSegment.GetComponent<SpriteRenderer>().sprite = GameOperations.LoadSpriteFromPath("Sprites/ArcadeCabinets/SNK/TailSegment3");
        collidedSegment.Ahead = null;

        int oldLength = bodyLength;

        MoveToTrash(collidedSegment);

        int segmentsLost = oldLength - bodyLength;

        health.IncrementMaxHealth(-healthPerSegment * segmentsLost);

        isSelfColliding = false;
    }

    public void ResetPlayer()
    {
        SNKPlayerBodySegment current = tailSegment;
        while(current != null && current != headSegment)
        {
            SNKPlayerBodySegment temp = current.Ahead;
            MoveToTrash(current);
            current = temp;
        }

        headSegment.transform.position = UnityEngine.Vector3.zero;

        health.Start();

        tailSegment = headSegment;

        headSegment.Start();

        // Reset Speed
        Speed = 5f;
    }

    private void MoveToTrash(SNKPlayerBodySegment segment)
    {
        if(segment == null)
        {
            return;
        }
        SNKPlayerBodySegment next = segment.Behind;
        segment.Behind = null;

        if(next != null)
            next.Ahead = null;

        segment.gameObject.transform.SetParent(trash.gameObject.transform);
        segment.transform.localPosition = UnityEngine.Vector3.zero;

        

        bodyLength--;

        MoveToTrash(next);
    }
}
