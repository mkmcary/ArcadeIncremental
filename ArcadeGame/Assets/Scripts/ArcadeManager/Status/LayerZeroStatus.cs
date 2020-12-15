using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LayerZeroStatus
{
    public long tickets;
    public List<ShopUpgrade> upgrades;

    public LayerZeroStatus()
    {
        tickets = 0;
        upgrades = new List<ShopUpgrade>();
    }
}
