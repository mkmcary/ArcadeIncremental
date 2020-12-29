using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MoneyMenuController : MonoBehaviour
{
    private PawnStatus pawnStatus;
    private ArcadeStatus arcadeStatus;

    // Money in the wallet menu
    public Text moneyText;

    [Header("Pop Up")]
    // PopUp
    public GameObject popUp;
    public Text descriptionText;

    [Header("Breakdown")]
    public GameObject breakDown;
    public Text gameTicketsText;
    public Text prizeTicketsText;
    public Text prizesText;
    public GameObject lineItemPrefab;
    public GameObject totalPrefab;
    public GameObject contentTab;

    // data on the trade
    public static BigInteger PRIZE_TICKET_RATIO = 100;
    public static BigInteger GAME_TICKET_RATIO = 1000;
    private BigInteger moneyToReceive;
    private BigInteger moneyFromGameTickets;
    private BigInteger moneyFromPrizeTickets;
    private BigInteger moneyFromPrizes;
    private List<PrizeUpgrade> prizesOwned;


    public void Activate()
    {
        gameObject.SetActive(true);
        closePopUp();
        pawnStatus = PawnManager.ReadPawnStatus();
        arcadeStatus = ArcadeManager.readArcadeStatus();
        moneyText.text = GameOperations.bigIntToString(pawnStatus.Money);
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
        List<LayerZeroStatus> gameStatuses = arcadeStatus.Statuses;
        // generic tickets are worth more
        moneyFromPrizeTickets = (gameStatuses[0].Tickets / PRIZE_TICKET_RATIO);

        // game tickets are all the same value
        moneyFromGameTickets = 0;
        for (int i = 1; i < gameStatuses.Count; i++)
        {
            moneyFromGameTickets += (gameStatuses[i].Tickets / GAME_TICKET_RATIO);
        }

        // prizes -> money
        moneyFromPrizes = 0;
        List <ShopUpgrade> prizes = gameStatuses[0].Upgrades;
        prizesOwned = new List<PrizeUpgrade>();
        for(int i = 0; i < prizes.Count; i++)
        {
            PrizeUpgrade prize = (PrizeUpgrade)prizes[i];
            if (prize.currentLevel != 0)
            {
                moneyFromPrizes += prize.MoneyValue.value;
                prizesOwned.Add(prize);
            }
        }

        // ending count
        moneyToReceive = moneyFromGameTickets + moneyFromPrizeTickets + moneyFromPrizes;
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
        gameTicketsText.text = "$" + GameOperations.bigIntToString(moneyFromGameTickets);

        // show prize tickets
        prizeTicketsText.text = "$" + GameOperations.bigIntToString(moneyFromPrizeTickets);

        // show prizes
        prizesText.text = "$" + GameOperations.bigIntToString(moneyFromPrizes);

        // show individual prizes
        int xPos = 50;
        int yPos = -220;
        int yOffset = -70;

        for(int i = 0; i < prizesOwned.Count; i++)
        {
            // make item
            GameObject lineItem =  Instantiate(lineItemPrefab);
            lineItem.transform.SetParent(contentTab.transform, false);

            // set position
            RectTransform rt = lineItem.GetComponent<RectTransform>();
            rt.anchorMin = new UnityEngine.Vector2(0, 0);
            rt.anchorMax = new UnityEngine.Vector2(1, 1);
            rt.anchoredPosition = new UnityEngine.Vector3(xPos, yPos, 0);

            // increment position
            yPos += yOffset;

            // set text
            lineItem.GetComponent<Text>().text = prizesOwned[i].upgradeName;
            lineItem.transform.GetChild(0).GetComponent<Text>().text = "$" + GameOperations.bigIntToString(prizesOwned[i].MoneyValue.value);
        }

        // make total
        GameObject total = Instantiate(totalPrefab);
        total.transform.SetParent(contentTab.transform, false);

        // set position
        RectTransform totalTransform = total.GetComponent<RectTransform>();
        totalTransform.anchorMin = new UnityEngine.Vector2(0, 0);
        totalTransform.anchorMax = new UnityEngine.Vector2(1, 1);
        total.GetComponent<RectTransform>().anchoredPosition = new UnityEngine.Vector3(10, yPos, 0);

        // increment position
        yPos += yOffset;

        // set text
        total.transform.GetChild(0).GetComponent<Text>().text = "$" + GameOperations.bigIntToString(moneyToReceive);

        // adjust 
        RectTransform contentTransform = contentTab.GetComponent<RectTransform>();
        int newHeight = Mathf.Max(-yPos, 680);
        contentTransform.sizeDelta = new UnityEngine.Vector2(850, newHeight);
    }

    public void closeBreakdown()
    {
        breakDown.SetActive(false);
    }

    public void acceptTrade()
    {
        // get money
        pawnStatus.Money += moneyToReceive;
        moneyToReceive = 0;

        // reset layer 0
        arcadeStatus.ResetButPreserve();
        ArcadeManager.writeArcadeStatus();

        // re-init
        Activate();

        // close popup
        closePopUp();
    }




}
