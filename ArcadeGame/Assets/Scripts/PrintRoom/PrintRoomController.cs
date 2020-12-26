using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintRoomController : MonoBehaviour
{
    private PawnStatus pawnStatus;

    public Text walletText;

    public void Activate()
    {
        pawnStatus = PawnManager.readPawnStatus();
        walletText.text = GameOperations.bigIntToString(pawnStatus.Money.value);
        gameObject.SetActive(true);
    }
}
