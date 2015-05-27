using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class SkyBezierCurve
{
	public AnimationCurve animX;
	public AnimationCurve animY;
	public Vector3 startPoint;
	public Vector3 endPoint;
	public float timeDuration;
	public int keyFrame = 60;
//	public bool isActive = false;

	[SerializeField] public List<Vector3> middlePoints ;
	List<float> tPara = new List<float> ();
	List<float> ftPara = new List<float> ();
	List<int> para = new List<int> ();

	public SkyBezierCurve(){
	}

	private float genBezier (float t, float pStart, float pEnd, float pIn)
	{
		float ft = 1 - t;
		return (ft * ft) * pStart + 2 * t * ft * pIn + (t * t) * pEnd;
	}

	private void genPara ()
	{
		para.Clear ();
		int lenth = middlePoints.Count + 1;
		para.Add (1);
		for (int i=1; i<=lenth; i++) {
			int pre = para [i - 1];
			para.Add (pre * (lenth + 1 - i) / i);
		}
	}

	private void genParaT (float t)
	{
		float ft = 1 - t;
		int lenth = middlePoints.Count + 1;
		tPara.Clear ();
		ftPara.Clear ();
		float tempT = 1f;
		float tempFt = 1f;
		for (int i=0; i<=lenth; i++) {
			float elementT = tempT;
			float elementFt = tempFt;
			tPara.Add (elementT);
			ftPara.Add (elementFt);
			tempT *= t;
			tempFt *= ft;
		}
	}

	private Keyframe[] genFrame (int lenth, float pStart, float pEnd, float pIn)
	{
		Keyframe[] ks = new Keyframe[lenth + 1];
		float step = 1 / (lenth * 1f);
		for (int i=0; i<ks.Length; i++) {
			ks [i] = new Keyframe (step * i, genBezier (step * i, pStart, pEnd, pIn));
			ks [i].inTangent = 0;
		}
		return ks;
	}

	private void genFrame (Keyframe[] ks_x, Keyframe[] ks_y)
	{
		genPara ();
		float step = 1 / ((ks_x.Length - 1) * 1f);
		for (int i=0; i<ks_x.Length; i++) {
			genParaT (step * i);
			ks_x [i] = new Keyframe (step * i, genBezierX ());
			ks_x [i].inTangent = 0;
			ks_y [i] = new Keyframe (step * i, genBezierY ());
			ks_y [i].inTangent = 0;
		}
	}

	private float genBezierX ()
	{
		int lenth = middlePoints.Count + 2;
		float temp = para [0] * ftPara [lenth - 1] * tPara [0] * startPoint.x + para [lenth - 1] * ftPara [0] * tPara [lenth - 1] * endPoint.x;
		for (int i=1; i<=lenth-2; i++) {
			temp += para [i] * ftPara [lenth - i-1] * tPara [i] * middlePoints [i - 1].x;
		}
		return temp;
	}

	private float genBezierY ()
	{
		int lenth = middlePoints.Count + 2;
		float temp = para [0] * ftPara [lenth - 1] * tPara [0] * startPoint.y + para [lenth - 1] * ftPara [0] * tPara [lenth - 1] * endPoint.y;
		for (int i=1; i<=lenth-2; i++) {
			temp += para [i] * ftPara [lenth - i-1] * tPara [i] * middlePoints [i - 1].y;
		}
		return temp;
	}

	public void CreateCurve ()
	{
		int totalKeyFrame = (int)timeDuration * keyFrame;
		Keyframe[] ks_x = new Keyframe[totalKeyFrame + 1];
		Keyframe[] ks_y = new Keyframe[totalKeyFrame + 1];
		genFrame (ks_x, ks_y);
		animX = new AnimationCurve (ks_x);
		animY = new AnimationCurve (ks_y);
	}

	public void Init(){
		if (middlePoints == null || middlePoints.Count == 0) {
			middlePoints = new List<Vector3>();
			middlePoints.Add(Vector3.zero);
		}
	}

//	public static int genN (int a, int b)
//	{
//		if (a < b || a <= 0 || b < 0)
//			return 0;
//		if (b == 0) {
//			return 1;
//		}
//
//		if (b > (a / 2 + 1)) {
//			return genN (a, a - b);
//		}
//
//		int tempA = 1;
//		int tempB = 1;
//		for (int i=1; i<=b; i++) {
//			tempA *= (a + 1 - i);
//			tempB *= i;
//		}
//		return tempA / tempB;
//	}
}

