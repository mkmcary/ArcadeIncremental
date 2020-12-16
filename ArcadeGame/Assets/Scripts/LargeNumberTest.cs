using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class LargeNumberTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BigInteger bigInt = BigInteger.Parse("123456789123456789");
        Debug.Log("toString: " + bigInt.ToString("e3"));
        Debug.Log("Our Method: " + ArcadeManager.bigIntToString(bigInt));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
