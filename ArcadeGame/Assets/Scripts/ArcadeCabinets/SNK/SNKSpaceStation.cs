using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNKSpaceStation : MonoBehaviour
{
    public GameObject spaceShipPrefab;
    public int ShipCount { get; set; }


    private void Start()
    {
        ShipCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(ShipCount == 0)
        {
            SpawnShip();
        }
    }

    public void SpawnShip()
    {
        ShipCount++;
        GameObject spaceShip = Instantiate(spaceShipPrefab, transform.position, Quaternion.identity);
        spaceShip.transform.SetParent(transform);
        spaceShip.GetComponent<SNKSpaceShip>().StartMoving(this);
    }

    private void RecieveShip(SNKSpaceShip spaceShip)
    {
        spaceShip.transform.SetParent(transform);
        spaceShip.StartMoving(this);
        ShipCount++;
    }

    public void Die()
    {
        // TODO: Finish this method to give points and animation
        FindObjectOfType<SNKPlayerController>().AddBodySegment();
        GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SNKSpaceShip spaceShip = collision.gameObject.GetComponent<SNKSpaceShip>();
        if(spaceShip != null && spaceShip.targetStation == this)
        {
            RecieveShip(spaceShip);
        }
    }
}
