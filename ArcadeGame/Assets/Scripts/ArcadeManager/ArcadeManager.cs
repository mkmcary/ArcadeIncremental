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
        QMGMenu = 2,
        KNGMenu = 3,
        ticketMenuScreen = 0
    }

    public static menuScreen activeScreen;

    public static ArcadeStatus ReadArcadeStatus()
    {
        if(arcadeStatus == null)
        {
            if (!ValidFile())
            {
                WriteArcadeStatus();
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

    private static bool ValidFile()
    {
        /*
        return System.IO.File.Exists(encryptedArcadeStatusPath) &&
            System.IO.File.ReadAllText(encryptedArcadeStatusPath).Contains(GameOperations.EncryptDecrypt("ArcadeStatus.json\n"));
        */
        return System.IO.File.Exists(arcadeStatusPath) &&
            System.IO.File.ReadAllText(arcadeStatusPath).Contains("ArcadeStatus.json\n");
    }

    public static void WriteArcadeStatus()
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
