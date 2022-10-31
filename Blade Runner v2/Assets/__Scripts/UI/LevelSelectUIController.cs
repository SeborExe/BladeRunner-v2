using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUIController : MonoBehaviour
{
    public static LevelSelectUIController Instance;

    [SerializeField] Image fadeScreen;
    [SerializeField] GameObject levelInfoPanel;
    [SerializeField] TMP_Text levelName, gemsText, bestTimeText, targetTimeText;

    private const float fadeSpeed = 3.5f;
    private bool shouldFadeBlack, shouldFadeFromBlack;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        FadeFromBlack();
    }

    private void Update()
    {
        if (shouldFadeBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
            {
                shouldFadeBlack = false;
            }
        }

        if (shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack()
    {
        shouldFadeBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeBlack = false;
    }

    public float GetFadeSpeed()
    {
        return fadeSpeed;
    }

    public void ShowInfo(MapPoint currentMappoint)
    {
        levelName.text = currentMappoint.levelName;
        gemsText.text = $"{currentMappoint.gemsCollected}/{currentMappoint.totalGems}";
        targetTimeText.text = $"Target: {currentMappoint.targetTime}s";

        if (currentMappoint.bestTime == 0)
        {
            bestTimeText.text = "Best: ---";
        }
        else
        {
            bestTimeText.text = "Best: " + currentMappoint.bestTime.ToString("F2") + "s";
        }

        levelInfoPanel.SetActive(true);
    }

    public void HideInfo()
    {
        levelInfoPanel.SetActive(false);
    }
}
