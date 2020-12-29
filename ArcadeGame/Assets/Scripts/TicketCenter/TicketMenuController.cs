using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class TicketMenuController : MonoBehaviour
{
    public Text prizeText;

    public void Activate()
    {
        gameObject.SetActive(true);
        BigInteger prizeTickets = ArcadeManager.readArcadeStatus().ArcadePrizeStatus.Tickets;
        prizeText.text = GameOperations.bigIntToString(prizeTickets);
    }
}
