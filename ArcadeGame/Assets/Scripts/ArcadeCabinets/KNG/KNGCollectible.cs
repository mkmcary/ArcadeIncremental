using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KNGCollectible : MonoBehaviour
{
    private KNGGameController gameController;

    public int basePoints;

    private bool interactable;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        gameController = GameObject.FindObjectOfType<KNGGameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactable = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (interactable)
        {
            gameController.CollectPoints(basePoints);
            SetInteractiblity(false);
        }
    }

    public void SetInteractiblity(bool interactable)
    {
        this.interactable = interactable;
        spriteRenderer.enabled = this.interactable;
    }
}
