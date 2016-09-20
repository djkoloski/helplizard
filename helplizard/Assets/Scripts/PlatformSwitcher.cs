﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlatformSwitcher : MonoBehaviour {

	public GameObject platform;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
			if(hit.collider != null)
			{
				StartCoroutine(MoveOverSeconds(platform, this.transform.position, 2f));
			}

		}
	}

	public IEnumerator MoveOverSpeed(GameObject platform, Vector3 end, float speed)
	{
		// speed should be 1 unit per second
		while (platform.transform.position != end)
		{
			platform.transform.position = Vector3.MoveTowards(platform.transform.position, end, speed * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
	}

	public IEnumerator MoveOverSeconds(GameObject platform, Vector3 end, float seconds)
	{
		float elapsedTime = 0;
		Vector3 startingPos = platform.transform.position;
		while (elapsedTime < seconds)
		{
			platform.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		transform.position = end;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, platform.transform.position);
	}
}
