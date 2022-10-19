using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] Image[] hearths;
    [SerializeField] Sprite fullHealth, emptyHealth;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateHealthDisplay()
    {
        switch(PlayerHealthController.Instance.GetCurrentHealth())
        {
            case 3:
                for (int i = 0; i < hearths.Length; i++)
                {
                    hearths[i].sprite = fullHealth;
                }
                break;

            case 2:
                for (int i = 0; i < hearths.Length - 1; i++)
                {
                    hearths[i].sprite = fullHealth;
                }
                hearths[2].sprite = emptyHealth;
                break;

            case 1:
                hearths[0].sprite = fullHealth;
                hearths[1].sprite = emptyHealth;
                hearths[2].sprite = emptyHealth;
                break;

            default:
                for (int i = 0; i < hearths.Length; i++)
                {
                    hearths[i].sprite = emptyHealth;
                }
                break;
        }
    }
}
