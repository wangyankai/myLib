using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEditor;

public class SkyRestParticleSystemShape : MonoBehaviour {

	// Use this for initialization
	void Start () {
		RectTransform parentTransform = transform.parent.transform as RectTransform;
		SerializedObject so = new SerializedObject(GetComponent<ParticleSystem>());
		so.FindProperty("ShapeModule.boxX").floatValue = parentTransform.rect.width/4f*0.8f;
		so.FindProperty("ShapeModule.boxY").floatValue = parentTransform.rect.height/4f*0.6f;
		so.ApplyModifiedProperties();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
