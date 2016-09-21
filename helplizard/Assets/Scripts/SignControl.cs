using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SignControl : MonoBehaviour
{
	public string signText;
	[SerializeField]
	private bool _isNarrator;
	[SerializeField]
	private NarratorType _narratorType;
	[SerializeField]
	private bool _isButt;

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
				PlayerController.Instance.OnReacted();
				CameraController.Instance.ZoomIn();
			}
		}
	}

	public void InstantiateCanvas()
	{
		if (_isNarrator)
		{
			HUDController.Instance.ShowNarrator(signText, _narratorType, null);
		}
		else if (_isButt)
		{
			HUDController.Instance.ShowButt(_isButt, null);
		}
		else
		{
			HUDController.Instance.ShowTextBubble(signText, null);
		}
		_animator.Play("sign_idle");
	}
}
