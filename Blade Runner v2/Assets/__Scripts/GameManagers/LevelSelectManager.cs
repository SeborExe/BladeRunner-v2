using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    private IEnumerator LoadLevelCoroutine(MapPoint currentMappoint)
    {
        LevelSelectUIController.Instance.FadeToBlack();
        yield return new WaitForSeconds((1f / LevelSelectUIController.Instance.GetFadeSpeed()) + 0.25f);
        SceneManager.LoadScene(currentMappoint.levelToLoad);
    }

    public void LoadLevel(MapPoint currentMappoint)
    {
        StartCoroutine(LoadLevelCoroutine(currentMappoint));
    }
}
