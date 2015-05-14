using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SkyMoveByCurve : MonoBehaviour {

	// Use this for initialization
    private float parentHight = 1;
    private float parentWidth = 1;
    public Vector3 startPosition;
    public Vector3 startPosition2;
    public float totalTime;
    public List<float> times;
    public List<Vector3> targets;
	private SkyAniCallBack  skyAniComplete ;


	void Start () {

        RectTransform parentTransform = transform.parent.transform as RectTransform;
        parentHight = parentTransform.rect.height;
        parentWidth = parentTransform.rect.width;
		transform.localPosition = SkyUtil.reletiveToLocal (startPosition, parentWidth, parentHight);
        for(int i=0;i<targets.Count;i++){
			targets[i] = SkyUtil.reletiveToLocal(targets[i],parentWidth,parentHight);
        }
        float sum = 0;
        for(int i=0;i<targets.Count;i++){
            sum += times[i];
        }
        for(int i=0;i<targets.Count;i++){
            times[i] *= totalTime;
            times[i] /= sum;
        }
		skyAniComplete = new SkyAniCallBack ();
		skyAniComplete.SetCompleteMethod (()=>{
			transform.localPosition = SkyUtil.reletiveToLocal (startPosition, parentWidth, parentHight);
            play();});
        play();
	}
	
    public void play(){
        SkyAnimator.moveToSequence (gameObject, times, targets, true, SkyAniDuration.Linear, skyAniComplete);
    }

	// Update is called once per frame
	void Update () {
//        transform.localPosition = SkyFlash.reletiveToLocal (startPosition, parentWidth, parentHight);
	}

}
