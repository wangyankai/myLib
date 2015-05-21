using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System;

[ExecuteInEditMode]
public class SkyMoveByCurve : SkyBaseAnimation
{

	[Serializable]
	public class Point
	{
		public float time;
		public Vector3 local;

		public Point ()
		{
			time = 1;
			local = new Vector3 (0.5f, 0.5f, 0);
		}

	}

	public float parentHight = 1;
	public float parentWidth = 1;
	public Vector3 startPosition;
	public Point[] targets;
	private Sequence mSequence;
	private List<float> times = new List<float> ();
	private List<Vector3> positions = new List<Vector3> ();
   
	public override void Init ()
	{
		getNewSize ();
		transform.localPosition = SkyUtil.reletiveToLocal (startPosition, parentWidth, parentHight);
		times.Clear ();
		positions.Clear ();
		for (int i=0; i<targets.Length; i++) {
			positions.Add (SkyUtil.reletiveToLocal (targets [i].local, parentWidth, parentHight));
			if (i == 0) {
				times.Add (Vector3.Distance (transform.localPosition, positions [i]));
			} else {
				times.Add (Vector3.Distance (positions [i - 1], positions [i]));
			}
		}
		float sum = 0;
		for (int i=0; i<times.Count; i++) {
			sum += times [i];
		}

		for (int i=0; i<times.Count; i++) {
			times [i] *= PlayTime;
			times [i] /= sum;
		}
	}

	public override void Play ()
	{
		gameObject.SetActive (true);
		transform.localScale = Vector3.one;
		transform.localPosition = SkyUtil.reletiveToLocal (startPosition, parentWidth, parentHight);
		mSequence = SkyAnimator.moveToSequence (gameObject, times, positions, true, SkyAniDuration.Linear, playComplete);
	}

	public override void DelayAction ()
	{  
		SkyAnimator.scaleTo (gameObject, DelayTime, Vector3.zero, SkyAniDuration.Linear, delayComplete);
	}

	public void getNewSize ()
	{
		RectTransform parentTransform = transform.parent.transform as RectTransform;
		parentHight = parentTransform.rect.height;
		parentWidth = parentTransform.rect.width;
	}
    
	void OnDestroy ()
	{
		if (mSequence != null) {
			mSequence.Kill ();
		}
	}

	public void myUpdate ()
	{
		if (targets == null || targets.Length == 0) {
			targets = new Point[]{ new Point ()};
		}
	}

	void OnEnable ()
	{
		startPosition = Vector3.zero;
		myUpdate ();
	}

	void Reset ()
	{
		startPosition = Vector3.zero;
		myUpdate ();
	}
}
