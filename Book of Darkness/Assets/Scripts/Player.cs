using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
	public static Player instance;

	[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;

	[HideInInspector]
	public Rigidbody2D body;
	[HideInInspector]
	public bool facingRight, canEnter = true;
	private Vector3 velocity = Vector3.zero;
	private SpriteRenderer sRenderer;
	private SpriteRenderer[] subsprites;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }
	public event Action GameOverEvent;
	public event Action RespawnEvent;

	public bool canHide = false;
	public PlayerState currentState;
	public PlayerState ExposedState,HidingState;
	public string scene;

	public SpriteRenderer screen;
	public bool invincible = false;
	Vector3 startingPos;
	string startingScene;

	float health = 3f;
	float initialHealth = 3f;

	Animator anim;

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
		scene = transform.parent.name;

		startingPos = transform.position;
		startingScene = scene;
		initialHealth = health;
	}

	private void Start() 
	{
		ExposedState = new ExposedState(this);
		HidingState = new HidingState(this);
		currentState = ExposedState;

		anim = GetComponent<Animator>();
	}

	public void Move(float move) {

		anim.SetFloat("Speed", Mathf.Abs(move));

		Vector3 targetVelocity = new Vector2(move * 10f, body.velocity.y);
		body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref velocity, movementSmoothing);

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

	public void PlayFootstep()
    {
		var pitch = UnityEngine.Random.Range(1f, 1.6f);
		AudioManager.instance.PlayClipAtPoint("Footstep", transform.position, 0.3f, pitch);
    }

	public void StopMovement()
    {
		body.velocity = Vector2.zero;
    }

	private void Flip()
	{
		Vector3 theScale = transform.localScale;

		facingRight = !facingRight;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void HidePlayer(bool hideBool)
    {
		sRenderer.enabled = hideBool;
		for (int i = 0; i < subsprites.Length; i++) subsprites[i].enabled = !hideBool;
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

	public void TakeHit(float damage)
    {
		if (invincible || IsHiding()) return;

		health -= damage;
		StartCoroutine(SetInvincible());
		Debug.Log("HIT TAKEN");

		if (health <= 0)
			GameOver();
    }

	public IEnumerator SetInvincible()
    {
		invincible = true;
		yield return new WaitForSeconds(1);
		invincible = false;
    }

	void GameOver()
    {
		screen.color = Color.black;
		transform.position = startingPos;
		scene = startingScene;
		PlayerTorch.instance.SetActive(false);

		GameOverEvent?.Invoke();
		StartCoroutine(FadeScreen());
		health = initialHealth;
    }

	IEnumerator FadeScreen()
    {
		yield return new WaitForSeconds(0.5f);
		var percent = 0f;
		var initColor = screen.color;

		while (percent < 1)
		{
			percent += Time.deltaTime;
			screen.color = Color.Lerp(initColor, Color.clear, percent);
			yield return null;
		}

		yield return new WaitForSeconds(1.5f);
		RespawnEvent?.Invoke();
	}
}