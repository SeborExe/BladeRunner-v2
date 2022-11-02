using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smasher : MonoBehaviour
{
    [SerializeField] Transform smasher;
    [SerializeField] Transform smashTarget;
    [SerializeField] float slamSpeed = 10f;
    [SerializeField] float waitAfterSlam = 1f;
    [SerializeField] float resetSpeed = 2f;
    [SerializeField] float detectionPlayerDistance = 2f;
    [SerializeField] bool isAutomatic = false;
    [SerializeField] float automaticSlamTime = 3f;

    private bool slamming;
    private bool resetting;
    private Vector3 startPoint;
    private float waitCounter;
    private float automaticTimer;

    private void Start()
    {
        startPoint = smasher.position;
    }

    private void Update()
    {
        if (!slamming && !resetting && !isAutomatic)
        {
            if (Vector3.Distance(smashTarget.position, Player.Instance.transform.position) < detectionPlayerDistance)
            {
                slamming = true;
                waitCounter = waitAfterSlam;
            }
        }
        else if (!slamming && !resetting && isAutomatic)
        {
            if (automaticTimer == 0)
            {
                slamming = true;
                waitCounter = waitAfterSlam;
            }
        }

        if (slamming)
        {
            smasher.position = Vector3.MoveTowards(smasher.position, smashTarget.position, slamSpeed * Time.deltaTime);

            if (smasher.position == smashTarget.position)
            {
                waitCounter -= Time.deltaTime;
                if (waitCounter <= 0)
                {
                    slamming = false;
                    resetting = true;
                }

            }
        }

        if (resetting)
        {
            smasher.position = Vector3.MoveTowards(smasher.position, startPoint, resetSpeed * Time.deltaTime);

            if (smasher.position == startPoint)
            {
                resetting = false;

                if (isAutomatic)
                {
                    automaticTimer = automaticSlamTime;
                }
            }
        }

        UpdateTimers();
    }

    private void UpdateTimers()
    {
        if (automaticTimer > 0)
        {
            automaticTimer -= Time.deltaTime;

            if (automaticTimer < 0)
            {
                automaticTimer = 0;
            }
        }
    }
}
