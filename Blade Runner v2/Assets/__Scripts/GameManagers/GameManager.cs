using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] float waitToRespawn = 2;
    [SerializeField] string levelToLoad;

    [HideInInspector] public Player player;
    [HideInInspector] public HealthController healthController;

    private int gemsCollected;
    private float timeInLevel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        MusicManager.Instance.PlayMusic(GameResources.Instance.MinaLevel);

        player = Player.Instance;
        healthController = player.playerHealth;
        timeInLevel = 0f;
    }

    private void Update()
    {
        timeInLevel += Time.deltaTime;
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.Instance.GetFadeSpeed()));
        UIController.Instance.FadeToBlack();
        yield return new WaitForSeconds(1f / UIController.Instance.GetFadeSpeed() + 0.2f);
        UIController.Instance.FadeFromBlack();

        player.transform.position = CheckPointManager.Instance.GetSpawnPoint();

        int amountHealth = (int)Mathf.Ceil(healthController.GetMaxHealth() / 2);
        healthController.SetCurrentHealth(amountHealth);
        UIController.Instance.UpdateHealthDisplay();

        player.gameObject.SetActive(true);
    }

    public int GetGems()
    {
        return gemsCollected;
    }

    public void AddGem(int amount = 1)
    {
        gemsCollected += amount;
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCouroutine());
    }

    private IEnumerator EndLevelCouroutine()
    {
        player.playerMovement.SetStopInput(true);
        player.playerMovement.StopPlayer();

        yield return new WaitForSeconds(1f);
        UIController.Instance.ShowCompleteText();
        yield return new WaitForSeconds(2f);

        UIController.Instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.Instance.GetFadeSpeed()) + 0.25f);

        PlayerPrefsSaves();

        SceneManager.LoadScene(levelToLoad);
    }

    private void PlayerPrefsSaves()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_gems"))
        {
            if (gemsCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
            }
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
        }

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time"))
        {
            if (timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time"))
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
        }
    }
}
