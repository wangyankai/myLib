using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects, CustomEditor(typeof(SkyBezierCurveOject))]
public class SkyBezierEditor : Editor
{

	private static Vector3 pointSnap = Vector3.one * 0.1f;
	private SerializedObject mEditor;
	private SerializedProperty loop, AutoRun, PlayTime, DelayTime, AutoStartDelayTime,skyBezierCurve, middlePoints, fixedPointColor, curveColor;
	private int teleportingElement;
	private static GUIContent
		playContent = new GUIContent ("PLAY", "duplicate this point"),
		insertContent = new GUIContent ("+", "duplicate this point"),
		deleteContent = new GUIContent ("-", "delete this point"),
		pointContent = GUIContent.none,
		teleportContent = new GUIContent ("T");
	
	void OnEnable ()
	{
		mEditor = new SerializedObject (target);
		loop = mEditor.FindProperty ("_loop");
		AutoRun = mEditor.FindProperty ("_AutoRun");
		PlayTime = mEditor.FindProperty ("_PlayTime");
		DelayTime = mEditor.FindProperty ("_DelayTime");
		AutoStartDelayTime = mEditor.FindProperty ("_AutoStartDelayTime");
		skyBezierCurve = mEditor.FindProperty ("skyBezierCurve");
		middlePoints = skyBezierCurve.FindPropertyRelative ("middlePoints");
		fixedPointColor = mEditor.FindProperty ("fixedPointColor");
		curveColor = mEditor.FindProperty ("curveColor");
		teleportingElement = -1;
		teleportContent.tooltip = "start teleporting this point";
	}
	
	private static GUILayoutOption
		buttonWidth = GUILayout.MaxWidth (30f),
		colorWidth = GUILayout.MaxWidth (200f);
	
	public override void OnInspectorGUI ()
	{
		mEditor.Update ();
	
		EditorGUILayout.PropertyField (loop);
		EditorGUILayout.PropertyField (AutoRun);
		EditorGUILayout.PropertyField (PlayTime);
		EditorGUILayout.PropertyField (DelayTime);
		EditorGUILayout.PropertyField (AutoStartDelayTime);

		EditorGUILayout.PropertyField (fixedPointColor);
		EditorGUILayout.PropertyField (curveColor);
		EditorGUILayout.PropertyField (skyBezierCurve.FindPropertyRelative ("animX"));
		EditorGUILayout.PropertyField (skyBezierCurve.FindPropertyRelative ("animY"));
		EditorGUILayout.PropertyField (skyBezierCurve.FindPropertyRelative ("startPoint"));
		EditorGUILayout.PropertyField (skyBezierCurve.FindPropertyRelative ("endPoint"));
		EditorGUILayout.PropertyField (skyBezierCurve.FindPropertyRelative ("timeDuration"));
		EditorGUILayout.PropertyField (skyBezierCurve.FindPropertyRelative ("keyFrame"));
		EditorGUILayout.PropertyField (skyBezierCurve.FindPropertyRelative ("middlePoints"));

	
		for (int i = 0; i < middlePoints.arraySize; i++) {
			SerializedProperty
			point = middlePoints.GetArrayElementAtIndex (i);
			
			EditorGUILayout.BeginHorizontal ();

			GUILayout.Label ("position");
			EditorGUILayout.PropertyField (point, pointContent, colorWidth);
			
			if (GUILayout.Button (teleportContent, EditorStyles.miniButtonLeft, buttonWidth)) {
				if (teleportingElement >= 0) {
					middlePoints.MoveArrayElement (teleportingElement, i);
					teleportingElement = -1;
					teleportContent.tooltip = "start teleporting this point";
				} else {
					teleportingElement = i;
					teleportContent.tooltip = "teleport here";
				}
			}
			if (GUILayout.Button (insertContent, EditorStyles.miniButtonMid, buttonWidth)) {
				middlePoints.InsertArrayElementAtIndex (i);
				((SkyBezierCurveOject)target).isDirty = true;
			}
			if (GUILayout.Button (deleteContent, EditorStyles.miniButtonRight, buttonWidth)) {
				middlePoints.DeleteArrayElementAtIndex (i);
				((SkyBezierCurveOject)target).isDirty = true;
			}
			
			EditorGUILayout.EndHorizontal ();
		}

		if (GUILayout.Button (playContent, EditorStyles.miniButtonMid, colorWidth)) {
			((SkyBezierCurveOject)target).PlayLoop();
		}
		
		if (
			mEditor.ApplyModifiedProperties () ||
			(Event.current.type == EventType.ValidateCommand &&
			Event.current.commandName == "UndoRedoPerformed")
			) {
			foreach (SkyBezierCurveOject s in targets) {
				if (PrefabUtility.GetPrefabType (s) != PrefabType.Prefab) {
					s.myUpdate ();
				}
			}
		}
	}
	
	void OnSceneGUI ()
	{
		SkyBezierCurveOject mObject = (SkyBezierCurveOject)target;
		Transform starTransform = mObject.transform.parent.transform;
		Quaternion rotation = Quaternion.Euler (0f, 0f, 0f);
		for (int i = 0; i < mObject.skyBezierCurve.middlePoints.Count; i++) {
			Vector3 temp = mObject.skyBezierCurve.middlePoints [i];
			mObject.skyBezierCurve.middlePoints [i] = checkPoint (ref temp, starTransform, mObject, rotation);
		}
		checkPoint (ref mObject.skyBezierCurve.startPoint, starTransform, mObject, rotation);
		checkPoint (ref mObject.skyBezierCurve.endPoint, starTransform, mObject, rotation);
	}

	private Vector3 checkPoint (ref Vector3 point, Transform starTransform, SkyBezierCurveOject mObject, Quaternion rotation)
	{
		Vector3 temp = point;
		
		Vector3 oldPoint = starTransform.TransformPoint (rotation * temp);
		Vector3 newPoint = Handles.FreeMoveHandle
			(oldPoint, Quaternion.identity, 0.1f, pointSnap, Handles.DotCap);
		if (oldPoint != newPoint) {
			point = Quaternion.Inverse (rotation) *
				starTransform.InverseTransformPoint (newPoint);
			mObject.myUpdate ();
			mObject.isDirty = true;
		}
		return point;
	}
}
