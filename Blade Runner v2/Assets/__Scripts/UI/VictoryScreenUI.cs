using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreenUI : MonoBehaviour
{
    [SerializeField] Button mainMenuButton;

    private void OnEnable()
    {
        mainMenuButton.onClick.AddListener(BackToMainMenu);
    }

    private void OnDisable()
    {
        mainMenuButton.onClick.RemoveListener(BackToMainMenu);
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
