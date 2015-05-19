using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkyBezierCurveOject : MonoBehaviour
{
    public SkyBezierCurve skyBezierCurve;
    public delegate void MCallBack ();
   
    public MCallBack MOnAnimationStart;
    public MCallBack MOnAnimationCompleted;

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

    public virtual void UpdateAnimation (float time)
    {
        transform.localScale = new Vector3 (((1 - time / skyBezierCurve.timeDuration) * 0.3f + 0.7f), ((1 - time / skyBezierCurve.timeDuration)) * 0.3f + 0.7f, 0);
        transform.localPosition = new Vector3 (skyBezierCurve.animX.Evaluate (time / skyBezierCurve.timeDuration), skyBezierCurve.animY.Evaluate (time / skyBezierCurve.timeDuration), 0);
    }

    IEnumerator Tweening (float time)
    {
        yield return new WaitForSeconds (time);
        if (MOnAnimationStart != null)
            MOnAnimationStart ();
        float t = Time.time;
        while (Time.time - t < skyBezierCurve.timeDuration) {
            yield return 0;
            UpdateAnimation (Time.time - t);
        }
        if (MOnAnimationCompleted != null)
            MOnAnimationCompleted ();
    }
    
}

