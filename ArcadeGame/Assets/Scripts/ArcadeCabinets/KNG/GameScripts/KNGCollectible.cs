using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KNGCollectible : MonoBehaviour
{
    private KNGGameController gameController;

    public int basePoints;

    private bool interact;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        gameController = GameObject.FindObjectOfType<KNGGameController>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        interact = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        KNGPlayerController player = collision.gameObject.GetComponent<KNGPlayerController>();
        if (player != null)
        {
            if (this.interact)
            {
                gameController.CollectPoints(basePoints);
                SetInteractiblity(false);
            }
        }
    }

    public void SetInteractiblity(bool interactable)
    {
        interact = interactable;
        if(spriteRenderer == null)
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        spriteRenderer.enabled = interact;
    }
}
