using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using UnityEngine;

public class ArcadeManager : MonoBehaviour
{
    public static ArcadeStatus readArcadeStatus()
    {
        string appPath = Application.dataPath + "/SaveData/ArcadeStatus.json";
        if (!System.IO.File.Exists(appPath))
        {
            ArcadeStatus arcadeStatus = new ArcadeStatus();
            writeArcadeStatus(arcadeStatus);
            return arcadeStatus;
        }
        else
        {
            string readIn = System.IO.File.ReadAllText(appPath);
            ArcadeStatus ret = JsonUtility.FromJson<ArcadeStatus>(readIn);

            List<LayerZeroStatus> statuses = ret.getLayerZeroStatuses();

            // initialize BigInteger values
            for(int i = 0; i < statuses.Count; i++)
            {
                // tickets
                statuses[i].tickets.initializeValue();

                // upgrades
                List<ShopUpgrade> upgrades = statuses[i].getUpgrades();
                for (int j = 0; j < upgrades.Count; j++)
                {
                    upgrades[j].price.initializeValue();
                }

                // score
                if (i != 0)
                {
                    CabinetStatus cabStatus = (CabinetStatus)statuses[i];
                    cabStatus.cumulativeScore.initializeValue();
                    cabStatus.highScore.initializeValue();
                }
            }

            return ret;
        }
    }

    public static void writeArcadeStatus(ArcadeStatus status)
    {
        // handle updating the BigInteger strings
        List<LayerZeroStatus> statuses = status.getLayerZeroStatuses();
        for (int i = 0; i < statuses.Count; i++)
        {
            // tickets
            statuses[i].tickets.updateValueString();

            // upgrades
            List<ShopUpgrade> upgrades = statuses[i].getUpgrades();
            for (int j = 0; j < upgrades.Count; j++)
            {
                upgrades[j].price.updateValueString();
            }

            // score
            if (i != 0)
            {
                CabinetStatus cabStatus = (CabinetStatus)statuses[i];
                cabStatus.cumulativeScore.updateValueString();
                cabStatus.highScore.updateValueString();
            }
        }

        string appPath = Application.dataPath + "/SaveData/ArcadeStatus.json";
        System.IO.File.WriteAllText(appPath, JsonUtility.ToJson(status, true));
    }

    /**
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
    */

    /**
     * Used to format a BigInteger as a string for output to the player.
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
