using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkyBezierCurveOject : MonoBehaviour
{
	public SkyBezierCurve skyBezierCurve;

	void Start ()
	{

	}

	void Update ()
	{
	
	}
	
	public void startAnimation (float time)
	{
		skyBezierCurve.startPoint = transform.localPosition;
		skyBezierCurve.CreateCurve ();
		StartCoroutine (Tweening (time));
	}

	IEnumerator Tweening (float time)
	{
		yield return new WaitForSeconds (time);
		float t = Time.time;
		while (Time.time - t < skyBezierCurve.timeDuration) {
			yield return 0;
			transform.localScale = new Vector3 (((Time.time - t) / skyBezierCurve.timeDuration / 2), ((Time.time - t) / skyBezierCurve.timeDuration) / 2, 0);
			transform.localPosition = new Vector3 (skyBezierCurve.animX.Evaluate ((Time.time - t) / skyBezierCurve.timeDuration), skyBezierCurve.animY.Evaluate ((Time.time - t) / skyBezierCurve.timeDuration), 0);
		}
		Destroy (gameObject);
	}
	
}

