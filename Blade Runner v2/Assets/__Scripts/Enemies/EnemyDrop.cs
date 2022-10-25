using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] GameObject collectable;
    [SerializeField, Tooltip("Value in percent"), Range(0,100)] float chanceToDrop;

    public void CheckDrop()
    {
        float dropSelect = Random.Range(0, 100f);
        if (dropSelect <= chanceToDrop)
        {
            Instantiate(collectable, transform.position, Quaternion.identity);
        }
    }
}
