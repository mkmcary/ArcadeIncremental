using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QMGButtonTask : MonoBehaviour
{ 
    public abstract void Activate();

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
