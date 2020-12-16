using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class TicketMenuController : MonoBehaviour
{
    public Text prizeText;

    // Start is called before the first frame update
    void Start()
    {
        BigInteger prizeTickets = ArcadeManager.readArcadeStatus().prizeStatus.tickets.value;
        prizeText.text = ArcadeManager.bigIntToString(prizeTickets);
    }
}
