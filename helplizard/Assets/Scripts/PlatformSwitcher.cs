using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlatformSwitcher : MonoBehaviour {

	public GameObject platform;
	public float platformSpeed;
	public string ID;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
			if(hit.collider != null && hit.collider.name == "EmptyPlatform_" + ID)
			{
				StartCoroutine(MoveOverSpeed(platform, this.transform.position, platformSpeed));
			}

		}
	}

	public IEnumerator MoveOverSpeed(GameObject platform, Vector3 end, float speed)
	{
		while (platform.transform.position != end)
		{
			platform.transform.position = Vector3.MoveTowards(platform.transform.position, end, speed * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, platform.transform.position);
	}
}
