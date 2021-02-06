using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNKSpaceStation : MonoBehaviour
{

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        // TODO: Finish this method to give points and animation
        FindObjectOfType<SNKPlayerController>().AddBodySegment();
        GameObject.Destroy(gameObject);
    }
}
