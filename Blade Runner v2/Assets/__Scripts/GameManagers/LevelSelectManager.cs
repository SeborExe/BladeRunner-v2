using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] LevelSelectPlayer player;
    [SerializeField] MapPoint[] allPoints;

    private void Start()
    {
        allPoints = FindObjectsOfType<MapPoint>();

        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            foreach (MapPoint mapPoint in allPoints)
            {
                if (mapPoint.levelToLoad == PlayerPrefs.GetString("CurrentLevel"))
                {
                    player.transform.position = mapPoint.transform.position;
                    player.SetCurrentPoint(mapPoint); 
                }
            }
        }
    }

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
