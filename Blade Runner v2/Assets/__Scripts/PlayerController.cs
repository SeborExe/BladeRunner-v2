using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float crouchSpeed = 0.4f;
    [SerializeField] private float movementSpoothing = 0.05f;
    [SerializeField] private Transform groundedPoint;
    [SerializeField] private Transform ceilingPoint;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded;
    private bool canDoubleJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rb.velocity.y);

        //Jumping
        isGrounded = Physics2D.OverlapCircle(groundedPoint.position, 0.2f, groundLayer);

        if (isGrounded)
        {
            canDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else
            {
                if (canDoubleJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    canDoubleJump = false;
                }
            }
        }

        if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        else if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        animator.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("isGrounded", isGrounded);
    }
}
