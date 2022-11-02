using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] float bounceForce = 25f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CharacterController>(out CharacterController characterController))
        {
            characterController.rb.velocity = new Vector2(characterController.rb.velocity.x, bounceForce);
            animator.SetTrigger("Bounce");
        }
    }
}
