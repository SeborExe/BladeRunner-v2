using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] GameObject bulletDestroyEffect;
    [SerializeField] bool isPlayerBullet = false;
    [SerializeField] bool canBounce = false;

    private void Update()
    {
        SetUpBullet();
    }

    private void SetUpBullet()
    {      
        if (!isPlayerBullet)
        {
            transform.position += new Vector3(-speed * transform.localScale.x * Time.deltaTime, 0f, 0f);
        }
        else
        {
            if (Player.Instance.characterController.GetFacingDirection())
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
            }
            else
            {
                transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("StompBox") && canBounce)
        {
            collision.GetComponent<Stomp>().Bounce();
        }

        else if (collision.CompareTag("Player") || (collision.CompareTag("StompBox") && !canBounce))
        {
            if (collision.GetComponent<HealthController>() != null)
            {
                HealthController health = collision.GetComponent<HealthController>();
                Damage(health);
            }
            else
            {
                HealthController health = collision.GetComponentInParent<HealthController>();
                Damage(health);
            }
        }

        Vector2 collisionPoint = collision.ClosestPoint(transform.position);
        Instantiate(bulletDestroyEffect, collisionPoint, Quaternion.identity);
        AudioManager.Instance.PlaySoundEffect(GameResources.Instance.EnemyExplode);
        Destroy(gameObject);
    }

    private void Damage(HealthController health)
    {
        health.TakeDamage(damage, true);
    }
}
