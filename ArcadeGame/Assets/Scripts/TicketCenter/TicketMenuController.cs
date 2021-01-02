using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class TicketMenuController : MonoBehaviour
{
    public Text prizeText;

    private void Start()
    {
        Activate();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        BigInteger prizeTickets = ArcadeManager.ReadArcadeStatus().ArcadePrizeStatus.Tickets;
        prizeText.text = GameOperations.BigIntToString(prizeTickets);
    }
}
