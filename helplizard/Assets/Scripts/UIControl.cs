using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UIControl : MonoBehaviour, IPointerClickHandler {

	public void OnPointerClick(PointerEventData eventData)
	{
		Destroy(gameObject);
	}
}
