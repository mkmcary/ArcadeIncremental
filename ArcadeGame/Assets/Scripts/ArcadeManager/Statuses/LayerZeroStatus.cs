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

    public BigIntWrapper Tickets
    {
        get { return tickets; }
        set { tickets = value; }
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

    public abstract LayerZeroStatus resetButPreserve();
}
