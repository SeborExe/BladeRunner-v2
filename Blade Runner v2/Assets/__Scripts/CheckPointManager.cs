using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager Instance;

    [SerializeField] private List<CheckPoint> checkpoints = new List<CheckPoint>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddCheckPoint(CheckPoint checkPoint)
    {
        checkpoints.Add(checkPoint);
    }

    public void DeactivateCheckPoints()
    {
        foreach (CheckPoint checkPoint in checkpoints)
        {
            checkPoint.ResetCheckPoint();
        }
    }
}
