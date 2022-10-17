using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    [SerializeField] private float moveSpeed = 40f;

    private float horizontalMove;
    private bool canDoubleJump;
    private bool crouch;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
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
                controller.rb.velocity = new Vector2(controller.rb.velocity.x, controller.GetJumpForce());
            }
            else
            {
                if (canDoubleJump)
                {
                    controller.rb.velocity = new Vector2(controller.rb.velocity.x, controller.GetJumpForce());
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
}
