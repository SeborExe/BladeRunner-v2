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
        moving,
        dialogue
    };

    [SerializeField] Transform bossTransform;
    [SerializeField] bossStates currentBossState;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] Transform[] leftPoints;
    [SerializeField] Transform[] rightPoints;

    [Space(20)]

    [Header("Aggro State")]
    [SerializeField] float timeBetweenShots;
    [SerializeField] float timeBetweenMines;
    [SerializeField] float firstMineTime = 0.1f;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform minePoint;
    [SerializeField] float hurtTime;
    [SerializeField] GameObject hitBox;

    [Space(20)]

    [Header("Weapons")]
    [SerializeField] GameObject normalBullet;
    [SerializeField] GameObject bounceBullet;
    [SerializeField] GameObject mine;

    [Space(20)]

    [Header("BounceBulletStats")]
    [SerializeField, Range(0, 100)] int chanseToBounceBullet = 25;
    [SerializeField] int bounceBulletChanceDecrease;

    [Space(20)]

    [Header("Buff when low health")]
    [SerializeField] float shotSpeedUp;
    [SerializeField] float mineSpeedUp;

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
            GameObject newBullet = Instantiate(GetBullet(), firePoint.position, firePoint.rotation);
            newBullet.transform.localScale = bossTransform.localScale;

            AudioManager.Instance.PlaySoundEffect(GameResources.Instance.BossShot);
        }
    }

    private GameObject GetBullet()
    {
        int bulletChance = UnityEngine.Random.Range(0, 100);
        GameObject bullet = null;

        if (bulletChance < chanseToBounceBullet)
        {
            bullet = bounceBullet;
        }
        else
        {
            bullet = normalBullet;
        }

        return bullet;
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
                mineTimer = firstMineTime;
            }
        }
    }

    public void TakeHit()
    {
        currentBossState = bossStates.hurt;
        hurtTimer = hurtTime;

        timeBetweenShots /= shotSpeedUp;
        timeBetweenMines /= mineSpeedUp;
        chanseToBounceBullet -= bounceBulletChanceDecrease;

        AudioManager.Instance.PlaySoundEffect(GameResources.Instance.BossImpact);
        animator.SetTrigger("Hit");
    }
}
