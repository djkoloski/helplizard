using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SignControl : MonoBehaviour, IPointerClickHandler {

	public Canvas canvas;
	public GameObject guiSign;
	public string signText;

	private Animator _animator;

	void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public void OnPointerClick(PointerEventData data)
	{
		_animator.Play("sign_click");
	}
	
	public void InstantiateCanvas()
	{
		GameObject gui = (GameObject)Instantiate(guiSign);
		gui.transform.SetParent(canvas.transform);
		RectTransform rect = gui.GetComponent<RectTransform>();
		rect.offsetMax = new Vector2(0, 0);
		rect.offsetMin = new Vector2(0, 0);
		Text text = gui.transform.GetChild(0).GetComponent<Text>();
		text.text = signText;
		_animator.Play("sign_idle");
	}
}
