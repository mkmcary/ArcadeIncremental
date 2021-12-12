using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class SNKHealthBar : MonoBehaviour
{
    private BigInteger currentHealth;
    private BigInteger maxHealth;

    public Gradient gradient;
    private Slider barUI;
    public Image fill;
    public Image background;
    public Text HealthText;

    // Start is called before the first frame update
    void Start()
    {
        barUI = GetComponent<Slider>();
        maxHealth = FindObjectOfType<SNKPlayerController>().healthPerSegment;
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Update is called once per frame
    private void UpdateHealthBar()
    {        
        barUI.value = GameOperations.BigIntDivideToFloat(currentHealth, maxHealth);

        Color gradColor = gradient.Evaluate(barUI.value);
        fill.color = gradColor;

        Color backColor = new Color(gradColor.r * .7f, gradColor.g * .7f, gradColor.b * .7f);
        background.color = backColor;

        HealthText.text = GameOperations.BigIntToString(currentHealth);
    }

    public void IncrementMaxHealth(BigInteger increment)
    {
        maxHealth += increment;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        IncrementCurrentHealth(increment);
    }

    public void IncrementCurrentHealth(BigInteger increment)
    {
        currentHealth += increment;
        UpdateHealthBar();

        if (IsDead())
        {
            FindObjectOfType<SNKGameController>().EndGame();
        }
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
