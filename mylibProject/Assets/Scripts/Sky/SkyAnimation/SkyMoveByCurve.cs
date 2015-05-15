using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class SkyMoveByCurve : SkyBaseAnimation
{
    private float parentHight = 1;
    private float parentWidth = 1;
    public Vector3 startPosition;
    public Vector3 startPosition2;
    public List<float> times;
    public List<Vector3> targets;
    private Sequence mSequence;

   
	public override void Init ()
    {
        RectTransform parentTransform = transform.parent.transform as RectTransform;
        parentHight = parentTransform.rect.height;
        parentWidth = parentTransform.rect.width;
        transform.localPosition = SkyUtil.reletiveToLocal (startPosition, parentWidth, parentHight);
        for (int i=0; i<targets.Count; i++) {
            targets [i] = SkyUtil.reletiveToLocal (targets [i], parentWidth, parentHight);
        }
        float sum = 0;
        for (int i=0; i<targets.Count; i++) {
            sum += times [i];
        }
        for (int i=0; i<targets.Count; i++) {
            times [i] *= PlayTime;
            times [i] /= sum;
        }
    }

	public override void Play ()
    {
        gameObject.SetActive (true);
        transform.localScale = Vector3.one;
        transform.localPosition = SkyUtil.reletiveToLocal (startPosition, parentWidth, parentHight);
		mSequence = SkyAnimator.moveToSequence (gameObject, times, targets, true, SkyAniDuration.Linear, playComplete);
    }

	public override void DelayAction ()
    {  
        SkyAnimator.scaleTo (gameObject, DelayTime, Vector3.zero, SkyAniDuration.Linear, delayComplete);
    }
	

    void OnDestroy() {
        if (mSequence != null) {
            mSequence.Kill ();
        }
    }
}
