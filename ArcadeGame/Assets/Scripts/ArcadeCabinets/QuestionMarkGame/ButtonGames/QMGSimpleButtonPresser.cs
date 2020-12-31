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
    public int xMax = 835;
    public int yMax = 415;

    // game values
    private int numTimesPressed;
    private int targetPresses;
    
    public override void Activate()
    {
        gameObject.SetActive(true);

        RandomizePosition();
        numTimesPressed = 0;
        targetPresses = Random.Range(minClicks, maxClicks);
        pressesText.text = "Presses Remaining: " + targetPresses;
    }

    private void RandomizePosition()
    {
        int x = Random.Range(-xMax, xMax);
        int y = Random.Range(-yMax, yMax);
        button.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
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
