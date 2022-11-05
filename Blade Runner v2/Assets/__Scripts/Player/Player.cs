using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [HideInInspector] public HealthController playerHealth;
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public CharacterController characterController;

    private void Awake()
    {
        Instance = this;

        playerHealth = GetComponent<HealthController>();
        playerMovement = GetComponent<PlayerMovement>();
        characterController = GetComponent<CharacterController>();
    }
}
