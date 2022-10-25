using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] RectTransform hearthBar;
    [SerializeField] GameObject hearthPrefab;
    [SerializeField] Sprite fullHealth, emptyHealth, halfHearth;
    [SerializeField] TMP_Text gemCounter;

    private List<Image> hearths = new List<Image>();
    private List<PickUp> gems = new List<PickUp>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InstantiatePlayerHearth();
        UpdateHealthDisplay();
        UpdateGemCount();
    }

    private void InstantiatePlayerHearth()
    {
        foreach (Transform child in hearthBar.transform)
        {
            Destroy(child.gameObject);
        }

        int hearthNumber = HealthController.Instance.GetMaxHealth();

        if (hearthNumber % 2 == 0)
        {
            int NumberOfHearts = hearthNumber / 2;
            for (int i = 0; i < NumberOfHearts; i++)
            {
                GameObject heart = Instantiate(hearthPrefab, hearthBar.transform);

                heart.transform.SetParent(hearthBar);
                heart.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * 110f, 0);

                Image hearthImage = heart.GetComponent<Image>();
                hearths.Add(hearthImage);
            }
        }
        else
        {
            int NumberOfHearts = (int)Mathf.Ceil((float)hearthNumber / 2);
            for (int i = 0; i < NumberOfHearts; i++)
            {
                GameObject heart = Instantiate(hearthPrefab, hearthBar.transform);

                heart.transform.SetParent(hearthBar);
                heart.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * 110f, 0);

                Image hearthImage = heart.GetComponent<Image>();
                hearths.Add(hearthImage);
            }
        }
    }

    public void UpdateHealthDisplay()
    {
        switch(HealthController.Instance.GetCurrentHealth())
        {
            case 6:
                for (int i = 0; i < hearths.Count; i++)
                {
                    if (i == 0 || i == 1 || i == 2)
                    {
                        hearths[i].sprite = fullHealth;
                        continue;
                    }
                    else
                    {
                        hearths[i].sprite = emptyHealth;
                        continue;
                    }
                }
                break;

            case 5:
                for (int i = 0; i < hearths.Count; i++)
                {
                    if (i == 0 || i == 1)
                    {
                        hearths[i].sprite = fullHealth;
                        continue;
                    }

                    if (i == 2)
                    {
                        hearths[i].sprite = halfHearth;
                        continue;
                    }
                    else
                    {
                        hearths[i].sprite = emptyHealth;
                        continue;
                    }
                }
                break;

            case 4:
                for (int i = 0; i < hearths.Count; i++)
                {
                    if (i == 0 || i == 1)
                    {
                        hearths[i].sprite = fullHealth;
                        continue;
                    }
                    else
                    {
                        hearths[i].sprite = emptyHealth;
                        continue;
                    }
                }
                break;

            case 3:
                for (int i = 0; i < hearths.Count; i++)
                {
                    if (i == 0)
                    {
                        hearths[i].sprite = fullHealth;
                        continue;
                    }

                    if (i == 1)
                    {
                        hearths[i].sprite = halfHearth;
                        continue;
                    }
                    else
                    {
                        hearths[i].sprite = emptyHealth;
                        continue;
                    }
                }
                break;

            case 2:
                for (int i = 0; i < hearths.Count; i++)
                {
                    if (i == 0)
                    {
                        hearths[i].sprite = fullHealth;
                        continue;
                    }
                    else
                    {
                        hearths[i].sprite = emptyHealth;
                        continue;
                    }
                }
                break;

            case 1:
                for (int i = 0; i < hearths.Count; i++)
                {
                    if (i == 0)
                    {
                        hearths[i].sprite = halfHearth;
                        continue;
                    }
                    else
                    {
                        hearths[i].sprite = emptyHealth;
                        continue;
                    }
                }
                break;

            default:
                for (int i = 0; i < hearths.Count; i++)
                {
                    hearths[i].sprite = emptyHealth;
                    continue;
                }
                break;
        }
    }

    public void UpdateGemCount()
    {
        int gemsCount = gems.Count;
        int inactiveGems = gems.Count(gem => gem.IsCollected());

        gemCounter.text = $"{inactiveGems} / {gemsCount}";
    }

    public void AddGem(PickUp gem)
    {
        gems.Add(gem);
    }
}
