using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PrizeUpgrade : ShopUpgrade
{
    [SerializeField]
    private BigIntWrapper moneyValue;

    public BigIntWrapper MoneyValue
    {
        get { return moneyValue; }
        set { moneyValue = value; }
    }

    public PrizeUpgrade() : base()
    {
        moneyValue = new BigIntWrapper();
    }
}
