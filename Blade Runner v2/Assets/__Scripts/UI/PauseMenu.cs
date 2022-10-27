using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button pauseButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button levelSelectButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] GameObject pausePanel;

    [Header("Scene Names")]
    [SerializeField] string levelSelect;

    private void OnEnable()
    {
        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(Resume);
        levelSelectButton.onClick.AddListener(LevelSelect);
        mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    private void OnDisable()
    {
        pauseButton.onClick.RemoveListener(Pause);
        resumeButton.onClick.RemoveListener(Resume);
        levelSelectButton.onClick.RemoveListener(LevelSelect);
        mainMenuButton.onClick.RemoveListener(LoadMainMenu);
    }

    private void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    private void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void LevelSelect()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelSelect);
    }
}
