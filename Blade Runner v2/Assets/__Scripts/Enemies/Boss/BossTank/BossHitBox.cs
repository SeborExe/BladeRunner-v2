using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitBox : MonoBehaviour
{
    [SerializeField] BossTankController bossTankController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("StompBox") && Player.Instance.transform.position.y > transform.position.y)
        {
            bossTankController.TakeHit();
            gameObject.SetActive(false);
        }
    }
}
