using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    [SerializeField] GameObject deathEffect;
    [SerializeField] int damage = 1;
    [SerializeField] float offset = 0.5f;
    PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && transform.position.y > collision.transform.position.y + offset)
        {
            collision.GetComponent<HealthController>().TakeDamage(damage);
            playerMovement.Bounce();
        }

        if (collision.tag == "Box" && transform.position.y > collision.transform.position.y + offset)
        {
            collision.GetComponent<HealthController>().TakeDamage(damage);
            float bounce = collision.GetComponent<Box>().GetBounce();
            playerMovement.Bounce(bounce);
        }
    }

    public void Bounce()
    {
        playerMovement.Bounce();
    }
}
