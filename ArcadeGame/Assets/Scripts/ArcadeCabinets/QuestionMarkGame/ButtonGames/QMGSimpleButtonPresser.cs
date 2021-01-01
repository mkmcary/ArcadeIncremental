using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QMGSimpleButtonPresser : QMGButtonTask
{
    [Header("Controls")]
    public QMGGameController gameController;
    public GameObject button;
    public Text pressesText;

    [Header("Game Constants")]
    public int minClicks = 5;
    public int maxClicks = 10;
    public List<RectTransform> Locations;

    // game values
    private int numTimesPressed;
    private int targetPresses;
    private int currentPosition;
    
    public override void Activate()
    {
        gameObject.SetActive(true);

        RandomizePosition();
        numTimesPressed = 0;
        currentPosition = 0;
        targetPresses = Random.Range(minClicks, maxClicks);
        pressesText.text = "Presses Remaining: " + targetPresses;
    }

    private void RandomizePosition()
    {
        int nextPosition = Random.Range(0, Locations.Count - 1);
        while(nextPosition == currentPosition)
        {
            nextPosition = Random.Range(0, Locations.Count - 1);
        }
        int x = (int) Locations[nextPosition].localPosition.x;
        int y = (int)Locations[nextPosition].localPosition.y;
        button.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        currentPosition = nextPosition;
    }

    public void OnButtonClick()
    {
        gameController.IncrementScore();
        numTimesPressed++;
        pressesText.text = "Presses Remaining: " + (targetPresses - numTimesPressed);
        if (numTimesPressed == targetPresses)
        {
            // choose a new game
            gameController.ChooseTask();
        }

        RandomizePosition();
    }
}
