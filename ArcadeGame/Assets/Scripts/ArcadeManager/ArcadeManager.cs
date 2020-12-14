using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeManager : MonoBehaviour
{
    public static ArcadeStatus readArcadeStatus()
    {
        string appPath = Application.dataPath + "/SaveData/ArcadeStatus.json";
        if (!System.IO.File.Exists(appPath))
        {
            ArcadeStatus arcadeStatus = new ArcadeStatus();
            System.IO.File.WriteAllText(appPath, JsonUtility.ToJson(arcadeStatus, true));
            return arcadeStatus;
        }
        else
        {
            string readIn = System.IO.File.ReadAllText(appPath);
            return JsonUtility.FromJson<ArcadeStatus>(readIn);
        }
    }

    public static void writeArcadeStatus(ArcadeStatus status)
    {
        string appPath = Application.dataPath + "/SaveData/ArcadeStatus.json";
        System.IO.File.WriteAllText(appPath, JsonUtility.ToJson(status, true));
    }
}
