using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
	[SerializeField] private float jumpForce = 10f;                         
	[Range(0, 1)][SerializeField] private float crouchSpeed = .36f;           
	[Range(0, .3f)][SerializeField] private float smoothMovement = .05f;   
	[SerializeField] private bool airControl = true;                         
	[SerializeField] private LayerMask groundLayer;                          
	[SerializeField] private Transform groundPoint;        
	[SerializeField] private Transform ceilingPoint;       
	[SerializeField] private Collider2D crouchDisableCollider;

	[HideInInspector] public Rigidbody2D rb;

	const float groundedRadius = .2f; 
	const float ceilingRafius = .2f;
	private bool isGrounded;  
	private bool facingRight = true;
	private bool isCrouching = false;
	private Vector3 velocity = Vector3.zero;

	[Header("Events"), Space]
	public BoolEvent OnCrouchEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = isGrounded;
		isGrounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPoint.position, groundedRadius, groundLayer);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isGrounded = true;
			}
		}
	}


	public void Move(float move, bool crouch, bool jump)
	{
		if (!crouch)
		{
			if (Physics2D.OverlapCircle(ceilingPoint.position, ceilingRafius, groundLayer))
			{
				crouch = true;
			}
		}

		if (isGrounded || airControl)
		{

			// If crouching
			if (crouch)
			{
				if (!isCrouching)
				{
					isCrouching = true;
					OnCrouchEvent?.Invoke(true);
				}

				move *= crouchSpeed;

				if (crouchDisableCollider != null)
					crouchDisableCollider.enabled = false;
			}
			else
			{
				if (crouchDisableCollider != null)
					crouchDisableCollider.enabled = true;

				if (isCrouching)
				{
					isCrouching = false;
					OnCrouchEvent?.Invoke(false);
				}
			}

			Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothMovement);

			if (move > 0 && !facingRight)
			{
				Flip();
			}

			else if (move < 0 && facingRight)
			{
				Flip();
			}
		}

		if (isGrounded && jump)
		{
			isGrounded = false;
			rb.AddForce(new Vector2(0f, jumpForce));
		}
	}

	private void Flip()
	{
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public bool GetIsGrounded()
	{
		return isGrounded;
	}

	public void SetIsGrounded(bool isGrounded)
	{
		this.isGrounded = isGrounded;
	}

	public float GetJumpForce()
    {
		return jumpForce;
    }
}