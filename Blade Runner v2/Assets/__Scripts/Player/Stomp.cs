using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    [SerializeField] GameObject deathEffect;
    [SerializeField] int damage = 1;
    PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && transform.position.y > collision.transform.position.y)
        {
            collision.GetComponent<HealthController>().TakeDamage(damage);
            playerMovement.Bounce();
        }
    }
}
