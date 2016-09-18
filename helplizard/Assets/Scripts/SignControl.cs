using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SignControl : MonoBehaviour, IPointerClickHandler {

	public Canvas canvas;
	public GameObject guiSign;
	public string signText;

	public void OnPointerClick(PointerEventData data)
	{
		GameObject gui = (GameObject)Instantiate(guiSign);
		gui.transform.SetParent(canvas.transform);
		RectTransform rect = gui.GetComponent<RectTransform>();
		rect.offsetMax = new Vector2(0, 0);
		rect.offsetMin = new Vector2(0, 0);
		Text text = gui.transform.GetChild(0).GetComponent<Text>();
		text.text = signText;
	}
}
