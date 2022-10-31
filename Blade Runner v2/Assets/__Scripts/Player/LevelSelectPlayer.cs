using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPlayer : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] MapPoint currentPoint;
    [SerializeField] LevelSelectManager selectManager;

    private bool levelLoading;

    private void Update()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentPoint.transform.position) > 0.01f || levelLoading) return; 

        if (Input.GetAxisRaw("Horizontal") > 0.5f)
        {
            if (currentPoint.right != null)
            {
                SetNextPoint(currentPoint.right);
            }
        }

        if (Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            if (currentPoint.left != null)
            {
                SetNextPoint(currentPoint.left);
            }
        }

        if (Input.GetAxisRaw("Vertical") > 0.5f)
        {
            if (currentPoint.up != null)
            {
                SetNextPoint(currentPoint.up);
            }
        }

        if (Input.GetAxisRaw("Vertical") < -0.5f)
        {
            if (currentPoint.down != null)
            {
                SetNextPoint(currentPoint.down);
            }
        }

        if (currentPoint.isLevel)
        {
            LevelSelectUIController.Instance.ShowInfo(currentPoint);
        }
        else
        {
            LevelSelectUIController.Instance.HideInfo();
        }

        if (currentPoint.isLevel && !currentPoint.isLocked)
        {
            if (Input.GetButtonDown("Jump"))
            {
                levelLoading = true;
                selectManager.LoadLevel(currentPoint);
            }
        }
    }

    private void SetNextPoint(MapPoint nextPoint)
    {
        currentPoint = nextPoint;
    }

    public MapPoint GetCurrentPoint()
    {
        return currentPoint;
    }

    public void SetCurrentPoint(MapPoint newPoint)
    {
        currentPoint = newPoint;
    }
}
