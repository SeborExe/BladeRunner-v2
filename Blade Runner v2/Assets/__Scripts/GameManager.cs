using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float waitToRespawn;

    private void Awake()
    {
        Instance = this;
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        PlayerHealthController.Instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn);

        PlayerHealthController.Instance.transform.position = CheckPointManager.Instance.GetSpawnPoint();

        int amountHealth = (int)Mathf.Ceil(PlayerHealthController.Instance.GetMaxHealth() / 2);
        PlayerHealthController.Instance.SetCurrentHealth(amountHealth);
        UIController.Instance.UpdateHealthDisplay();

        PlayerHealthController.Instance.gameObject.SetActive(true);
    }
}
