using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SNKPlayerController : MonoBehaviour
{
    public SNKHealthBar health;
    public BigInteger healthPerSegment = 10;

    public SNKPlayerBodySegment headSegment;
    public GameObject bodySegment;
    private SNKPlayerBodySegment tailSegment;

    public int bodyLength = 1;

    // Start is called before the first frame update
    void Start()
    {
        tailSegment = headSegment;
    }

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
            prevTail.GetComponent<SpriteRenderer>().sprite = GameOperations.LoadSpriteFromPath("Sprites/ArcadeCabinets/SNK/CenterSegment");
        }
        
        // Set the tail segment to be the new segment
        tailSegment = newSegment;
        newSegment.BodyIndex = bodyLength++;

        health.IncrementMaxHealth(healthPerSegment);
    }

    public void SelfCollide(SNKPlayerBodySegment collidedSegment)
    {
        // TODO: Implement breaking off part that collides.
        // Set Ahead of the collided to the tail
        // for loop through body and destroy gameobjects
        // make sure length if correct.
        health.IncrementCurrentHealth(-healthPerSegment);
    }
}
