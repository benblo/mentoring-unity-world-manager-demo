using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(menuName = "Game/Level Definition")]
public class LevelDefinition : ScriptableObject
{
#if UNITY_EDITOR
	public UnityEditor.SceneAsset[] scenes;
#else
	public Object[] scenes;
#endif
}
