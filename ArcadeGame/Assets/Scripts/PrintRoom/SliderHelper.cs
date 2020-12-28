using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class SliderHelper : MonoBehaviour
{
    public Slider slider;
    public Text valueText;

    public void Populate(BigInteger number, int currentLevel, int maxLevel)
    {
        // display number
        valueText.text = GameOperations.bigIntToString(number);

        // do scaling for the slider
        slider.value = ((float) currentLevel) / ((float) maxLevel);
    }

    public void Populate(int number, int currentLevel, int maxLevel)
    {
        // display number
        valueText.text = GameOperations.bigIntToString(new BigInteger(number));

        // do scaling for the slider
        slider.value = ((float)currentLevel) / ((float)maxLevel);
    }

    public void LogarithmicPopulate(BigInteger number)
    {
        valueText.text = GameOperations.bigIntToString(number);

        double value = BigInteger.Log10(number);
        value = value / 10f;
        value += 0.1f;

        if(value < 1f)
        {
            slider.value = (float) value;
        } else
        {
            // do further scaling (next overflow tier)
            Debug.Log("Next overflow tier should happen");
        }
        
    }

    public void timePopulate(float number)
    {
        valueText.text = number + " s";

        // 600s -> 1 bar = 0.1
        if(number == 600f) {
            slider.value = 0.1f;
        }

        // 450s -> 2 bars
        if (number == 450f)
        {
            slider.value = 0.2f;
        }
        // 300s -> 3 bars
        if (number == 300f)
        {
            slider.value = 0.3f;
        }
        // 210s -> 4 bars
        if (number == 210f)
        {
            slider.value = 0.4f;
        }
        // 150s -> 5 bars
        if (number == 150f)
        {
            slider.value = 0.5f;
        }
        // 90s -> 6 bars
        if (number == 90f)
        {
            slider.value = 0.6f;
        }
        // 60s -> 7 bars
        if (number == 60f)
        {
            slider.value = 0.7f;
        }
        // 30s -> 8 bars
        if (number == 30f)
        {
            slider.value = 0.8f;
        }
        // 15s -> 9 bars
        if (number == 15f)
        {
            slider.value = 0.9f;
        }
        // 5s -> 10 bars = 1
        if (number == 5f)
        {
            slider.value = 1f;
        }
    }
}
