using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    public enum bossStates
    {
        shooting,
        hurt, 
        moving
    };

    [SerializeField] Transform bossTransform;
    [SerializeField] bossStates currentBossState;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] Transform[] leftPoints;
    [SerializeField] Transform[] rightPoints;

    [Space(20)]

    [Header("Aggro State")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject mine;
    [SerializeField] float timeBetweenShots;
    [SerializeField] float timeBetweenMines;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform minePoint;
    [SerializeField] float hurtTime;
    [SerializeField] GameObject hitBox;

    private Animator animator;
    private bool moveRight;
    private float shotTimer;
    private float mineTimer;
    private float hurtTimer;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        currentBossState = bossStates.shooting;
    }

    private void Update()
    {
        UpdateBossState();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeHit();
        }
#endif
    }

    private void UpdateBossState()
    {
        switch(currentBossState)
        {
            case bossStates.shooting:
                UpdateShotTimers();
                break;

            case bossStates.hurt:
                UpdateHurtTimer();
                break;

            case bossStates.moving:
                Move();
                break;
        }
    }

    private void Move()
    {
        if (moveRight)
        {
            int index = UnityEngine.Random.Range(0, rightPoints.Length);
            Transform point = rightPoints[0];

            bossTransform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
            if (bossTransform.position.x > point.position.x)
            {
                SetAfterMove(1, false);
            }
        }
        else
        {
            int index = UnityEngine.Random.Range(0, leftPoints.Length);
            Transform point = leftPoints[0];

            bossTransform.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
            if (bossTransform.position.x < point.position.x)
            {
                SetAfterMove(-1, true);
            }
        }

        mineTimer -= Time.deltaTime;

        if (mineTimer <= 0)
        {
            mineTimer = timeBetweenMines;
            Instantiate(mine, minePoint.position, minePoint.rotation);
        }
    }

    private void SetAfterMove(float scale, bool isMovingRight)
    {
        bossTransform.localScale = new Vector3(scale, 1f, 1f);
        moveRight = isMovingRight;
        currentBossState = bossStates.shooting;
        shotTimer = 0;

        animator.SetTrigger("StopMoving");
        hitBox.SetActive(true);
    }

    private void UpdateShotTimers()
    {
        shotTimer -= Time.deltaTime;
        if (shotTimer <= 0)
        {
            shotTimer = timeBetweenShots;
            GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            newBullet.transform.localScale = bossTransform.localScale;
        }
    }

    private void UpdateHurtTimer()
    {
        if (hurtTimer != 0)
        {
            hurtTimer -= Time.deltaTime;
            if (hurtTimer < 0)
            {
                hurtTimer = 0;
                currentBossState = bossStates.moving;
                mineTimer = 0;
            }
        }
    }

    public void TakeHit()
    {
        currentBossState = bossStates.hurt;
        hurtTimer = hurtTime;

        animator.SetTrigger("Hit");
    }
}
