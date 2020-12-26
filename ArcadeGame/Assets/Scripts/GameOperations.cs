using System.Numerics;
using System.Text;
using UnityEngine;

public class GameOperations : MonoBehaviour
{
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
        for (int i = 0; i < exponent.Length; i++)
        {
            if (exponent[i] != '0' || reachedNonZero)
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

    public static Sprite loadSpriteFromPath(string path)
    {
        return Resources.Load<Sprite>(path);
    }

    /**
     * Simple XOR Encryption for output to files.
     */
    public static string EncryptDecrypt(string textToEncrypt)
    {
        int key = 129;
        StringBuilder inSb = new StringBuilder(textToEncrypt);
        StringBuilder outSb = new StringBuilder(textToEncrypt.Length);
        char c;
        for (int i = 0; i < textToEncrypt.Length; i++)
        {
            c = inSb[i];
            c = (char)(c ^ key);
            outSb.Append(c);
        }
        return outSb.ToString();
    }
}
