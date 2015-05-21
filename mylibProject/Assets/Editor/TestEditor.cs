using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects, CustomEditor(typeof(TestForEditor))]
public class TestEditor : Editor
{
	private static Vector3 pointSnap = Vector3.one * 0.1f;
	private SerializedObject testForEditor;
	private SerializedProperty point;

	void OnEnable ()
	{
		testForEditor = new SerializedObject (target);
		point = testForEditor.FindProperty ("point");
	}

	private static GUILayoutOption
		buttonWidth = GUILayout.MaxWidth (20f),
		colorWidth = GUILayout.MaxWidth (50f);

	public override void OnInspectorGUI ()
	{
		testForEditor.Update ();
		GUILayout.Label ("Points");
		EditorGUILayout.PropertyField (point);
		if (
			testForEditor.ApplyModifiedProperties () ||
			(Event.current.type == EventType.ValidateCommand &&
			Event.current.commandName == "UndoRedoPerformed")
			) {
			foreach (TestForEditor s in targets) {
				if (PrefabUtility.GetPrefabType (s) != PrefabType.Prefab) {
					s.myUpdate ();
				}
			}
		}
	}

	void OnSceneGUI ()
	{
		TestForEditor testForEditor = (TestForEditor)target;
		Transform starTransform = testForEditor.transform;
		Quaternion rotation = Quaternion.Euler (0f, 0f, 0f);
		testForEditor.getNewSize ();
		Vector3 temp = SkyUtil.reletiveToLocal (testForEditor.point,testForEditor.parentWidth,testForEditor.parentHight);

		Vector3 oldPoint = starTransform.TransformPoint (rotation * temp);
		Vector3 newPoint = Handles.FreeMoveHandle
				(oldPoint, Quaternion.identity, 0.1f, pointSnap, Handles.DotCap);
		if (oldPoint != newPoint) {


			testForEditor.point = Quaternion.Inverse (rotation) *
				starTransform.InverseTransformPoint (newPoint);
			testForEditor.point.x = testForEditor.point.x/testForEditor.parentWidth+0.5f;
			testForEditor.point.y = testForEditor.point.y/testForEditor.parentHight+0.5f;
			testForEditor.myUpdate ();
		}
	}
}
