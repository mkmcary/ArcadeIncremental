using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[Serializable]
public abstract class LayerZeroStatus
{
    [SerializeField]
    private BigIntWrapper tickets;
    private List<ShopUpgrade> upgrades;

    public BigInteger Tickets
    {
        get { return tickets.value; }
        set { tickets.value = value; }
    }

    public List<ShopUpgrade> Upgrades
    {
        get { return upgrades; }
        set { upgrades = value; }
    }

    public LayerZeroStatus()
    {
        tickets = new BigIntWrapper();
        upgrades = new List<ShopUpgrade>();
    }

    public abstract LayerZeroStatus ResetButPreserve();
}
