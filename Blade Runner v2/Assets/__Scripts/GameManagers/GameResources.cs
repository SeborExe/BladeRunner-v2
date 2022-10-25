using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    public static GameResources Instance;

    [Header("Audio")]
    public AudioClip BossHit;
    public AudioClip BossImpact;
    public AudioClip BossShot;
    public AudioClip EnemyExplode;
    public AudioClip LevelSelected;
    public AudioClip MapMovement;
    public AudioClip PickupGem;
    public AudioClip PickupHealth;
    public AudioClip PlayerDead;
    public AudioClip PlayerHurt;
    public AudioClip PlayerJump;
    public AudioClip WarpJingle;

    [Header("Music")]
    public AudioClip BossBatle;
    public AudioClip GameComplete;
    public AudioClip LevelSelect;
    public AudioClip LevelVictory;
    public AudioClip MinaLevel;
    public AudioClip TitleScreen;

    private void Awake()
    {
        Instance = this;
    }
}
