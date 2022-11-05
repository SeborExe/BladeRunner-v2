using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] int damage;
    [SerializeField] bool isBossMine = false;
    [SerializeField] float timeToDetonate = 0;

    private float detonateTimer;

    private void Start()
    {
        detonateTimer = timeToDetonate;
    }

    private void Update()
    {
        if (detonateTimer != 0)
        {
            detonateTimer -= Time.deltaTime;
            if (detonateTimer <= 0)
            {
                AudioManager.Instance.PlaySoundEffect(GameResources.Instance.PlayerDead);
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<HealthController>(out HealthController health) && (collision.CompareTag("Player") || collision.CompareTag("StompBox")))
        {
            health.TakeDamage(damage, true);
            AudioManager.Instance.PlaySoundEffect(GameResources.Instance.PlayerDead);

            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
