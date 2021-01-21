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

    private bool initialized;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        Speed = 3f;
        initialized = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(Ahead != null)
        {
            if (!initialized || transform.position == DesiredPosition)
            {
                UpdateDesiredPosition();
            }
            transform.position = Vector3.MoveTowards(transform.position, DesiredPosition, Speed * Time.deltaTime);
        }
        
    }

    protected virtual void UpdateDesiredPosition()
    {
        initialized = true;
        DesiredPosition = Ahead.transform.position;
    }
}
