using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] Vector2 MinMaxPitchVolume = new Vector2(0.9f, 1.1f);

    AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        audioSource.pitch = Random.Range(MinMaxPitchVolume.x, MinMaxPitchVolume.y);
        audioSource.PlayOneShot(clip);
    }
}
