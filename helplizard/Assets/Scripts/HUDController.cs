using UnityEngine;

public class HUDController : MonoBehaviour
{
	// Public variables
	private static HUDController _instance;
	public static HUDController Instance
	{
		get { return _instance; }
	}

	// Private variables
	private Animator _animator;

	private Callback _callback;

	// Events
	public void Awake()
	{
		_instance = this;

		_animator = GetComponent<Animator>();

		_callback = null;
	}

	// Public interface
	public void FadeOut(Callback callback)
	{
		_callback = callback;
		_animator.Play("FadeOut");
	}
	public void OnFadeOutFinished()
	{
		if (_callback != null)
		{
			Callback temp = _callback;
			_callback = null;
			temp();
		}
	}
}