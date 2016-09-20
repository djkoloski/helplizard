using UnityEngine;

public class Level : MonoBehaviour
{
	// Exposed variables
	[SerializeField]
	private float _killPlaneHeight;

	// Public variables
	private static Level _instance;
	public static Level Instance
	{
		get { return _instance; }
	}

	public float KillPlaneHeight
	{
		get { return _killPlaneHeight; }
	}

	// Events
	public void Awake()
	{
		_instance = this;
	}
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(new Vector3(-100.0f, _killPlaneHeight), new Vector3(100.0f, _killPlaneHeight));
	}
}