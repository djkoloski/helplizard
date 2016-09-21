using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	// Exposed variables
	[SerializeField]
	private float _movementSpeed;
	[SerializeField]
	private float _jumpSpeed;

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

	// Private variables
	private Rigidbody2D _rigidbody2D;
	private bool _alive;
	private bool _grounded;

	// Events
	public void Awake()
	{
		_instance = this;

		_rigidbody2D = GetComponent<Rigidbody2D>();
		_alive = true;
		_grounded = false;
	}
	public void Update()
	{
		if (!_alive)
			return;

		_grounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, ~Layers.AllIgnoreRaycasts);

		float vertSpeed = _rigidbody2D.velocity.y;
		if (Input.GetAxis("Vertical") > 0.1f && _grounded)
			vertSpeed = _jumpSpeed;

		_rigidbody2D.velocity = new Vector2(
			Input.GetAxis("Horizontal") * _movementSpeed,
			vertSpeed);

		if (transform.position.y < Level.Instance.KillPlaneHeight)
			Die(Level.Instance.RandomFallDeath);
	}

	// Private interface
	private void Die(string deathStatement)
	{
		_alive = false;

		HUDController.Instance.FadeOut(() => HUDController.Instance.ShowTextBubble(deathStatement, Level.Instance.Reset));
	}
}