using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyViewUI : MonoBehaviour
{
    public Image currencyImage;
    public Text currencyText;

    public void Populate(CabinetStatus status)
    {
        currencyText.text = GameOperations.BigIntToString(status.Tickets);
        currencyImage.sprite = GetTicketSprite(status);
    }

    private Sprite GetTicketSprite(CabinetStatus status)
    {
        if(status is DebugCabinetStatus)
        {
            return GameOperations.LoadSpriteFromPath("Sprites/Currency/Tickets/DebugTicket");
        }
        if (status is QMGCabinetStatus)
        {
            return GameOperations.LoadSpriteFromPath("Sprites/Currency/Tickets/QMGTicket");
        }
        if (status is KNGCabinetStatus)
        {
            return GameOperations.LoadSpriteFromPath("Sprites/Currency/Tickets/KNGTicket");
        }
        if(status is BRDCabinetStatus)
        {
            return GameOperations.LoadSpriteFromPath("Sprites/Currency/Tickets/BRDTicket");
        }
        // This is bad if this happens double check.
        return null;
    }
}
