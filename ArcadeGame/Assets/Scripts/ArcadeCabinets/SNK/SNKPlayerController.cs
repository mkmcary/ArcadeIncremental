using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SNKPlayerController : MonoBehaviour
{
    Vector2 velocity = new Vector2(1, 0);

    Rigidbody2D rigid;

    private List<GameObject> body;
    public GameObject headSegment;
    public GameObject bodySegment;
    public GameObject tailSegment;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        rigid.velocity = velocity;
        ResetBody();

    }

    private void ResetBody()
    {
        body = new List<GameObject>();
        body.Add(headSegment);
        body.Add(tailSegment);
    }

    public void AddBodySegement()
    {
        GameObject newPiece = Instantiate(bodySegment, transform.position, Quaternion.identity);
        body.Insert(body.Count - 2, newPiece);
        newPiece.GetComponent<Rigidbody2D>().velocity = body[body.Count - 3].GetComponent<Rigidbody2D>().velocity;
    }

    private void RecalculateVelocity()
    {
        for(int i = body.Count; i >= 1; i--)
        {
            body[i].GetComponent<Rigidbody2D>().velocity = body[i - 1].GetComponent<Rigidbody2D>().velocity;
        }
    }
}
