using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<HealthController>(out HealthController playerHealth) && collision.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage, true);
        } 
    }
}
