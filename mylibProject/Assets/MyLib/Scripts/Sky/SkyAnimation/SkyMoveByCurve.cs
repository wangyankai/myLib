using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System;

[ExecuteInEditMode]
public class SkyMoveByCurve : SkyBaseAnimationObject
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
	public Point[] targets;
	private Sequence mSequence;
	private List<float> times = new List<float> ();
	private List<Vector3> positions = new List<Vector3> ();
	private Transform m_Transform;
	public Color m_Color = Color.green; // 线框颜色

	public bool IsDirty = true;
	
	public override void Init ()
	{
		base.Init ();
		IsDirty = true;
		PlayCallBack.AddStepCompleteMethod (() => {
			transform.localScale = Vector3.zero;
		});
	}

	public void computePath ()
	{
		if (IsDirty) {
			getNewSize ();
			times.Clear ();
			positions.Clear ();
			for (int i=0; i<targets.Length; i++) {
				positions.Add (SkyUtil.reletiveToLocal (targets [i].local, parentWidth, parentHight));
				if (i == 0)
					times.Add (0);
				else
					times.Add (Vector3.Distance (positions [i - 1], positions [i]));
			}
			float sum = 0;
			for (int i=0; i<times.Count; i++) {
				sum += times [i];
			}
            
			if (sum != 0) {
				for (int i=0; i<times.Count; i++) {
					times [i] *= PlayTime;
					times [i] /= sum;
				}
			}
			IsDirty = false;
		}
	}

	public override void PlayLoop ()
	{
		base.PlayLoop ();
		gameObject.SetActive (true);
		transform.localScale = Vector3.one;
		transform.localPosition = SkyUtil.reletiveToLocal (targets [0].local, parentWidth, parentHight);
		computePath ();
		mSequence = SkyAnimator.moveToSequence (gameObject, times, positions, true, SkyAniDuration.Linear, PlayCallBack);
	}

	public override void Delay ()
	{  
		base.Delay ();
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
			IsDirty = true;
		}
	}

	void OnEnable ()
	{
		myUpdate ();
		computePath ();
	}

	void Reset ()
	{
		myUpdate ();
		computePath ();
	}

	void OnDrawGizmos ()
	{
		m_Transform = transform.parent.transform;
		if (m_Transform == null)
			return;

		if ((!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode) && positions.Count > 0 && (!transform.localPosition.Equals (positions [0]))) {
			if (IsDirty) {
				transform.localPosition = positions [0];
			} else {
				positions [0] = transform.localPosition;
				targets [0].local.x = positions [0].x / parentWidth + 0.5f;
				targets [0].local.y = positions [0].y / parentHight + 0.5f;
				IsDirty = true;
			}
		}


		computePath ();
		// 设置矩阵
		Matrix4x4 defaultMatrix = Gizmos.matrix;
		Gizmos.matrix = m_Transform.localToWorldMatrix;
		
		// 设置颜色
		Color defaultColor = Gizmos.color;
		Gizmos.color = m_Color;	
	

		for (int i=1; i<positions.Count; i++) {
			Gizmos.DrawLine (positions [i - 1], positions [i]);
		}
        
		// 恢复默认颜色
		Gizmos.color = defaultColor;
		
		// 恢复默认矩阵
		Gizmos.matrix = defaultMatrix;
	}
}
