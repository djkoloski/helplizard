using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
	// Exposed variables
	[SerializeField]
	private GameObject _textBubblePrefab;
	[SerializeField]
	private GameObject _narratorPrefab;

	// Public variables
	private static HUDController _instance;
	public static HUDController Instance
	{
		get { return _instance; }
	}

	public Canvas Canvas
	{
		get { return _canvas; }
	}

	// Private variables
	private Animator _animator;
	private Canvas _canvas;

	private Callback _callback;

	// Events
	public void Awake()
	{
		_instance = this;

		_animator = GetComponent<Animator>();
		_canvas = GetComponent<Canvas>();

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
	public void FadeIn(Callback callback)
	{
		_callback = callback;
		_animator.Play("FadeIn");
	}
	public void OnFadeInFinished()
	{
		if (_callback != null)
		{
			Callback temp = _callback;
			_callback = null;
			temp();
		}
	}
	public void ShowTextBubble(string content, Callback callback)
	{
		GameObject textBubbleGO = Instantiate(_textBubblePrefab);
		textBubbleGO.transform.SetParent(_canvas.transform);
		RectTransform rect = textBubbleGO.GetComponent<RectTransform>();
		rect.offsetMax = rect.offsetMin = Vector2.zero;

		UIControl textBubble = textBubbleGO.GetComponent<UIControl>();
		textBubble.SetText(content);
		textBubble.SetCallback(callback);
	}
	public void ShowNarrator(string content, NarratorType type, Callback callback)
	{
		GameObject narratorGO = Instantiate(_narratorPrefab);
		narratorGO.transform.SetParent(_canvas.transform);
		RectTransform rect = narratorGO.GetComponent<RectTransform>();
		rect.offsetMax = rect.offsetMin = new Vector2(50.0f, 50.0f);

		NarratorController narrator = narratorGO.GetComponent<NarratorController>();
		narrator.SetText(content);
		narrator.SetCallback(callback);
		narrator.SetType(type);
	}
}