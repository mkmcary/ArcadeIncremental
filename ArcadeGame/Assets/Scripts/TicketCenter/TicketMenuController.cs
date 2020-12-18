using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class TicketMenuController : MonoBehaviour
{
    public Text prizeText;

    public void Awake()
    {
        Activate();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        BigInteger prizeTickets = ArcadeManager.readArcadeStatus().prizeStatus.tickets.value;
        prizeText.text = ArcadeManager.bigIntToString(prizeTickets);
    }
}
