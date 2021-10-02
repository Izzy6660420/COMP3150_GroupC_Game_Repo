//From https://github.com/Brackeys/2D-Character-Controller

using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
	public static Player instance;

	[SerializeField] private float jumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool airControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask whatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform groundCheck;                           // A position marking where to check if the player is grounded.

	const float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool grounded;            // Whether or not the player is grounded.
	new public Rigidbody2D rigidbody;
	public bool facingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;
	new private SpriteRenderer renderer;
	private SpriteRenderer[] subsprites;

	public Torch playerTorch;
	private TorchUI torchUI;
	private PanicUI panicUI;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private bool canHide = false;
	public PlayerState currentState;
	public PlayerState ExposedState,HidingState;
	public bool canEnter = true;
	public string scene;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.Log("More than one instance of CharacterController2D detected!");
		}
		instance = this;

		rigidbody = GetComponent<Rigidbody2D>();
		renderer = GetComponent<SpriteRenderer>();
		subsprites = GetComponentsInChildren<SpriteRenderer>();
		playerTorch = GetComponent<Torch>();
		scene = transform.parent.name;

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void Start() 
	{
		ExposedState = new ExposedState(this);
		HidingState = new HidingState(this);
		currentState = ExposedState;
		torchUI = TorchUI.instance;
		panicUI = PanicUI.instance;
	}

	private void FixedUpdate() 
	{
		bool wasGrounded = grounded;
		grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			GameObject go = colliders[i].gameObject;
			if (go != gameObject & go.CompareTag("Ground"))
			{
				grounded = true;
				if (!wasGrounded)
                {
					OnLandEvent.Invoke();
				}
			}
		}
	}

	public void Update()
    {
		torchUI.SetCamera(DimensionController.Instance.MainCam());
		panicUI.SetCamera(DimensionController.Instance.MainCam());
    }

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Hideable"))
        {
			canHide = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Hideable"))
		{
			canHide = false;
		}
	}

	public void Move(float move, bool jump) {

		// Only control the player if grounded or airControl is turned on
		if (grounded || airControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, rigidbody.velocity.y);
			// Smooth it out and apply it to the player
			rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref velocity, movementSmoothing);

			// Character direction by cursor position
			Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(rigidbody.position);
			if(dir.x > 0 && !facingRight)
			{
				Flip();
			} else if(dir.x < 0 && facingRight)
			{
				Flip();
			}
		}
		// Jumping
		if (grounded && jump)
		{
			grounded = false;
			rigidbody.AddForce(new Vector2(0f, jumpForce));
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void HidePlayer(bool hideBool)
    {
		Physics2D.IgnoreLayerCollision(3, 7,  hideBool);
		renderer.enabled = hideBool;
		playerTorch.SetActive(!hideBool);
		torchUI.enabled = !hideBool;

		for (int i = 0; i < subsprites.Length; i++)
		{
			subsprites[i].enabled = !hideBool;
        }
    }

    public bool canHideInf()
    {
        return canHide;
    }

	public void ChangeScene(string newScene)
    {
		scene = newScene;
    }

    public bool CompareScene(string other)
    {
		return other == scene;
    }

	public bool IsHiding()
    {
		return (currentState == HidingState);
    }
}