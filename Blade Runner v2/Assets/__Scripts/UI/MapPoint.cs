using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public MapPoint up, right, down, left;
    public bool isLevel;
    public bool isLocked = true;
    public string levelToLoad, levelToCheck, levelName;

    [SerializeField] SpriteRenderer padlock;

    private void Start()
    {
        if (isLevel && levelToLoad != null)
        {
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
}
