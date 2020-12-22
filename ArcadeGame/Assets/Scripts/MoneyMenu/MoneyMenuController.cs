using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class MoneyMenuController : MonoBehaviour
{
    private PawnStatus pawnStatus;

    // Money in the wallet menu
    public Text moneyText;

    [Header("Pop Up")]
    // PopUp
    public GameObject popUp;
    public Text descriptionText;

    [Header("Breakdown")]
    public GameObject breakDown;

    // data on the trade
    private BigInteger moneyToReceive;
    public static BigInteger PRIZE_TICKET_RATIO = 100;
    public static BigInteger GAME_TICKET_RATIO = 1000;
    private ArcadeStatus arcadeStatus;

    public void Activate()
    {
        gameObject.SetActive(true);
        pawnStatus = PawnManager.readPawnStatus();
        arcadeStatus = ArcadeManager.readArcadeStatus();
        moneyText.text = GameOperations.bigIntToString(pawnStatus.Money.value);
    }

    public void initializePopUp()
    {
        // calculate the money to receive
        calculateMoneyToReceive();

        popUp.SetActive(true);
        descriptionText.text = "Best I Can Do Is:\n$" + GameOperations.bigIntToString(moneyToReceive);
    }

    private void calculateMoneyToReceive()
    {
        moneyToReceive = 0;

        // tickets -> money
        List<LayerZeroStatus> gameStatuses = arcadeStatus.getLayerZeroStatuses();
        // generic tickets are worth more
        moneyToReceive += (gameStatuses[0].Tickets.value / PRIZE_TICKET_RATIO);

        // game tickets are all the same value
        for (int i = 1; i < gameStatuses.Count; i++)
        {
            moneyToReceive += (gameStatuses[i].Tickets.value / GAME_TICKET_RATIO);
        }

        // prizes -> money
        List<ShopUpgrade> prizes = gameStatuses[0].Upgrades;
        for(int i = 0; i < prizes.Count; i++)
        {
            PrizeUpgrade prize = (PrizeUpgrade)prizes[i];
            if (prize.currentLevel != 0)
            {
                moneyToReceive += prize.MoneyValue.value;
            }
        }
    }

    public void closePopUp()
    {
        closeBreakdown();
        popUp.SetActive(false);
    }

    public void initializeBreakdown()
    {
        breakDown.SetActive(true);

        // do whatever is needed to display breakdown data
        // show game tickets

        // show prize tickets

        // show prizes
    }

    public void closeBreakdown()
    {
        breakDown.SetActive(false);
    }

    public void acceptTrade()
    {
        // get money
        pawnStatus.Money.value += moneyToReceive;
        moneyToReceive = 0;

        // reset layer 0
        arcadeStatus.resetButPreserve();
        ArcadeManager.writeArcadeStatus();

        Debug.Log("According to moneymenu: " + arcadeStatus.prizeStatus.Upgrades[0].currentLevel);
        Debug.Log("According to arcademanager: " + ArcadeManager.readArcadeStatus().prizeStatus.Upgrades[0].currentLevel);

        // re-init
        Activate();

        // close popup
        closePopUp();
    }




}
