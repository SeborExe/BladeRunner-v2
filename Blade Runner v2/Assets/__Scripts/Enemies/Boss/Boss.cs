using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject ObjectToTurnOff;
    [SerializeField] GameObject objectToSetActiveAfterBoss;

    public void OnDefeated()
    {
        objectToSetActiveAfterBoss.SetActive(true);

        MusicManager.Instance.StopPlayMusic();
        MusicManager.Instance.PlayMusic(GameResources.Instance.MinaLevel);

        ObjectToTurnOff.SetActive(false);
    }
}
