using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class SPCEnemyTroop : MonoBehaviour
{
    // point value
    protected long pointValue = 100;
    // troop health
    protected float health = 1;
    // rigidbody
    protected Rigidbody2D rb;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /**
     * Handle collisions with player projectiles and explosions.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // deal with player projectiles
        SPCProjectile proj = collision.gameObject.GetComponent<SPCProjectile>();
        if (proj != null && proj.PlayerProjectile)
        {
            health--;
            GameObject.Destroy(proj.gameObject);
        }

        // deal with explosions
        SPCExplosion explosion = collision.gameObject.GetComponent<SPCExplosion>();
        if (explosion != null)
        {
            health -= explosion.damage;
        }

        // handle potential death condition
        if (health <= 0)
        {
            // give points (game controller)
            Debug.Log("You earned " + pointValue + " points");

            // die
            Die();
        }
    }

    /**
     * This will allow for any special conditions to occur on death.
     * Ex: animations, explosions.
     */
    protected abstract void Die();
}
