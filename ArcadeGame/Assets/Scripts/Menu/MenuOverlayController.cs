using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOverlayController : MonoBehaviour
{
    [Header("Categories of the Overlay")]
    public GameObject overlayWhenClosed;
    public GameObject overlayWhenOpen;

    [Header("Valid Landing Locations")]
    public GameObject ticketCenterMenu;
    public GameObject pawnShopMenu;

    [Header("Currency View")]
    public GameObject ticketLinePrefab;
    public RectTransform scrollViewTransform;
    public Text moneyText;
    public Text prizeTicketText;

    // items related to the currency view
    private List<GameObject> lineItems;
    private ArcadeStatus arcadeStatus;
    private PawnStatus pawnStatus;

    [Header("Tabs")]
    public GameObject settingsTab;
    public GameObject achievementsTab;
    public GameObject questsTab;

    // Start is called before the first frame update
    void Start()
    {
        CloseMenu();
    }

    public void OpenMenu()
    {
        InitializeCurrencyView();
        OpenSettings();

        overlayWhenClosed.SetActive(false);
        overlayWhenOpen.SetActive(true);
    }

    public void CloseMenu()
    {
        if(lineItems != null)
        {
            for(int i = 0; i < lineItems.Count; i++)
            {
                GameObject.Destroy(lineItems[i]);
            }
        }
        

        overlayWhenClosed.SetActive(true);
        overlayWhenOpen.SetActive(false);
    }

    public void GoToTicketCenter()
    {
        MenuManager.HideAllScreens();
        ticketCenterMenu.GetComponent<TicketMenuController>().Activate();
        CloseMenu();
    }

    public void GoToPawnShop()
    {
        MenuManager.HideAllScreens();
        pawnShopMenu.GetComponent<MoneyMenuController>().Activate();
        CloseMenu();
    }

    public void InitializeCurrencyView()
    {
        arcadeStatus = ArcadeManager.ReadArcadeStatus();
        pawnStatus = PawnManager.ReadPawnStatus();

        // Set the static currencies
        moneyText.text = GameOperations.BigIntToString(pawnStatus.Money);
        prizeTicketText.text = GameOperations.BigIntToString(arcadeStatus.ArcadePrizeStatus.Tickets);

        // Get the list of statuses to complete dynamic currencies
        int xPos = 0;
        int yPos = -10;
        int yOffset = -70;
        List<LayerZeroStatus> statuses = arcadeStatus.Statuses;
        lineItems = new List<GameObject>();
        for(int i = 1; i < statuses.Count; i++)
        {
            CabinetStatus status = (CabinetStatus)statuses[i];
            if (status.IsActive)
            {
                // Add prefab
                // Make the item
                GameObject lineItem = Instantiate(ticketLinePrefab);
                lineItem.transform.SetParent(scrollViewTransform, false);
                lineItems.Add(lineItem);

                // Set position
                RectTransform rt = lineItem.GetComponent<RectTransform>();
                rt.anchorMin = new UnityEngine.Vector2(0, 0);
                rt.anchorMax = new UnityEngine.Vector2(1, 1);
                rt.sizeDelta = new Vector2(200, 50);
                rt.anchoredPosition = new UnityEngine.Vector3(xPos, yPos, 0);

                yPos += yOffset;

                //Populate the prefab with the proper data.
                lineItem.GetComponent<CurrencyViewUI>().Populate(status);
            }
        }
        int newHeight = Mathf.Max(-yPos, 720);
        scrollViewTransform.sizeDelta = new Vector2(225, newHeight);
    }

    private void CloseAllTabs()
    {
        settingsTab.SetActive(false);
        achievementsTab.SetActive(false);
        questsTab.SetActive(false);
    }

    public void OpenSettings()
    {
        CloseAllTabs();
        settingsTab.SetActive(true);
    }

    public void OpenAchievements()
    {
        CloseAllTabs();
        achievementsTab.SetActive(true);
    }

    public void OpenQuests()
    {
        CloseAllTabs();
        questsTab.SetActive(true);
    }

}
