using UnityEngine;
using System.Collections;

public class EndGoal : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other)
	{
		Level.Instance.NextScene();
	}
}
