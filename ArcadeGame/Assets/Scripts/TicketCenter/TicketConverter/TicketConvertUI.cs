﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketConvertUI : MonoBehaviour
{
    /** UI Elements for this convert. */
    public Image image;
    public Text nameText;
    public Text inputText;
    public Text outputText;
    public Text countText;

    /** The convert we are displaying in this TicketConvertUI. */
    public TicketConvert activeConvert;

    public void populate()
    {
        gameObject.SetActive(true);
        image.sprite = Resources.Load<Sprite>(activeConvert.sprite);
        nameText.text = activeConvert.gameName;
        inputText.text = GameOperations.bigIntToString(activeConvert.inputAmount);
        outputText.text = GameOperations.bigIntToString(activeConvert.outputAmount);
        countText.text = GameOperations.bigIntToString(activeConvert.getCount());
    }
}
