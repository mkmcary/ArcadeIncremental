using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[Serializable]
public class LayerZeroStatus
{
    public BigIntWrapper tickets;
    private List<ShopUpgrade> upgrades;

    public LayerZeroStatus()
    {
        tickets = new BigIntWrapper();
        upgrades = new List<ShopUpgrade>();
    }

    public List<ShopUpgrade> getUpgrades()
    {
        return upgrades;
    }
}
