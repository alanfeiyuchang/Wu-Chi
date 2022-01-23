using UnityEngine;
using UnityEngine.Events;

public class CharacterController2d : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = true;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
	[SerializeField] private PhysicsMaterial2D noFriction;
	[SerializeField] private PhysicsMaterial2D maxFriction;


	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	// new variable
	private CapsuleCollider2D m_Collider2D;
	private Animator animator;

	private Vector2 newVelocity;
	private Vector2 colliderSize;
	private Vector2 slopeNormalPerp;

	private float slopeDownAngle;
	private float slopeDownAngleOld;
	private float slopeSideAngle;

	private bool m_isOnSlope;
	private bool firstLanding;

	[HideInInspector]public string currentState;

	[HideInInspector] public bool m_FacingRight = true;

	public float slopeCheckDistance;
	public float movementSpeed;

	//Animation State
	const string PLAYER_IDLE = "Player_Idle";
	const string PLAYER_RUN = "Player_Run";
	const string PLAYER_JUMP = "Player_Jump";

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_Collider2D = GetComponent<CapsuleCollider2D>();
		animator = GetComponent<Animator>();
		colliderSize = m_Collider2D.size;

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

    private void FixedUpdate()
	{
		CheckGround();
		CheckSlope();
		AnimationControl();
	}

	private void AnimationControl()
	{
		if (!m_Grounded && !m_isOnSlope)
			ChangeAnimationState(PLAYER_JUMP);
		else if (m_Rigidbody2D.velocity.x != 0)
			ChangeAnimationState(PLAYER_RUN);
		else
			ChangeAnimationState(PLAYER_IDLE);
	}

	private void CheckGround()
    {
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
                {
					firstLanding = true;
					OnLandEvent.Invoke();
				}
			}
		}
	}

	private void CheckSlope()
    {
		Vector2 checkPos = transform.position - new Vector3(0f, colliderSize.y);
		CheckSlopeOnVertical(checkPos);
		//CheckSlopeOnHorizontal(checkPos);
	}

	private void CheckSlopeOnHorizontal(Vector2 checkPos)
    {
		RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, Vector3.right, slopeCheckDistance, m_WhatIsGround);
		RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, Vector3.left, slopeCheckDistance, m_WhatIsGround);
       
		if (slopeHitFront)
		{
			m_isOnSlope = true;
			slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
			Debug.DrawLine(checkPos, slopeHitFront.point, Color.yellow);
		}
		else if (slopeHitBack)
		{
			m_isOnSlope = true;
			slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
			Debug.DrawRay(checkPos, slopeHitBack.point, Color.white);
		}
        else
        {
			m_isOnSlope = false;
			slopeSideAngle = 0f;
		}
    }

	private void CheckSlopeOnVertical(Vector2 checkPos)
    {
		RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector3.down, slopeCheckDistance, m_WhatIsGround);

        if (hit)
        {
			slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
			slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

			if(slopeDownAngle != slopeDownAngleOld)
            {
				m_isOnSlope = true;
            }
			slopeDownAngleOld = slopeDownAngle;

			Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
			Debug.DrawRay(hit.point, hit.normal, Color.green);
        }

		if(m_isOnSlope && Input.GetAxisRaw("Horizontal") == 0)
		{
			m_Collider2D.sharedMaterial = maxFriction;
		}
        else
        {
			m_Collider2D.sharedMaterial = noFriction;
		}
	}

	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			}
			else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			//Vector3 targetVelocity = new Vector2(move * 20f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			//m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			if (m_Grounded && !m_isOnSlope)
            {
				if(firstLanding)
					newVelocity.Set(move * movementSpeed, -20.0f);
				else
					newVelocity.Set(move * movementSpeed, 0.0f);
			}
            else if (m_Grounded && m_isOnSlope)
			{
				newVelocity.Set(-move * movementSpeed * slopeNormalPerp.x, -move * movementSpeed * slopeNormalPerp.y);
			}
            else if (!m_Grounded)
			{
				newVelocity.Set(move * movementSpeed, m_Rigidbody2D.velocity.y);
            }

            m_Rigidbody2D.velocity = newVelocity;
            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}

		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			newVelocity.Set(0.0f, 0.0f);
			m_Rigidbody2D.velocity = newVelocity;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void ChangeAnimationState(string newState)
    {
		if (currentState == newState) return;
		// play animation
		animator.Play(newState);
		currentState = newState;

	}
}