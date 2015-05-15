using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkyFlash : MonoBehaviour {

	// Use this for initialization
    private float parentHight = 1;
    private float parentWidth = 1;
    public Vector3 scaleMin;
    public Vector3 scaleMax;
    public Color colorMin;
    public Color colorMax;
    public float FirstAnimationTime;
    public float DelayTime;
    public float AutoStartDelay;
    public SkyAniDuration PositionSkyAniDuration = SkyAniDuration.Linear;

    public Vector3 positionMin;
    public Vector3 positionMid;
    public Vector3 positionMax;
    SkyAniCallBack scalFirstComplete;
    SkyAniCallBack FirstAnimationComplete;
    SkyAniCallBack colorFirstComplete;
    SkyAniCallBack positionFirstComplete;
    SkyAniCallBack delayTimeComplete;

    public bool loop = true;
    public bool AutoRun = true;
    private Image mImage;

	void Start () {
        Init ();
//        gameObject.SetActive (false);
        if (AutoRun) {
            StartCoroutine(delayTimeAction(AutoStartDelay,FirstAnimation));
        }
	}

    void Init(){
        RectTransform parentTransform = transform.parent.transform as RectTransform;
        parentHight = parentTransform.rect.height;
        parentWidth = parentTransform.rect.width;
        mImage = GetComponent<Image> ();
        mImage.color = colorMin;
        scalFirstComplete = new SkyAniCallBack ();
        scalFirstComplete.SetCompleteMethod (()=>{SkyAnimator.scaleTo (gameObject, FirstAnimationTime/2f, scaleMin, SkyAniDuration.Linear, null);});
        positionFirstComplete = new SkyAniCallBack ();
        positionFirstComplete.SetCompleteMethod (()=>{
            SkyAnimator.moveTo (gameObject, FirstAnimationTime/2f, SkyUtil.reletiveToLocal(positionMax,parentWidth,parentHight), true, PositionSkyAniDuration, FirstAnimationComplete);});
        FirstAnimationComplete = new SkyAniCallBack ();
        FirstAnimationComplete.SetCompleteMethod (()=>{
            gameObject.SetActive (false);
            if (loop) {
                DelayAnimation();;
            }
        });
        delayTimeComplete = new SkyAniCallBack ();
        delayTimeComplete.SetCompleteMethod (()=>{FirstAnimation();});
        colorFirstComplete = new SkyAniCallBack ();
        colorFirstComplete.SetCompleteMethod (()=>{ SkyAnimator.colorTo (mImage, FirstAnimationTime/2f, colorMin, SkyAniDuration.Linear, null);});
    }

    void FirstAnimation(){
        gameObject.SetActive (true);
        transform.localScale = scaleMin;
        transform.localPosition = SkyUtil.reletiveToLocal (positionMin, parentWidth, parentHight);
        SkyAnimator.moveTo (gameObject, FirstAnimationTime/2f, SkyUtil.reletiveToLocal(positionMid,parentWidth,parentHight), true, PositionSkyAniDuration, positionFirstComplete);
        SkyAnimator.scaleTo (gameObject, FirstAnimationTime/2f, scaleMax, SkyAniDuration.Linear, scalFirstComplete);
        SkyAnimator.colorTo (mImage, FirstAnimationTime/2f, colorMax, SkyAniDuration.Linear, colorFirstComplete);
    }

    void DelayAnimation(){
        if (loop) {
            SkyAnimator.colorTo (mImage, DelayTime, colorMin, SkyAniDuration.Linear, delayTimeComplete);
        } else {
            gameObject.SetActive (false);
        }
    }
	
    IEnumerator delayTimeAction (float delayTime,System.Action a)
    {
        yield return new WaitForSeconds (delayTime);
        a ();
    }

	// Update is called once per frame
	void Update () {
	
	}

}
