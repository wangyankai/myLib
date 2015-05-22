using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects, CustomEditor(typeof(SkyMoveByCurve))]
public class SkyMoveByCurveEditor : Editor
{
	private static Vector3 pointSnap = Vector3.one * 0.1f;
	private SerializedObject mEditor;
	private SerializedProperty loop, AutoRun, PlayTime, DelayTime, AutoStartDelayTime, points;
	private int teleportingElement;
	private static GUIContent
		insertContent = new GUIContent ("+", "duplicate this point"),
		deleteContent = new GUIContent ("-", "delete this point"),
		pointContent = GUIContent.none,
		teleportContent = new GUIContent ("T");

	void OnEnable ()
	{
		mEditor = new SerializedObject (target);
		loop = mEditor.FindProperty ("loop");
		AutoRun = mEditor.FindProperty ("AutoRun");
		PlayTime = mEditor.FindProperty ("PlayTime");
		DelayTime = mEditor.FindProperty ("DelayTime");
		AutoStartDelayTime = mEditor.FindProperty ("AutoStartDelayTime");
		points = mEditor.FindProperty ("targets");

		teleportingElement = -1;
		teleportContent.tooltip = "start teleporting this point";
	}
	
	private static GUILayoutOption
		buttonWidth = GUILayout.MaxWidth (30f),
		colorWidth = GUILayout.MaxWidth (200f);
	
	public override void OnInspectorGUI ()
	{
		mEditor.Update ();
		GUILayout.Label ("Points");
		EditorGUILayout.PropertyField (loop);
		EditorGUILayout.PropertyField (AutoRun);
		EditorGUILayout.PropertyField (PlayTime);
		EditorGUILayout.PropertyField (DelayTime);
		EditorGUILayout.PropertyField (AutoStartDelayTime);

		EditorGUILayout.PropertyField (points);


		for (int i = 0; i < points.arraySize; i++) {
			SerializedProperty
			point = points.GetArrayElementAtIndex (i);

			EditorGUILayout.BeginHorizontal ();

			GUILayout.Label ("position");
//			EditorGUILayout.PropertyField (point.FindPropertyRelative ("time"), pointContent, buttonWidth);
			EditorGUILayout.PropertyField (point.FindPropertyRelative ("local"), pointContent, colorWidth);
			
			if (GUILayout.Button (teleportContent, EditorStyles.miniButtonLeft, buttonWidth)) {
				if (teleportingElement >= 0) {
					points.MoveArrayElement (teleportingElement, i);
					teleportingElement = -1;
					teleportContent.tooltip = "start teleporting this point";
				} else {
					teleportingElement = i;
					teleportContent.tooltip = "teleport here";
				}
			}
			if (GUILayout.Button (insertContent, EditorStyles.miniButtonMid, buttonWidth)) {
				points.InsertArrayElementAtIndex (i);
			}
			if (GUILayout.Button (deleteContent, EditorStyles.miniButtonRight, buttonWidth)) {
				points.DeleteArrayElementAtIndex (i);
			}
			
			EditorGUILayout.EndHorizontal ();
		}

		if (GUILayout.Button (insertContent, EditorStyles.miniButtonMid, buttonWidth)) {
			SkyMoveByCurve mObject = (SkyMoveByCurve)target;
			mObject.Play();
		}
		if (
			mEditor.ApplyModifiedProperties () ||
			(Event.current.type == EventType.ValidateCommand &&
			Event.current.commandName == "UndoRedoPerformed")
			) {
			foreach (SkyMoveByCurve s in targets) {
				if (PrefabUtility.GetPrefabType (s) != PrefabType.Prefab) {
					s.myUpdate ();
				}
			}
		}
	}
	
	void OnSceneGUI ()
	{
		SkyMoveByCurve mObject = (SkyMoveByCurve)target;
		Transform starTransform = mObject.transform.parent.transform;
		Quaternion rotation = Quaternion.Euler (0f, 0f, 0f);
		mObject.getNewSize ();
		for (int i = 1; i < mObject.targets.Length; i++) {
			Vector3 temp = SkyUtil.reletiveToLocal (mObject.targets [i].local, mObject.parentWidth, mObject.parentHight);
		
			Vector3 oldPoint = starTransform.TransformPoint (rotation * temp);
			Vector3 newPoint = Handles.FreeMoveHandle
			(oldPoint, Quaternion.identity, 0.1f, pointSnap, Handles.DotCap);
			if (oldPoint != newPoint) {
				mObject.isDirty = true;
				mObject.targets [i].local = Quaternion.Inverse (rotation) *
					starTransform.InverseTransformPoint (newPoint);
				mObject.targets [i].local.x = mObject.targets [i].local.x / mObject.parentWidth + 0.5f;
				mObject.targets [i].local.y = mObject.targets [i].local.y / mObject.parentHight + 0.5f;
				mObject.myUpdate ();
			}
		}
	}
}
