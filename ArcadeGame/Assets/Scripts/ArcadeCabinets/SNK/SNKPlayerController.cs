using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNKPlayerController : MonoBehaviour
{
    public SNKPlayerBodySegment headSegment;
    public GameObject bodySegment;
    private SNKPlayerBodySegment tailSegment;

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
        GameObject newPiece = Instantiate(bodySegment, tailSegment.transform.position, Quaternion.identity);
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
    }
}
