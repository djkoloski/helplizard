using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UIControl : MonoBehaviour, IPointerClickHandler {

	private Animator _animator;

	void Awake()
	{
		_animator = GetComponent<Animator>();
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		_animator.Play("sign_exit");
	}

	public void Exit()
	{
		Destroy(gameObject);
	}
}
