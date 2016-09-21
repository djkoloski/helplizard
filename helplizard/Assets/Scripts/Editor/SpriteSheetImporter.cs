using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SpriteSheetImporter : EditorWindow
{
	Texture diffuseTexture;
	int pixelsPerUnit;
	WrapMode wrap;
	AnimatorController controller;
	string directory;
	string spritePath;

	[MenuItem("HelpLizard/Import SpriteSheet")]
	static void Init()
	{
		SpriteSheetImporter window = GetWindow<SpriteSheetImporter>();
		window.Show();
	}

	void OnEnable()
	{
		pixelsPerUnit = 100;
		wrap = WrapMode.Loop;
	}

	void OnGUI()
	{
		diffuseTexture = (Texture)EditorGUILayout.ObjectField("Diffuse", diffuseTexture, typeof(Texture), false);
		pixelsPerUnit = EditorGUILayout.IntField("Pixels per Unit", pixelsPerUnit);
		wrap = (WrapMode)EditorGUILayout.EnumPopup("Wrap", wrap);
		controller = (AnimatorController)EditorGUILayout.ObjectField("Controller", controller, typeof(AnimatorController), false);
		spritePath = EditorGUILayout.TextField("Sprite path", spritePath);
		if (GUILayout.Button("Import"))
			ImportAnimation();

		directory = EditorGUILayout.TextField("Directory", directory);
		if (GUILayout.Button("Import Directory"))
			ImportDirectory();

		if (GUILayout.Button("Fixup Center"))
			FixupCenter();
	}

	void ImportDirectory()
	{
		string[] fileEntries = Directory.GetFiles(Application.dataPath + "/" + directory);
		foreach (string fileName in fileEntries)
		{
			int index = fileName.LastIndexOf("\\");
			string localPath = "Assets/" + directory;

			if (index > 0)
				localPath += fileName.Substring(index).Replace('\\', '/');

			diffuseTexture = (Texture)AssetDatabase.LoadAssetAtPath(localPath, typeof(Texture));

			if (diffuseTexture != null)
				ImportAnimation();
		}
		diffuseTexture = null;
	}

	void ImportAnimation()
	{
		string diffusePath = AssetDatabase.GetAssetPath(diffuseTexture);

		string[] pieces = Path.GetFileNameWithoutExtension(diffusePath).Split('_');

		int fps = int.Parse(pieces[pieces.Length - 3].Substring(0, pieces[pieces.Length - 3].Length - 3));

		string[] frameSizePieces = pieces[pieces.Length - 2].Split('x');
		int frameWidth = int.Parse(frameSizePieces[0]);
		int frameHeight = int.Parse(frameSizePieces[1]);

		int frameCount = int.Parse(pieces[pieces.Length - 1].Substring(5));

		string[] namePieces = new string[pieces.Length - 3];
		System.Array.Copy(pieces, namePieces, pieces.Length - 3);
		string name = string.Join("_", namePieces);

		TextureImporter importer = AssetImporter.GetAtPath(diffusePath) as TextureImporter;

		importer.textureType = TextureImporterType.Sprite;
		importer.maxTextureSize = 8192;
		importer.textureFormat = TextureImporterFormat.AutomaticTruecolor;
		importer.spriteImportMode = SpriteImportMode.Multiple;
		importer.filterMode = FilterMode.Point;
		importer.spritePivot = Vector2.zero;
		importer.spritePixelsPerUnit = pixelsPerUnit;
		importer.mipmapEnabled = false;

		SpriteMetaData[] spritesheet = new SpriteMetaData[frameCount];
		for (int i = 0; i < frameCount; ++i)
		{
			SpriteMetaData meta = new SpriteMetaData();
			meta.alignment = (int)SpriteAlignment.BottomLeft;
			meta.name = name + "_" + i.ToString();
			meta.pivot = Vector2.zero;
			meta.rect = new Rect(i * frameWidth, 0, frameWidth, frameHeight);
			spritesheet[i] = meta;
		}

		importer.spritesheet = spritesheet;

		AssetDatabase.ImportAsset(diffusePath);

		Object[] assets = AssetDatabase.LoadAllAssetRepresentationsAtPath(diffusePath);
		Sprite[] sprites = new Sprite[frameCount];

		foreach (Object asset in assets)
		{
			if (asset.name == Path.GetFileNameWithoutExtension(diffusePath))
				continue;

			string[] assetNamePieces = asset.name.Split('_');
			int index = int.Parse(assetNamePieces[assetNamePieces.Length - 1]);
			sprites[index] = asset as Sprite;
		}

		AnimationClip clip = new AnimationClip();
		clip.name = name;
		clip.frameRate = fps;
		clip.wrapMode = wrap;

		AnimationClipSettings settings = AnimationUtility.GetAnimationClipSettings(clip);
		settings.loopTime = true;
		AnimationUtility.SetAnimationClipSettings(clip, settings);

		EditorCurveBinding binding = new EditorCurveBinding();
		binding.type = typeof(SpriteRenderer);
		binding.path = spritePath;
		binding.propertyName = "m_Sprite";

		ObjectReferenceKeyframe[] keyframes = new ObjectReferenceKeyframe[frameCount];
		for (int i = 0; i < frameCount; ++i)
		{
			keyframes[i] = new ObjectReferenceKeyframe();
			keyframes[i].time = i / (float)fps;
			keyframes[i].value = sprites[i];
		}

		AnimationUtility.SetObjectReferenceCurve(clip, binding, keyframes);

		if (controller.layers.Length == 0)
			controller.AddLayer("Base Layer");

		controller.AddMotion(clip, 0);

		AssetDatabase.CreateAsset(clip, "Assets/" + name + ".anim");
		AssetDatabase.Refresh();
	}

	void FixupCenter()
	{
		string diffusePath = AssetDatabase.GetAssetPath(diffuseTexture);

		TextureImporter importer = AssetImporter.GetAtPath(diffusePath) as TextureImporter;

		SpriteMetaData[] spritesheet = new SpriteMetaData[importer.spritesheet.Length];
		spritesheet[0] = importer.spritesheet[0];
		int alignment = spritesheet[0].alignment;
		Vector2 pivot = spritesheet[0].pivot;
		for (int i = 1; i < spritesheet.Length; ++i)
		{
			spritesheet[i] = importer.spritesheet[i];
			spritesheet[i].alignment = alignment;
			spritesheet[i].pivot = pivot;
		}

		importer.spritePivot = pivot;
		importer.spritesheet = spritesheet;

		AssetDatabase.ImportAsset(diffusePath, ImportAssetOptions.ForceUpdate);
		AssetDatabase.Refresh();
	}
}