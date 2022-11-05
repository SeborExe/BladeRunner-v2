using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] float bounce;

    public float GetBounce()
    {
        return bounce;
    }
}
