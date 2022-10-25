using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] float waitToRespawn = 2;

    private int gemsCollected;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        MusicManager.Instance.PlayMusic(GameResources.Instance.MinaLevel);
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        HealthController.Instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn);

        HealthController.Instance.transform.position = CheckPointManager.Instance.GetSpawnPoint();

        int amountHealth = (int)Mathf.Ceil(HealthController.Instance.GetMaxHealth() / 2);
        HealthController.Instance.SetCurrentHealth(amountHealth);
        UIController.Instance.UpdateHealthDisplay();

        HealthController.Instance.gameObject.SetActive(true);
    }

    public int GetGems()
    {
        return gemsCollected;
    }

    public void AddGem(int amount = 1)
    {
        gemsCollected += amount;
    }
}
