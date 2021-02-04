using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPCExplosion : MonoBehaviour
{
    public int damage;
    public float duration;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitBeforeDespawn());
    }

    private IEnumerator WaitBeforeDespawn()
    {
        yield return new WaitForSeconds(duration);
        GameObject.Destroy(gameObject);
    }
}
