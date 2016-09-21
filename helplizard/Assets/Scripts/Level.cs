using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
	// Exposed variables
	[SerializeField]
	private float _killPlaneHeight;
	[SerializeField]
	private List<string> _fallDeathStatements;
	[SerializeField]
	private List<string> _resetDeathStatements;

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
	public string RandomFallDeath
	{
		get { return _fallDeathStatements[Random.Range(0, _fallDeathStatements.Count)]; }
	}
	public string RandomResetDeath
	{
		get { return _resetDeathStatements[Random.Range(0, _resetDeathStatements.Count)]; }
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
	public void Reset()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void NextScene()
	{
		int i = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(i+1);
	}
}