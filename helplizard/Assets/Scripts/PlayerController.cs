using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// Exposed variables

	// Public variables
	private PlayerController _instance;
	public PlayerController Instance
	{
		get { return _instance; }
	}

	// Private variables
	private Rigidbody2D _rigidbody2D;

	// Events
	public void Awake()
	{
		_instance = this;
	}

	// Private interface
}