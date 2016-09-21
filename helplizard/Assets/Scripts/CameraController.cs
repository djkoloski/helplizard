using UnityEngine;

public class CameraController : MonoBehaviour
{
	// Exposed variables
	[SerializeField]
	private float _pixelsPerUnit;
	[SerializeField]
	private float _moveTime;
	[SerializeField]
	private Vector2 _lookOffset;
	[SerializeField]
	private float _jitterAmount;

	// Public variables
	private static CameraController _instance;
	public static CameraController Instance
	{
		get { return _instance; }
	}

	// Private variables
	private Animator _animator;
	private Camera _camera;

	private Vector3 _velocity;
	private Vector3 _position;

	// Events
	public void Awake()
	{
		_instance = this;

		_animator = GetComponent<Animator>();
		_camera = GetComponent<Camera>();

		_velocity = Vector3.zero;
	}
	public void Start()
	{
		_position = PlayerController.Instance.transform.position + (Vector3)_lookOffset;
		_position.z = transform.position.z;
		transform.position = _position;
	}
	public void FixedUpdate()
	{
		if (!PlayerController.Instance.Alive)
			return;

		_camera.orthographicSize = Screen.height / _pixelsPerUnit / 2.0f;
		_camera.pixelRect = new Rect(0, 0, Screen.width / 2 * 2, Screen.height / 2 * 2);

		_position = Vector3.SmoothDamp(transform.position, PlayerController.Instance.transform.position + (Vector3)_lookOffset, ref _velocity, _moveTime);
		_position.z = transform.position.z;
		transform.position = _position + (Vector3)Random.insideUnitCircle * _jitterAmount;
	}

	// Public interface
	public void ZoomIn()
	{
		_animator.SetBool("Zoomed", true);
	}
	public void ZoomOut()
	{
		_animator.SetBool("Zoomed", false);
	}
}