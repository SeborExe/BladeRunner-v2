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

    private void Awake()
    {
        Instance = this;
    }
}
