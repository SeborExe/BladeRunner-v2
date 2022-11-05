using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossActivator : MonoBehaviour
{
    [SerializeField] GameObject boss;

    private bool alreadyTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boss.SetActive(true);
            MusicManager.Instance.PlayMusic(GameResources.Instance.BossBatle);
            //gameObject.SetActive(false);

            if (!alreadyTriggered && TryGetComponent<PlayableDirector>(out PlayableDirector playableDirector)) 
            {
                alreadyTriggered = true;
                playableDirector.Play();
            }
        }
    }
}
