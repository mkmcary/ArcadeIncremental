using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[Serializable]
public class BigIntWrapper
{
    public BigInteger value;
    [SerializeField]
    private string valueString;

    public BigIntWrapper()
    {
        value = 0;
        valueString = "0";
    }

    public BigIntWrapper(BigInteger num)
    {
        value = num;
        valueString = num.ToString();
    }

    public void updateValueString()
    {
        valueString = value.ToString();
    }

    public void initializeValue()
    {
        value = BigInteger.Parse(valueString);
    }

}
