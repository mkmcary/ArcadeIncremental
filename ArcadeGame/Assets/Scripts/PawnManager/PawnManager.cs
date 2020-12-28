using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager : MonoBehaviour
{
    private static PawnStatus pawnStatus;

    // Location of the game information .json
    private static string pawnStatusPath = Application.dataPath + "/SaveData/PawnStatus.json";
    private static string encryptedPawnStatusPath = Application.dataPath + "/SaveData/EncryptedPawnStatus.json";

    public static PawnStatus readPawnStatus()
    {
        if (pawnStatus == null)
        {
            if (!validFile())
            {
                writePawnStatus();
            }
            else
            {
                /*
                string readIn = System.IO.File.ReadAllText(encryptedPawnStatusPath);
                readIn = GameOperations.EncryptDecrypt(readIn);
                readIn = readIn.Substring(readIn.IndexOf("\n") + 1);
                pawnStatus = JsonUtility.FromJson<PawnStatus>(readIn);
                */
                string readIn = System.IO.File.ReadAllText(pawnStatusPath);
                readIn = readIn.Substring(readIn.IndexOf("\n") + 1);
                pawnStatus = JsonUtility.FromJson<PawnStatus>(readIn);
            }
        }
        return pawnStatus;
    }

    private static bool validFile()
    {
        /*
        return System.IO.File.Exists(encryptedPawnStatusPath) &&
            System.IO.File.ReadAllText(encryptedPawnStatusPath).Contains(GameOperations.EncryptDecrypt("PawnStatus.json\n"));
        */
        return System.IO.File.Exists(pawnStatusPath) &&
            System.IO.File.ReadAllText(pawnStatusPath).Contains("PawnStatus.json\n");
    }

    public static void writePawnStatus()
    {
        if (pawnStatus == null)
        {
            pawnStatus = new PawnStatus();
        }
        System.IO.Directory.CreateDirectory(Application.dataPath + "/SaveData");
        string unencrypted = "PawnStatus.json\n" + JsonUtility.ToJson(pawnStatus, true);
        string encrypted = GameOperations.EncryptDecrypt(unencrypted);
        System.IO.File.WriteAllText(pawnStatusPath, unencrypted);
        System.IO.File.WriteAllText(encryptedPawnStatusPath, encrypted);
    }

    public static void recordTimeStamp()
    {
        pawnStatus.TimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
    }
}
