using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopUpgrade
{
    // Data For a ShopUpgrade
    public string sprite;
    public long price;
    public string upgradeName;
    public string description;
    public int currentLevel;
    public int maxLevel;
    public float priceScale;
    public scaleType sType;

    public enum scaleType
    {
        ADD, MULT, EXP
    }

    public ShopUpgrade()
    {
        sprite = null;
        price = 0;
        upgradeName = null;
        description = null;
        currentLevel = 0;
        maxLevel = 1;
        priceScale = 1;
        sType = scaleType.MULT;
    }

    public ShopUpgrade(string sprite, long price, string uName, string description, int currentLevel, int maxLevel, float priceScale, scaleType sType)
    {
        this.sprite = sprite;
        this.price = price;
        this.upgradeName = uName;
        this.description = description;
        this.currentLevel = currentLevel;
        this.maxLevel = maxLevel;
        this.priceScale = priceScale;
        this.sType = sType;
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
            Debug.Log("Leveled up: " + upgradeName + ", level is now " + currentLevel);
            scalePrice();
        }
    }

    private void scalePrice()
    {
        if(sType == scaleType.MULT)
        {
            price = (long)((float)price * priceScale);
        } else if(sType == scaleType.ADD)
        {
            price += (long)priceScale;
        } else if(sType == scaleType.EXP)
        {
            long temp = (long)Mathf.Pow((float)price, priceScale);
            if(temp == price)
            {
                price = temp + 1;
            } else
            {
                price = temp;
            }
        }
    }
}
