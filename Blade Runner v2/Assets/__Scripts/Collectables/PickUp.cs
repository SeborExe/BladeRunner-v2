using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] Pickup pickupType;
    [SerializeField] GameObject pickUpEffect;
    [SerializeField] int gemsAmount = 1;
    [SerializeField] int healthToRecive;

    private bool isCollected = false;

    private void Start()
    {
        if (pickupType == Pickup.Gem)
        {
            UIController.Instance.AddGem(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCollected)
        {
            switch(pickupType)
            {
                case Pickup.Gem:
                    GameManager.Instance.AddGem();
                    isCollected = true;
                    UIController.Instance.UpdateGemCount();
                    gameObject.SetActive(false);
                    break;

                case Pickup.Health:
                    PlayerHealthController playerHealthController = collision.GetComponent<PlayerHealthController>();
                    if (playerHealthController.GetCurrentHealth() == playerHealthController.GetMaxHealth()) return;
                    playerHealthController.HealPlayer(healthToRecive);
                    UIController.Instance.UpdateHealthDisplay();
                    isCollected = true;
                    gameObject.SetActive(false);
                    break;
            }

            Instantiate(pickUpEffect, transform.position, Quaternion.identity);
        }
    }

    public bool IsCollected()
    {
        return isCollected;
    }
}