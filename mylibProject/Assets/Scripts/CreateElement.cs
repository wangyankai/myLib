﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateElement : MonoBehaviour {

	public SkyBezierCurveOject skyBezierObject;
	public GameObject targarPosition;
	public GameObject startPosition;
	List<SkyBezierCurveOject> elements = new List<SkyBezierCurveOject>();
	List<SkyAction> animation = new List<SkyAction>();
	SkyAniSequence animationSquence = new SkyAniSequence ();
	public int count = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void create(){
	
		elements.Clear ();
		for (int i=0; i<count; i++) {
			SkyBezierCurveOject element = Instantiate (skyBezierObject) as SkyBezierCurveOject;
//			element.Init();
			element.skyBezierCurve.endPoint = targarPosition.transform.localPosition;
			element.transform.SetParent(transform,false);
			element.transform.localPosition = startPosition.transform.localPosition;
			element.transform.localScale = Vector3.zero;
			element.skyBezierCurve.startPoint = element.transform.localPosition;
			Vector3 temp1 = Vector3.zero;
			temp1.x = genOffset() + element.transform.localPosition.x;
			temp1.y = genOffset();
			Vector3 temp = Vector3.zero;
			temp.x = genOffset() - element.transform.localPosition.x;
			temp.y = genOffset();

			element.skyBezierCurve.middlePoints.Clear();
			element.skyBezierCurve.middlePoints.Add(temp1);
			element.skyBezierCurve.middlePoints.Add(temp);
			element.skyBezierCurve.timeDuration =  Random.Range(1,2);
			element.DelayTime = i*1f/count;
			element.skyBezierCurve.CreateCurve();
			element.playAction.AddCompleteMethod(()=>{
				element.RemoveFromSeqence();
				Destroy(element.gameObject);});
//			elements.Add(element);
//			element.PlayWithDelay();
//			animation.Add(element);
			animationSquence.AppendAction(element);
		}

		SkyAniSequence tempSeq = new SkyAniSequence();
		SkyDelayAnimation skyDelay0 = new SkyDelayAnimation ();
		skyDelay0.playAction.AddCompleteMethod (Test2);
		tempSeq.AppendAction (skyDelay0);
		SkyDelayAnimation skyDelay1 = new SkyDelayAnimation ();
		skyDelay0.playAction.AddCompleteMethod (Test3);
		tempSeq.AppendAction (skyDelay1);

		animationSquence.AppendAction(tempSeq);

		SkyDelayAnimation skyDelay = new SkyDelayAnimation ();
		skyDelay.playAction.AddCompleteMethod (Test);
//		animation.Add(skyDelay);
//		skyDelay.Play ();
		animationSquence.AppendAction(skyDelay);
//		foreach (SkyAnimation skyAnimation in animation) {
//			skyAnimation.Play();
//		}
		animationSquence.Play ();
	}


	public void Test(){
		Debug.Log ("Delay .......");
	}

	public void Test2(){
		Debug.Log ("Delay ....... sss");
	}

	public void Test3(){
		Debug.Log ("Delay ....... 44444");
	}


	private float genOffset(){
		return Random.Range(-100,100);
	}
}
