using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class WorldWindow : EditorWindow
{
	[MenuItem("Demo/World Window")]
	static void Open()
	{
		GetWindow<WorldWindow>();
	}

	public WorldDefinition world;
	void OnGUI()
	{
		world = (WorldDefinition)EditorGUILayout.ObjectField("World", world, typeof(WorldDefinition), false);
		if (!world)
		{
			return;
		}

		GUILayout.BeginHorizontal();
		GUILayout.Label("main");
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Open"))
		{
			var scenePath = AssetDatabase.GetAssetPath(world.mainScene);
			EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
		}
		if (GUILayout.Button("Close"))
		{
			var scenePath = AssetDatabase.GetAssetPath(world.mainScene);
			var scene = SceneManager.GetSceneByPath(scenePath);
			EditorSceneManager.CloseScene(scene, true);
		}
		GUILayout.EndHorizontal();

		foreach (var level in world.levels)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label(level.name);
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Open"))
			{
				Open(level);
			}
			if (GUILayout.Button("Close"))
			{
				Close(level);
			}
			GUILayout.EndHorizontal();
		}

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Open all"))
		{
			foreach (var level in world.levels)
			{
				Open(level);
			}
		}
		if (GUILayout.Button("Close all"))
		{
			foreach (var level in world.levels)
			{
				Close(level);
			}
		}
		GUILayout.EndHorizontal();
	}

	static void Open(LevelDefinition level)
	{
		foreach (var sceneAsset in level.scenes)
		{
			var scenePath = AssetDatabase.GetAssetPath(sceneAsset);
			var scene = SceneManager.GetSceneByPath(scenePath);
			if (scene.isLoaded)
				continue; // already open

			scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
			var roots = scene.GetRootGameObjects();
			var content = Array.Find(roots, r => r.GetComponent<SceneContent>());
			Debug.Log($"loaded scene <{scene.name}>, content <{(content ? content.name : "null")}>", content);
		}
	}

	static void Close(LevelDefinition level)
	{
		foreach (var sceneAsset in level.scenes)
		{
			var scenePath = AssetDatabase.GetAssetPath(sceneAsset);
			var scene = SceneManager.GetSceneByPath(scenePath);
			if (!scene.isLoaded)
				continue; // already closed

			EditorSceneManager.CloseScene(scene, true);
		}
	}
}
