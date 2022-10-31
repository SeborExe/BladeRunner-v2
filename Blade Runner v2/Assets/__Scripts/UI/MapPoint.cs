using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public MapPoint up, right, down, left;
    public bool isLevel;
    public bool isLocked = true;
    public string levelToLoad, levelToCheck, levelName;

    public int gemsCollected, totalGems;
    public float bestTime, targetTime;

    [SerializeField] SpriteRenderer padlock, gemBadge, timeBadge;

    private void Start()
    {
        InitializeLevels();
    }

    private void InitializeLevels()
    {
        if (isLevel && levelToLoad != null)
        {
            if (PlayerPrefs.HasKey(levelToLoad + "_gems"))
            {
                gemsCollected = PlayerPrefs.GetInt(levelToLoad + "_gems");
            }

            if (PlayerPrefs.HasKey(levelToLoad + "_time"))
            {
                bestTime = PlayerPrefs.GetFloat(levelToLoad + "_time");
            }

            ShowBadges();

            isLocked = true;
            UpdatePadlockUI();

            if (PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
            {
                if (PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
                {
                    isLocked = false;
                    UpdatePadlockUI();
                }
            }
        }

        if (isLevel && levelToLoad != null && levelToCheck == "")
        {
            isLocked = false;
            UpdatePadlockUI();
        }
    }

    private void OnEnable()
    {
        UpdatePadlockUI();
    }

    private void UpdatePadlockUI()
    {
        if (!isLocked && padlock != null)
        {
            padlock.gameObject.SetActive(false);
        }
    }

    private void ShowBadges()
    {
        if (gemsCollected >= totalGems)
        {
            gemBadge.gameObject.SetActive(true);
        }

        if (bestTime <= targetTime && bestTime != 0)
        {
            timeBadge.gameObject.SetActive(true);
        }
    }
}
