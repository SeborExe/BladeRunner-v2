using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] float moveSpeed;
    [SerializeField] int currentPoint;
    [SerializeField] float distanceToAttackPlayer;
    [SerializeField] float chaseSpeed;
    [SerializeField] float waitTimeAfterAttack;

    private SpriteRenderer spriteRenderer;

    private Vector3 attackTarget;
    private float attackTimer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        foreach (Transform point in points)
        {
            point.parent = null;
        }
    }

    private void Update()
    {
        UpdateTimers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void UpdateTimers()
    {
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0f)
            {
                attackTimer = 0f;
            }
        }
    }

    private void Move()
    {
        if (attackTimer > 0f) return;

        if (FindPlayerInRange())
        {
            attackTarget = Vector3.zero;

            transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, points[currentPoint].position) < 0.05f)
            {
                currentPoint++;

                if (currentPoint >= points.Length)
                {
                    currentPoint = 0;
                }
            }

            FlipSprite();
        }
        else
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        if (attackTarget == Vector3.zero)
        {
            attackTarget = Player.Instance.transform.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

        FlipSpriteInAttackState();

        if (Vector3.Distance(transform.position, attackTarget) <= 0.1f)
        {
            attackTimer = waitTimeAfterAttack;
            attackTarget = Vector3.zero;
        }
    }

    private void FlipSpriteInAttackState()
    {
        if (transform.position.x > attackTarget.x && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }

        if (transform.position.x > attackTarget.x && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }

        if (transform.position.x < attackTarget.x && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }

        if (transform.position.x < attackTarget.x && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }
    }

    private bool FindPlayerInRange()
    {
        return (Vector3.Distance(transform.position, Player.Instance.transform.position) > distanceToAttackPlayer);

    }

    private void FlipSprite()
    {
        if (transform.position.x < points[currentPoint].position.x)
        {
            spriteRenderer.flipX = true;
        }

        else if (transform.position.x > points[currentPoint].position.x)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, distanceToAttackPlayer);
    }
}
