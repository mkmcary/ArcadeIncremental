using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNKSpaceShip : MonoBehaviour
{
    private float speed;

    private SNKSpaceStationSpawner stationSpawner;

    public SNKSpaceStation targetStation;
    public SNKSpaceStation homeStation;

    private Rigidbody2D rigid;

    // Update is called once per frame
    void Update()
    {
        if(targetStation == null)
        {
            GoHome();
        }
    }

    public void StartMoving(SNKSpaceStation home)
    {
        speed = 3f;
        rigid = GetComponent<Rigidbody2D>();

        stationSpawner = FindObjectOfType<SNKSpaceStationSpawner>();

        if (homeStation != null)
        {
            homeStation.ShipCount--;
        }
        homeStation = home;
        SelectNextTarget();
        StartCoroutine(WaitThenMove());
    }

    private IEnumerator WaitThenMove()
    {
        rigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(2);
        MoveToTarget();
    }

    private void SelectNextTarget()
    {
        if(targetStation != null)
        {
            homeStation = targetStation;
        }
        
        SNKSpaceStation temp = homeStation;
        while(temp == homeStation)
        {
            temp = stationSpawner.GetRandomStation();
        }
        targetStation = temp;
    }

    private void LookAtTarget()
    {
        Vector3 diff = targetStation.transform.position - transform.position;
        diff.Normalize();

        float rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90);
    }

    private void MoveToTarget()
    {
        LookAtTarget();
        rigid.velocity = new Vector2(transform.up.x * speed, transform.up.y * speed);
    }

    private void GoHome()
    {
        targetStation = homeStation;
        MoveToTarget();
    }

    private void Die()
    {
        if (homeStation != null)
        {
            homeStation.ShipCount--; 
        }
               
        FindObjectOfType<SNKPlayerController>().AddBodySegment();
        GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<SNKPlayerBodySegment>() != null)
        {
            Die();
        }
    }
}
