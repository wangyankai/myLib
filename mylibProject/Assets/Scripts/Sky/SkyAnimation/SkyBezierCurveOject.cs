using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class SkyBezierCurveOject : SkyBaseAnimation
{
	public SkyBezierCurve skyBezierCurve;
//	public delegate void MCallBack ();
   
	public SkyAniCallBack ActionCallBack;
	private Transform m_Transform;
	public Color fixedPointColor = Color.green; // 线框颜色
	public Color curveColor = Color.red;
	public bool isDirty = true;

	public override void Init ()
	{
//		isDirty = true;
//		computePath ();
		skyBezierCurve.Init ();
	}
    
	public override void Play ()
	{
		computePath ();
	    StartCoroutine (Tweening (AutoStartDelayTime));
	}

	public override void DelayAction ()
	{  
		if (DelayTime > 0) {
			StartCoroutine (delayTimeAction (DelayTime,()=>{ delayComplete.OnCompleteMethod();}));
		} else {
			delayComplete.OnCompleteMethod();
		}
	}

	public void computePath ()
	{
		if (isDirty) {
			skyBezierCurve.CreateCurve ();
			isDirty = false;
		}
	}

	public virtual void UpdateAnimation (float time)
	{
		transform.localScale = new Vector3 (((1 - time / skyBezierCurve.timeDuration) * 0.3f + 0.7f), ((1 - time / skyBezierCurve.timeDuration)) * 0.3f + 0.7f, 0);
		transform.localPosition = new Vector3 (skyBezierCurve.animX.Evaluate (time / skyBezierCurve.timeDuration), skyBezierCurve.animY.Evaluate (time / skyBezierCurve.timeDuration), 0);
	}


	IEnumerator Tweening (float time)
	{
		yield return new WaitForSeconds (time);
		if (ActionCallBack != null && ActionCallBack.OnStartMethod!=null)
			ActionCallBack.OnStartMethod ();
		float t = Time.time;
		while (Time.time - t < skyBezierCurve.timeDuration) {
			yield return 0;
			UpdateAnimation (Time.time - t);
		}
		if (ActionCallBack != null && ActionCallBack.OnCompleteMethod!=null)
			ActionCallBack.OnCompleteMethod ();
		if (playComplete != null) {
			playComplete.OnCompleteMethod ();
		}
	}

	public void myUpdate ()
	{
		skyBezierCurve.Init ();
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
		

		if ((!transform.localPosition.Equals (skyBezierCurve.startPoint))&&(!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)) {
			if (isDirty) {
				transform.localPosition = skyBezierCurve.startPoint;
			} else {
				skyBezierCurve.startPoint = transform.localPosition;
				isDirty = true;
			}
		}	
		computePath ();
		// 设置矩阵
		Matrix4x4 defaultMatrix = Gizmos.matrix;
		Gizmos.matrix = m_Transform.localToWorldMatrix;
		
		// 设置颜色
		Color defaultColor = Gizmos.color;
		Gizmos.color = fixedPointColor;	
		
		
		for (int i=0; i<skyBezierCurve.middlePoints.Count; i++) {
			if (i == 0) {
				Gizmos.DrawLine (skyBezierCurve.startPoint, skyBezierCurve.middlePoints [i]);
			} else {
				Gizmos.DrawLine (skyBezierCurve.middlePoints [i - 1], skyBezierCurve.middlePoints [i]);
			}
		}
		Gizmos.DrawLine (skyBezierCurve.middlePoints [skyBezierCurve.middlePoints.Count - 1], skyBezierCurve.endPoint);

		Gizmos.color = curveColor; 

		float step = 1f / skyBezierCurve.keyFrame;
		float time = 1f / skyBezierCurve.keyFrame;
		Vector3 start = new Vector3 (skyBezierCurve.startPoint.x, skyBezierCurve.startPoint.y, 0);
		Vector3 end = new Vector3 (skyBezierCurve.animX.Evaluate (time / skyBezierCurve.timeDuration), skyBezierCurve.animY.Evaluate (time / skyBezierCurve.timeDuration), 0);
		while (time < skyBezierCurve.timeDuration) {
			Gizmos.DrawLine (start, end);
			time += step;
			start.x = end.x;
			start.y = end.y;
			end.x = skyBezierCurve.animX.Evaluate (time / skyBezierCurve.timeDuration);
			end.y = skyBezierCurve.animY.Evaluate (time / skyBezierCurve.timeDuration);
		}
		Gizmos.DrawLine (start, end);
		// 恢复默认颜色
		Gizmos.color = defaultColor;
		
		// 恢复默认矩阵
		Gizmos.matrix = defaultMatrix;
	}

//	public void getNewSize ()
//	{
//		RectTransform parentTransform = transform.parent.transform as RectTransform;
//		parentHight = parentTransform.rect.height;
//		parentWidth = parentTransform.rect.width;
//	}
}

