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
    [SerializeField] private float ladderSpeed = 8f;
    [SerializeField] private float knockBackLength;
    [SerializeField] private float knockBackForceX;
    [SerializeField] private float knockBackForceY;
    [SerializeField] private float bounceForce;

    private float knockBackTimer;
    private float horizontalMove;
    private bool canDoubleJump;
    private bool crouch;
    private bool stopInput;
    [SerializeField] private bool isLadder;
    [SerializeField] private bool isClimbing;

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

        HandleLadderClimbing();

        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, false);

        OnClimbing();
    }

    private void HandleMoving()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
    }

    private void HandleJumping()
    {
        if (Input.GetButtonDown("Jump") && !isLadder)
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

    private void HandleLadderClimbing()
    {
        if (isLadder && Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
        {
            isClimbing = true;
        }
        else if (isLadder && Mathf.Abs(Input.GetAxis("Vertical")) <= 0.1f)
        {
            isClimbing = false;
        }
    }

    private void UpdateAnimations()
    {
        animator.SetFloat("moveSpeed", Mathf.Abs(horizontalMove));
        animator.SetBool("isGrounded", controller.GetIsGrounded());
        animator.SetBool("isClimbing", isClimbing);
        animator.SetBool("isLadder", isLadder);
    }

    private void OnClimbing()
    {
        if (isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * ladderSpeed);
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder = true;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
            rb.gravityScale = 3f;
        }
    }

    public void KnockBack()
    {
        knockBackTimer = knockBackLength;
        //rb.velocity = new Vector2(0f, rb.velocity.y);
        stomp.enabled = false;

        animator.SetTrigger("hurt");
    }

    public void Bounce(float bounce = 0)
    {
        if (bounce == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, bounce);
        }

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
