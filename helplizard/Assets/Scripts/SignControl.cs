using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SignControl : MonoBehaviour
{
	public GameObject guiSign;
	public string signText;

	private Animator _animator;

	void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
			if (hit.collider != null && hit.collider.gameObject == gameObject)
			{
				_animator.Play("sign_click");
				CameraController.Instance.ZoomIn();
			}
		}
	}
	
	public void InstantiateCanvas()
	{
		GameObject gui = Instantiate(guiSign);
		gui.transform.SetParent(HUDController.Instance.Canvas.transform);
		RectTransform rect = gui.GetComponent<RectTransform>();
		rect.offsetMax = new Vector2(0, 0);
		rect.offsetMin = new Vector2(0, 0);
		Text text = gui.transform.FindChild("Text").GetComponent<Text>();
		text.text = signText;
		_animator.Play("sign_idle");
	}
}
