using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class SkyMoveByCurve : MonoBehaviour
{

    // Use this for initialization
    private float parentHight = 1;
    private float parentWidth = 1;
    public Vector3 startPosition;
    public Vector3 startPosition2;
    public bool isLoop = true;
    public bool AutoRun = true;
    public float AutoStartDelay;
    public float delayTime;
    public float totalTime;
    public List<float> times;
    public List<Vector3> targets;
    private SkyAniCallBack  skyAniComplete ;
    private SkyAniCallBack  delayComplete ;
    private Sequence mSequence;

    void Start ()
    {

        init ();

//        gameObject.SetActive (false);
        if (AutoRun) {
            StartCoroutine (delayTimeAction (AutoStartDelay, play));
        }
    }
    
    public void init ()
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
            times [i] *= totalTime;
            times [i] /= sum;
        }

        skyAniComplete = new SkyAniCallBack ();
        skyAniComplete.SetCompleteMethod (() => {
            gameObject.SetActive (false);
            if (isLoop) {
                DelayAction ();
            }
            ;});
        
        delayComplete = new SkyAniCallBack ();
        delayComplete.SetCompleteMethod (() => {
            play ();});
    }

    public void play ()
    {
        gameObject.SetActive (true);
        transform.localScale = Vector3.one;
        transform.localPosition = SkyUtil.reletiveToLocal (startPosition, parentWidth, parentHight);
        mSequence = SkyAnimator.moveToSequence (gameObject, times, targets, true, SkyAniDuration.Linear, skyAniComplete);
    }

    public void DelayAction ()
    {
        SkyAnimator.scaleTo (gameObject, delayTime, Vector3.zero, SkyAniDuration.Linear, delayComplete);
    }

    IEnumerator delayTimeAction (float delayTime, System.Action a)
    {
        yield return new WaitForSeconds (delayTime);
        a ();
    }
    // Update is called once per frame
    void Update ()
    {
    }

    void OnDestroy() {

        if (mSequence != null) {
            mSequence.Kill ();
        }
    }
}
