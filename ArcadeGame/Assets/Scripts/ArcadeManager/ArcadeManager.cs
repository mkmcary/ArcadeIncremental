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

    public static string convertToScientific(long number)
    {
        if(number < 10000000)
        {
            return number.ToString();
        }

        string digits = number.ToString();

        if(digits.Length - 1 > 9)
        {
            // remove a digit from the decimal if exponent is 2 digits
            return digits[0] + "." + digits.Substring(1, 2) + "e" + (digits.Length - 1);
        }

        return digits[0] + "." + digits.Substring(1, 3) + "e" + (digits.Length - 1);
    }

    public static Sprite loadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }
}
