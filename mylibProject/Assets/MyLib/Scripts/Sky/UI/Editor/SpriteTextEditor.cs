using UnityEngine;
using UnityEditor;
using System.Collections;

[CanEditMultipleObjects, CustomEditor(typeof(SpriteTextPanel))]
public class SpriteTextEditor : Editor
{
	private SerializedObject mEditor;
	private SerializedProperty MLayoutType, SpriteTextElement, TextContent, MSpriteTexConfig;

	void OnEnable ()
	{
		mEditor = new SerializedObject (target);
		MLayoutType = mEditor.FindProperty ("MLayoutType");
		SpriteTextElement = mEditor.FindProperty ("SpriteTextElement");
		TextContent = mEditor.FindProperty ("_textContent");
		MSpriteTexConfig = mEditor.FindProperty ("MSpriteTexConfig");
	}

	public override void OnInspectorGUI ()
	{
		mEditor.Update ();
		
		EditorGUILayout.PropertyField (MLayoutType);
		EditorGUILayout.PropertyField (SpriteTextElement);
		EditorGUILayout.PropertyField (TextContent);
		EditorGUILayout.PropertyField (MSpriteTexConfig);
		
		if (
			mEditor.ApplyModifiedProperties () ||
			(Event.current.type == EventType.ValidateCommand &&
			Event.current.commandName == "UndoRedoPerformed")
			) {
			foreach (SpriteTextPanel s in targets) {
				if (PrefabUtility.GetPrefabType (s) != PrefabType.Prefab) {
					s.myUpdate ();
				}
			}
		}
	}
}
