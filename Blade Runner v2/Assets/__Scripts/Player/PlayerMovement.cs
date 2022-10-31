using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private Rigidbody2D rb;
    private Stomp stomp;

    [SerializeField] private float moveSpeed = 40f;
    [SerializeField] private float knockBackLength;
    [SerializeField] private float knockBackForceX;
    [SerializeField] private float knockBackForceY;
    [SerializeField] private float bounceForce;

    private float knockBackTimer;
    private float horizontalMove;
    private bool canDoubleJump;
    private bool crouch;
    private bool stopInput;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        stomp = GetComponentInChildren<Stomp>();
    }

    private void Start()
    {
        rb = controller.rb;
    }

    private void Update()
    {
        UpdateTimers();

        if (knockBackTimer > 0) return;

        if (stopInput) return;

        HandleMoving();

        HandleJumping();

        HandleCrouching();

        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, false);
    }

    private void HandleMoving()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
    }

    private void HandleJumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (controller.GetIsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, controller.GetJumpForce());
                AudioManager.Instance.PlaySoundEffect(GameResources.Instance.PlayerJump);
            }
            else
            {
                if (canDoubleJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, controller.GetJumpForce());
                    AudioManager.Instance.PlaySoundEffect(GameResources.Instance.PlayerJump);
                    canDoubleJump = false;
                }
            }
        }

        if (controller.GetIsGrounded())
        {
            canDoubleJump = true;
        }
    }

    private void HandleCrouching()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }

        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    private void UpdateAnimations()
    {
        animator.SetFloat("moveSpeed", Mathf.Abs(horizontalMove));
        animator.SetBool("isGrounded", controller.GetIsGrounded());
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);
    }

    private void UpdateTimers()
    {
        if (knockBackTimer > 0)
        {
            if (controller.GetFacingDirection())
            {
                //rb.velocity = new Vector2(-knockBackForce, rb.velocity.y);
                rb.AddRelativeForce(new Vector2(-knockBackForceX, knockBackForceY));
            }
            else
            {
                //rb.velocity = new Vector2(knockBackForce, rb.velocity.y);
                rb.AddRelativeForce(new Vector2(knockBackForceX, knockBackForceY));
            }

            knockBackTimer -= Time.deltaTime;
            if (knockBackTimer < 0)
            {
                knockBackTimer = 0;
                stomp.enabled = true;
            }
        }
    }

    public void KnockBack()
    {
        knockBackTimer = knockBackLength;
        //rb.velocity = new Vector2(0f, rb.velocity.y);
        stomp.enabled = false;

        animator.SetTrigger("hurt");
    }

    public void Bounce()
    {
        rb.velocity = new Vector2(rb.velocity.x, bounceForce);
        AudioManager.Instance.PlaySoundEffect(GameResources.Instance.PlayerJump);
    }

    public void SetStopInput(bool stopInput)
    {
        this.stopInput = stopInput;
    }

    public void StopPlayer()
    {
        horizontalMove = 0;
        UpdateAnimations();
    }
}
