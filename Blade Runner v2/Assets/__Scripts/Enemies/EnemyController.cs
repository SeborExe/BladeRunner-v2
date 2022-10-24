using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Transform leftPoint;
    [SerializeField] Transform rightPoint;
    [SerializeField] Vector2 moveTime;
    [SerializeField] Vector2 waitTime;

    private bool isMovingRight;
    private float moveCount, waitCount;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        leftPoint.parent = null;
        rightPoint.parent = null;

        isMovingRight = true;
        moveCount = UnityEngine.Random.Range(moveTime.x, moveTime.y);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;

            if (isMovingRight)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                spriteRenderer.flipX = true;

                if (transform.position.x >= rightPoint.position.x)
                {
                    isMovingRight = false;
                }
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                spriteRenderer.flipX = false;

                if (transform.position.x < leftPoint.position.x)
                {
                    isMovingRight = true;
                }
            }

            if (moveCount <= 0)
            {
                moveCount = 0;
                waitCount = UnityEngine.Random.Range(waitTime.x, waitTime.y);
            }

            animator.SetBool("isMoving", true);
        }

        else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            rb.velocity = new Vector2(0, rb.velocity.y);

            if (waitCount <= 0)
            {
                waitCount = 0;
                moveCount = UnityEngine.Random.Range(moveTime.x, moveTime.y);
            }

            animator.SetBool("isMoving", false);
        }
    }
}
