using UnityEngine;

public class CameraController : MonoBehaviour
{
	// Exposed variables
	[SerializeField]
	private float _moveTime;

	// Public variables
	private static CameraController _instance;
	public static CameraController Instance
	{
		get { return _instance; }
	}

	// Private variables
	private Vector3 _velocity;

	// Events
	public void Awake()
	{
		_instance = this;

		_velocity = Vector3.zero;
	}
	public void FixedUpdate()
	{
		if (!PlayerController.Instance.Alive)
			return;

		Vector3 position = Vector3.SmoothDamp(transform.position, PlayerController.Instance.transform.position, ref _velocity, _moveTime);
		position.z = transform.position.z;
		transform.position = position;
	}
}