using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketMenuController : MonoBehaviour
{
    public Text prizeText;

    // Start is called before the first frame update
    void Start()
    {
        long prizeTickets = ArcadeManager.readArcadeStatus().prizeStatus.tickets;
        prizeText.text = ArcadeManager.convertToScientific(prizeTickets);
    }
}
