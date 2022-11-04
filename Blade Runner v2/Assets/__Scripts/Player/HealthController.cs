using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] bool isPlayer = false;
    [SerializeField] int maxHealth = 3;
    [SerializeField] int currentHealth;
    [SerializeField] float invincibleLength = 1f;
    [SerializeField] GameObject deathEffect;
    [SerializeField] bool hasSpriteRenderer = true;

    private SpriteRenderer spriteRenderer;

    private float invincibleTimer;
    private Coroutine immunityCoroutine;
    private const float spriteFlashInterval = 0.1f;
    private WaitForSeconds WaitForSecondsSpriteFlashInterfal = new WaitForSeconds(spriteFlashInterval);

    public event Action OnDie;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        OnDie += Die;
    }

    private void OnDisable()
    {
        OnDie -= Die;
    }

    private void Update()
    {
        UpdateTimers();
    }

    public virtual void TakeDamage(int amount = 1)
    {
        if (invincibleTimer > 0) return;

        currentHealth -= amount;

        if (isPlayer)
        {
            UIController.Instance.UpdateHealthDisplay();
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDie?.Invoke();
        }
        else
        {
            invincibleTimer = invincibleLength;
            AudioManager.Instance.PlaySoundEffect(GameResources.Instance.PlayerHurt);
            PostHitImmunity();
        }
    }

    public void HealPlayer(int amount)
    {
        int healthAfterHeal = currentHealth += amount;
        currentHealth = Mathf.Min(healthAfterHeal, maxHealth);
    }

    public virtual void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        if (isPlayer)
        {
            AudioManager.Instance.PlaySoundEffect(GameResources.Instance.PlayerDead);
            GameManager.Instance.RespawnPlayer();
        }
        else
        {
            GetComponentInParent<EnemyDrop>().CheckDrop();
            AudioManager.Instance.PlaySoundEffect(GameResources.Instance.EnemyExplode);
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    private void UpdateTimers()
    {
        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                invincibleTimer = 0;
            }
        }
    }

    private void PostHitImmunity()
    {
        if (!hasSpriteRenderer) return;

        if (immunityCoroutine != null)
            StopCoroutine(immunityCoroutine);

        immunityCoroutine = StartCoroutine(PostHitImmunityRoutine(invincibleLength, spriteRenderer));
    }

    private IEnumerator PostHitImmunityRoutine(float immuneTime, SpriteRenderer spriteRenderer)
    {
        int iterations = Mathf.RoundToInt(immuneTime / spriteFlashInterval / 2f);

        while (iterations > 0)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
            yield return WaitForSecondsSpriteFlashInterfal;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            yield return WaitForSecondsSpriteFlashInterfal;
            iterations--;
            yield return null;
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetCurrentHealth(int amount)
    {
        currentHealth = amount;
    }
}
