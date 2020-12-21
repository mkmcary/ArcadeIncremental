using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager : MonoBehaviour
{
    private static PawnStatus pawnStatus;

    // Location of the game information .json
    private static string pawnStatusPath = Application.dataPath + "/SaveData/PawnStatus.json";

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
                string readIn = System.IO.File.ReadAllText(pawnStatusPath);
                readIn = readIn.Substring(readIn.IndexOf("\n") + 1);
                pawnStatus = JsonUtility.FromJson<PawnStatus>(readIn);
            }
        }
        return pawnStatus;
    }

    private static bool validFile()
    {
        return System.IO.File.Exists(pawnStatusPath) && System.IO.File.ReadAllText(pawnStatusPath).Contains("PawnStatus.json\n");
    }

    public static void writePawnStatus()
    {
        if (pawnStatus == null)
        {
            pawnStatus = new PawnStatus();
        }
        System.IO.Directory.CreateDirectory(Application.dataPath + "/SaveData");
        System.IO.File.WriteAllText(pawnStatusPath, "PawnStatus.json\n" + JsonUtility.ToJson(pawnStatus, true));
    }
}
