using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Transform farBackground;
    [SerializeField] Transform middleBackground;

    [SerializeField] private float middleBackgroundMoveMultiplier = 0.1f;
    [SerializeField] private float farBackgroundMoveMultiplier = 0.2f;
    private Vector2 lastPos;

    private void Start()
    {
        lastPos = transform.position;
    }
     
    private void Update()
    {
        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        farBackground.position += new Vector3(amountToMove.x, amountToMove.y * farBackgroundMoveMultiplier, 0f);
        middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * middleBackgroundMoveMultiplier;

        lastPos = transform.position;
    }
}
