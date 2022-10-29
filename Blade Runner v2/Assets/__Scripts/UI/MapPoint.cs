using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    [SerializeField] private MapPoint up, right, down, left;
    [SerializeField] bool isLevel;
}
