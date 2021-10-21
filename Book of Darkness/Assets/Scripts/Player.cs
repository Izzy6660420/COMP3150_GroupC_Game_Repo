//From https://github.com/Brackeys/2D-Character-Controller

using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
	public static Player instance;

	[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;

	public Rigidbody2D body;
	[HideInInspector]
	public bool facingRight, canEnter = true;
	private Vector3 velocity = Vector3.zero;
	private SpriteRenderer sRenderer;
	private SpriteRenderer[] subsprites;

	[HideInInspector]
	public Torch torch;
	private TorchUI torchUI;
	private PanicUI panicUI;
	private GameObject torchBar;
	private GameObject panicBar;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private bool canHide = false;
	public PlayerState currentState;
	public PlayerState ExposedState,HidingState;
	public string scene;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.Log("More than one instance of CharacterController2D detected!");
		}
		instance = this;

		body = GetComponent<Rigidbody2D>();
		sRenderer = GetComponent<SpriteRenderer>();
		subsprites = GetComponentsInChildren<SpriteRenderer>();
		torch = GetComponent<Torch>();
		scene = transform.parent.name;
	}

	private void Start() 
	{
		ExposedState = new ExposedState(this);
		HidingState = new HidingState(this);
		currentState = ExposedState;
		torchUI = TorchUI.instance;
		panicUI = PanicUI.instance;

		torchBar = torchUI.gameObject;
		panicBar = panicUI.gameObject;

		torch.SetActive(false);
		torchBar.SetActive(false);
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

		// Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(move * 10f, body.velocity.y);
		// Smooth it out and apply it to the player
		body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref velocity, movementSmoothing);

		// Character direction by cursor position
		Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(body.position);
		if (dir.x > 0 && !facingRight)
		{
			Flip();
		}
		else if (dir.x < 0 && facingRight)
		{
			Flip();
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
		sRenderer.enabled = hideBool;
		torch.SetActive(false);
		torchBar.SetActive(false);
		panicBar.SetActive(!hideBool);

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