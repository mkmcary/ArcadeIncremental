using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeManager : MonoBehaviour
{
    private ArcadeStatus arcadeStatus;

    string appPath;

    // Start is called before the first frame update
    void Start()
    {
        appPath = Application.dataPath + "/SaveData/ArcadeStatus.json";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void readFile()
    {
        if (System.IO.File.Exists(appPath))
        {
            string text = System.IO.File.ReadAllText(appPath);
            arcadeStatus = JsonUtility.FromJson<ArcadeStatus>(text);
        }
        else
        {
            arcadeStatus = new ArcadeStatus();
        }
    }
}
