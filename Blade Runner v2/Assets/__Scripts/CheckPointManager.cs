using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager Instance;

    [SerializeField] private List<CheckPoint> checkpoints = new List<CheckPoint>();

    private Vector3 spawnPoint;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        spawnPoint = PlayerHealthController.Instance.transform.position + Vector3.up;
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

    public void SetSpawnPoint(Vector3 spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }

    public Vector3 GetSpawnPoint()
    {
        return spawnPoint;
    }
}
