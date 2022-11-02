using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smasher : MonoBehaviour
{
    [SerializeField] Transform smasher;
    [SerializeField] Transform smashTarget;
    [SerializeField] float slamSpeed = 10f;
    [SerializeField] float waitAfterSlam = 1f;
    [SerializeField] float resetSpeed = 2f;
}
