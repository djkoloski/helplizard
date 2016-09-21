using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIControl : MonoBehaviour, IPointerClickHandler
{
	// Exposed variables
	[SerializeField]
	private Text _text;

	// Private variables
	private Animator _animator;

	private Callback _callback;

	// Events
	public void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	// Public interface
	public void SetText(string text)
	{
		_text.text = text;
	}
	public void SetCallback(Callback callback)
	{
		_callback = callback;
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		CameraController.Instance.ZoomOut();
		_animator.Play("sign_exit");
	}
	public void Exit()
	{
		Callback temp = _callback;
		_callback = null;
		if (temp != null)
			temp();

		Destroy(gameObject);
	}
}
