using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public abstract class SPCTarget : MonoBehaviour
{
    public long pointValue;
    public bool isShooter;
    public GameObject projectilePrefab;

    protected bool isMoving;
    protected bool isShooting;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isMoving = false;
        isShooting = false;
    }

    // Update is called once per frame
    protected void Update()
    {
        if(!isMoving)
            StartCoroutine(Move());
        if(isShooter && !isShooting)
            StartCoroutine(Shoot());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SPCProjectile proj = collision.gameObject.GetComponent<SPCProjectile>();
        if(proj != null)
        {
            if (proj.PlayerProjectile)
            {
                // give points (game controller)
                Debug.Log("You earned " + pointValue + " points");

                // destroy (maybe a custom method to play animation?)
                GameObject.Destroy(proj.gameObject);
                GameObject.Destroy(gameObject);
            }
        }
    }


    protected virtual IEnumerator Move()
    {
        // we shouldnt call this again until done.
        isMoving = true;

        // move down
        UnityEngine.Vector3 start = transform.position;
        UnityEngine.Vector3 target = start + new UnityEngine.Vector3(0, -1, 0);
        for(float i = 0f; i < 1f; i += 0.01f)
        {
            transform.position = UnityEngine.Vector3.Lerp(start, target, i);
            yield return new WaitForSeconds(0.01f);
        }

        // move back up
        UnityEngine.Vector3 temp = target;
        target = start;
        start = temp;
        for (float i = 0f; i < 1f; i += 0.01f)
        {
            transform.position = UnityEngine.Vector3.Lerp(start, target, i);
            yield return new WaitForSeconds(0.01f);
        }

        // we can move again
        isMoving = false;
    }

    protected abstract IEnumerator Shoot();
}
