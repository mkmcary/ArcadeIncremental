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
                readIn = readIn.Substring(readIn.IndexOf("\n") + 2);
                arcadeStatus = JsonUtility.FromJson<ArcadeStatus>(readIn);
            }
        }
        return arcadeStatus;
    }

    private static bool validFile()
    {
        return System.IO.File.Exists(arcadeStatusPath) && System.IO.File.ReadAllText(arcadeStatusPath).Contains("ArcadeStatus.json\n\n");
    }

    public static void writeArcadeStatus()
    {
        if(arcadeStatus == null)
        {
            arcadeStatus = new ArcadeStatus();
        }
        System.IO.File.WriteAllText(arcadeStatusPath, "ArcadeStatus.json\n\n" + JsonUtility.ToJson(arcadeStatus, true));
    }

    /**
     * Used to format a BigInteger as a string for output to the player.
     * @param number The value to format
     * @return returns the custom formatted string of the number
     */
    public static string bigIntToString(BigInteger number)
    {
        if (number < 10000000)
        {
            // if the number is below a certain size, we just want the number
            return number.ToString();
        }

        // get the scientific notation representation
        string unchanged = number.ToString("e3");
        int indexOfE = unchanged.IndexOf("e");
        string beforeExponent = unchanged.Substring(0, indexOfE + 1);
        string exponent = unchanged.Substring(indexOfE + 2);

        // find if we have zeros - if so, disregard them
        StringBuilder newExp = new StringBuilder();
        bool reachedNonZero = false;
        for(int i = 0; i < exponent.Length; i++)
        {
            if(exponent[i] != '0' || reachedNonZero)
            {
                newExp.Append(exponent[i]);
                reachedNonZero = true;
            }
        }

        // build the final string
        StringBuilder finalString = new StringBuilder();
        finalString.Append(beforeExponent);
        finalString.Append(newExp.ToString());

        // return
        return finalString.ToString();
    }

    public static Sprite loadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }
}
