using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using UnityEngine;

public class ArcadeManager : MonoBehaviour
{
    private static ArcadeStatus arcadeStatus;

    // Location of the game information .json
    private static string arcadeStatusPath = Application.dataPath + "/SaveData/ArcadeStatus.json";

    public enum menuScreen {
        debugGameMenu = 1, 
        cabinetScreen = 0
    }

    public static menuScreen activeScreen;

    public static ArcadeStatus readArcadeStatus()
    {
        if(arcadeStatus == null)
        {
            if (!validFile())
            {
                writeArcadeStatus();
            }
            else
            {
                string readIn = System.IO.File.ReadAllText(arcadeStatusPath);
                readIn = readIn.Substring(readIn.IndexOf("\n") + 1);
                arcadeStatus = JsonUtility.FromJson<ArcadeStatus>(readIn);
            }
        }
        return arcadeStatus;
    }

    private static bool validFile()
    {
        return System.IO.File.Exists(arcadeStatusPath) && System.IO.File.ReadAllText(arcadeStatusPath).Contains("ArcadeStatus.json\n");
    }

    public static void writeArcadeStatus()
    {
        if(arcadeStatus == null)
        {
            arcadeStatus = new ArcadeStatus();
        }
        System.IO.Directory.CreateDirectory(Application.dataPath + "/SaveData");
        System.IO.File.WriteAllText(arcadeStatusPath, "ArcadeStatus.json\n" + JsonUtility.ToJson(arcadeStatus, true));
    }
}
