using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	// Exposed variables
	[SerializeField]
	private float _movementSpeed;
	[SerializeField]
	private float _jumpSpeed;
	[SerializeField]
	private Transform _body;

	// Public variables
	private static PlayerController _instance;
	public static PlayerController Instance
	{
		get { return _instance; }
	}

	public bool Alive
	{
		get { return _alive; }
	}
	public bool Enabled
	{
		get { return _enabled; }
	}

	// Private variables
	private Animator _animator;
	private Rigidbody2D _rigidbody2D;

	private bool _enabled;
	private bool _alive;
	private bool _grounded;

	// Events
	public void Awake()
	{
		_instance = this;

		_animator = GetComponent<Animator>();
		_rigidbody2D = GetComponent<Rigidbody2D>();

		_enabled = true;
		_alive = true;
		_grounded = false;
	}
	public void Update()
	{
		if (!_enabled)
			return;

		_grounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, ~Layers.AllIgnoreRaycasts);
		_animator.SetBool("Airborne", !_grounded);

		float vertSpeed = _rigidbody2D.velocity.y;
		if (Input.GetAxis("Vertical") > 0.1f && _grounded)
		{
			vertSpeed = _jumpSpeed;
			_animator.SetTrigger("Jumped");
		}

		_rigidbody2D.velocity = new Vector2(
			Input.GetAxis("Horizontal") * _movementSpeed,
			vertSpeed);

		const float k_faceCutoff = 0.1f;
		if (Mathf.Abs(Input.GetAxis("Horizontal")) > k_faceCutoff)
		{
			_body.transform.localScale = new Vector3(Mathf.Sign(Input.GetAxis("Horizontal")), 1.0f, 1.0f);
			_animator.SetBool("Running", true);
		}
		else
			_animator.SetBool("Running", false);

		if (transform.position.y < Level.Instance.KillPlaneHeight)
			Die(Level.Instance.RandomFallDeath);

		if (Input.GetKeyDown(KeyCode.R))
			Reset();
	}

	// Public interface
	public void OnReacted()
	{
		_animator.SetTrigger("React");
	}
	public void OnResetDeath()
	{
		Die(Level.Instance.RandomResetDeath);
	}
	public void Disable()
	{
		_enabled = false;
	}
	public void Enable()
	{
		_enabled = true;
	}

	// Private interface
	private void Reset()
	{
		Disable();
		_animator.SetTrigger("Reset");
	}
	private void Die(string deathStatement)
	{
		Enable();
		_alive = false;
		HUDController.Instance.FadeOut(() => HUDController.Instance.ShowTextBubble(deathStatement, Level.Instance.Reset));
	}
}