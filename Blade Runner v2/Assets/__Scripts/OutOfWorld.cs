using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfWorld : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealthController>(out PlayerHealthController playerHealth))
        {
            playerHealth.TakeDamage(playerHealth.GetMaxHealth());
        }
    }
}
