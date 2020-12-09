using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string targetScene;

    public void changeScene()
    {
        SceneManager.LoadScene(targetScene);
    }

    public void changeScene(string target)
    {
        SceneManager.LoadScene(target);
    }
}
