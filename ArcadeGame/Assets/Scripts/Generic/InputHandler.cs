using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{

    public string targetScene;

    public void handleTouch()
    {
        if(gameObject.CompareTag("SceneButton"))
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}
