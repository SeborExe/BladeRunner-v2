using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip clip, bool onShot = false)
    {
        if (onShot)
        {
            audioSource.loop = false;
            audioSource.PlayOneShot(clip);
        }
        else
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void StopPlayMusic()
    {
        audioSource.Stop();
    }
}
