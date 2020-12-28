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
    private static string encryptedArcadeStatusPath = Application.dataPath + "/SaveData/EncryptedArcadeStatus.json";

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
                /*
                string readIn = System.IO.File.ReadAllText(encryptedArcadeStatusPath);
                readIn = GameOperations.EncryptDecrypt(readIn);
                readIn = readIn.Substring(readIn.IndexOf("\n") + 1);
                arcadeStatus = JsonUtility.FromJson<ArcadeStatus>(readIn);
                */
                // Temp readable files
                string readIn = System.IO.File.ReadAllText(arcadeStatusPath);
                readIn = readIn.Substring(readIn.IndexOf("\n") + 1);
                arcadeStatus = JsonUtility.FromJson<ArcadeStatus>(readIn);
            }
        }
        return arcadeStatus;
    }

    private static bool validFile()
    {
        /*
        return System.IO.File.Exists(encryptedArcadeStatusPath) &&
            System.IO.File.ReadAllText(encryptedArcadeStatusPath).Contains(GameOperations.EncryptDecrypt("ArcadeStatus.json\n"));
        */
        return System.IO.File.Exists(arcadeStatusPath) &&
            System.IO.File.ReadAllText(arcadeStatusPath).Contains("ArcadeStatus.json\n");
    }

    public static void writeArcadeStatus()
    {
        if(arcadeStatus == null)
        {
            arcadeStatus = new ArcadeStatus();
        }
        System.IO.Directory.CreateDirectory(Application.dataPath + "/SaveData");
        string unencrypted = "ArcadeStatus.json\n" + JsonUtility.ToJson(arcadeStatus, true);
        string encrypted = GameOperations.EncryptDecrypt(unencrypted);
        System.IO.File.WriteAllText(arcadeStatusPath, unencrypted);
        System.IO.File.WriteAllText(encryptedArcadeStatusPath, encrypted);
    }
}
