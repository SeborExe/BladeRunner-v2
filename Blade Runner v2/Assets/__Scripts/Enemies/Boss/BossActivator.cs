using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField] GameObject boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boss.SetActive(true);
            MusicManager.Instance.PlayMusic(GameResources.Instance.BossBatle);
            gameObject.SetActive(false);
        }
    }
}
