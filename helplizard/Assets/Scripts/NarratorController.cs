using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum NarratorType
{
	Happy,
	Smug,
	Spooky
}

public class NarratorController : MonoBehaviour, IPointerClickHandler
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
	public void Start()
	{
		PlayerController.Instance.Disable();
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
	public void SetType(NarratorType type)
	{
		_animator.SetInteger("NarratorType", (int)type);
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		CameraController.Instance.ZoomOut();
		_animator.SetTrigger("Closed");
	}
	public void OnSpeak()
	{
		_animator.SetTrigger("Speak");
	}
	public void Exit()
	{
		Callback temp = _callback;
		_callback = null;
		if (temp != null)
			temp();

		Destroy(gameObject);

		PlayerController.Instance.Enable();
	}
}
