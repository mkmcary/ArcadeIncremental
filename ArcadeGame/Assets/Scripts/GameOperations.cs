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
    public static string BigIntToString(BigInteger number)
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

    public static Sprite LoadSpriteFromPath(string path)
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

    /**
     * Takes in two BigIntegers to divide and return a float
     * @param b1 the dividend of the equation
     * @param b2 the divisor of the equation
     * @returns the approximate quotient as a float
     */ 
    public static float BigIntDivideToFloat(BigInteger b1, BigInteger b2)
    {
        string dividend = b1.ToString();
        string divisor = b2.ToString();

        if(dividend.Length > divisor.Length)
        {
            dividend = dividend.Substring(0, dividend.Length - divisor.Length + 1);
            divisor = divisor.Substring(0, 1);
        }
        else
        {
            divisor = divisor.Substring(0, divisor.Length - dividend.Length + 1);
            dividend = dividend.Substring(0, 1);
        }
        return float.Parse(dividend) / float.Parse(divisor);
    }
}
