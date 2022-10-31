using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button continueButton;
    [SerializeField] string startScene, continueScene;

    private void Start()
    {
        if (PlayerPrefs.HasKey(startScene + "_unlocked"))
        {
            continueButton.gameObject.SetActive(true);
        }
        else
        {
            continueButton.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        continueButton.onClick.AddListener(Continue);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(StartGame);
        quitButton.onClick.RemoveListener(QuitGame);
        continueButton.onClick.RemoveListener(Continue);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(startScene);
        PlayerPrefs.DeleteAll();
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void Continue()
    {
        SceneManager.LoadScene(continueScene);
    }
}
