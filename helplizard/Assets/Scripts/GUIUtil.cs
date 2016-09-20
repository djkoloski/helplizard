using UnityEngine;
using System.Collections.Generic;

public static class GUIUtil
{
	public static void DrawString(string text, Vector3 worldPos, Color color)
	{
#if UNITY_EDITOR
		Vector3 screenPos = UnityEditor.SceneView.lastActiveSceneView.camera.WorldToScreenPoint(worldPos);
		if (screenPos.z < 0)
			return;

		Vector3 offset = UnityEditor.SceneView.lastActiveSceneView.camera.transform.right * 0.5f + Vector3.up * 0.25f;
		UnityEditor.Handles.BeginGUI();
		GUIStyle style = new GUIStyle(GUI.skin.label);
		style.normal.textColor = color;
		UnityEditor.Handles.Label(worldPos + offset, text, style);
		UnityEditor.Handles.EndGUI();
#endif
	}
}