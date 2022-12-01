using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(menuName = "Game/World Definition")]
public class WorldDefinition : ScriptableObject
{
#if UNITY_EDITOR
	public UnityEditor.SceneAsset mainScene;
#else
	public Object mainScene;
#endif
	public LevelDefinition[] levels;
}
