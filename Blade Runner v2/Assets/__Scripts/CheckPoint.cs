using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] Sprite CheckPointOn, CheckPointOff;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        CheckPointManager.Instance.AddCheckPoint(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && spriteRenderer.sprite != CheckPointOn)
        {
            CheckPointManager.Instance.DeactivateCheckPoints();
            spriteRenderer.sprite = CheckPointOn;

            CheckPointManager.Instance.SetSpawnPoint(transform.position + Vector3.up);
        }
    }

    public void ResetCheckPoint()
    {
        spriteRenderer.sprite = CheckPointOff;
    }
}
