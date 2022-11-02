using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject objectToSwitch;
    [SerializeField] Sprite downSprite;
    [SerializeField] GameObject effect;
    [SerializeField] bool shouldBeActiveAfterSwitch;

    private SpriteRenderer spriteRenderer;
    private bool hasSwitched;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasSwitched)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            AudioManager.Instance.PlaySoundEffect(GameResources.Instance.PickupHealth);

            objectToSwitch.gameObject.SetActive(shouldBeActiveAfterSwitch);
            spriteRenderer.sprite = downSprite;
            hasSwitched = true;
        }
    }
}
