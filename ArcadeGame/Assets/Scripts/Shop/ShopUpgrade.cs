using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[Serializable]
public class ShopUpgrade
{
    // Data For a ShopUpgrade
    public string sprite;
    public BigIntWrapper price;
    public string upgradeName;
    public string description;
    public int currentLevel;
    public int maxLevel;
    public BigInteger priceScale;
    public ScaleType sType;

    public enum ScaleType
    {
        ADD, MULT, EXP
    }

    public ShopUpgrade()
    {
        sprite = null;
        price = new BigIntWrapper();
        upgradeName = null;
        description = null;
        currentLevel = 0;
        maxLevel = 1;
        priceScale = 1;
        sType = ScaleType.MULT;
    }

    public void LevelUp()
    {
        if (currentLevel == maxLevel)
        {
            // error message   
        }
        else
        {
            currentLevel++;
            scalePrice();
        }
    }

    private void scalePrice()
    {
        if(sType == ScaleType.MULT)
        {
            price.value = (price.value * priceScale);
        } else if(sType == ScaleType.ADD)
        {
            price.value += priceScale;
        } else if(sType == ScaleType.EXP)
        {
            // assuming that we will not have to deal with overflow on exponential scaling
            BigInteger temp = BigInteger.Pow(price.value, (int)priceScale);
            if(temp == price.value)
            {
                price.value = temp + 1;
            } else
            {
                price.value = temp;
            }
        }
    }
}
