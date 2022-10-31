using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [HideInInspector] public HealthController playerHealth;
    [HideInInspector] public PlayerMovement playerMovement;

    private void Awake()
    {
        Instance = this;

        playerHealth = GetComponent<HealthController>();
        playerMovement = GetComponent<PlayerMovement>();
    }
}
