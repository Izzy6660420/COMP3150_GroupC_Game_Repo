//From https://github.com/Brackeys/2D-Character-Controller

using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f;  // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	private SpriteRenderer m_Renderer;
	private SpriteRenderer[] m_playerSubSprites;
	public Torch m_playerTorch;
	public TorchUI torchUI;
	private float torchBarOffset = 1.5f;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private bool canHide = false;
	public PlayerState currentState;
	public PlayerState ExposedState,HidingState;

	private void Awake() 
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_Renderer = GetComponent<SpriteRenderer>();
		m_playerSubSprites = GetComponentsInChildren<SpriteRenderer>();
		m_playerTorch = GetComponent<Torch>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void Start() 
	{
		ExposedState = new ExposedState(this);
		HidingState = new HidingState(this);
		currentState = ExposedState;
	}

	private void FixedUpdate() 
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
					OnLandEvent.Invoke();
				}
			}
		}
	}

	public void Update()
    {
		torchUI.setLocation(new Vector3(transform.position.x, transform.position.y + torchBarOffset, 0.0f));
    }

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Furniture"))
        {
			canHide = true;
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Furniture"))
		{
			canHide = false;
		}
	}

	public void Move(float move, bool jump) {

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// This block of code controls the character's direction by cursor position
			Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(m_Rigidbody2D.position);
			if(dir.x > 0 && !m_FacingRight)
			{
				Flip();
			} else if(dir.x < 0 && m_FacingRight)
			{
				Flip();
			}
		}
		// Jumping
		if (m_Grounded && jump)
		{
			m_Grounded = false;
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

	public void HidePlayer(bool hideBool)
    {
		Physics2D.IgnoreLayerCollision(3, 7,  !hideBool);
		m_Renderer.enabled = hideBool;
		m_playerTorch.SetActive(!hideBool);
		torchUI.enabled = !hideBool;

		for (int i = 0; i < m_playerSubSprites.Length; i++)
		{
			m_playerSubSprites[i].enabled = !hideBool;
		}
	}

	public bool canHideInf()
	{
		return canHide;
	}
}