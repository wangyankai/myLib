using UnityEngine;
using System.Collections;
using UI.UIComponent.ScrollList;

public class MyScrollPanel : SkyScrollPanel
{

    public float pauseTime = 1f;
    public float vectory = 0.15f;

    protected override void myUpdate ()
    {
        base.myUpdate ();
        moveLeftToRight ();
    }

    private void moveLeftToRight ()
    {
        if (AutoScroll && GetElementCount () > 1) {
            myscrollBar.value -= Time.deltaTime * vectory * standCount / GetElementCount ();
            if (myscrollBar.value <= index * 1f / (GetElementCount () - 1)) {
                AutoScroll = false;
                float nextValue = index * 1f / (GetElementCount () - 1);
                myscrollBar.value = nextValue;
                float delayTime_f = pauseTime;
                index --;
                bool reset = false;
                if (myscrollBar.value <= 0) {
                    index = (GetElementCount () - 1)/2;
                    nextValue = index * 1f / (GetElementCount () - 1);
                    reset = true;
//                    delayTime_f = 0;
                }
                StartCoroutine (delayTime (reset, delayTime_f));
            }
        }
    }


    protected override void onBeginDrag ()
    {
        base.onBeginDrag ();
    }
    
    protected override void onEndDrag ()
    {
        index = (int)(myscrollBar.value * (GetElementCount () - 1));
        base.onEndDrag ();
    }
    
    protected override void initScrollSize ()
    {
        base.initScrollSize ();
    }

    public override void SetPosition ()
    {
        if (GetElementCount () > 1) {
            index = (GetElementCount () - 1) / 2;
            myscrollBar.value = index * 1f / (GetElementCount () - 1);
        } else {
            index = 0;
            myscrollBar.value = 0;
        }

    }
    
    IEnumerator delayTime (bool reset, float delayTime)
    {
        yield return new WaitForSeconds (delayTime);
        if (!((SkyScrollRect)myScrollRect).IsDraging) {
            AutoScroll = true;
            if (reset) {
                for(int i=0;i<(GetElementCount ())/2;i++){
                    myScrollList.transform.GetChild (0).SetSiblingIndex (GetElementCount () - 1);
                }
                myscrollBar.value = (index+1) * 1f / (GetElementCount () - 1);
            }
        }
    }

    public override void OnSubPointDown ()
    {
        base.OnSubPointDown ();
    }
    
    public override void OnSubPointUp ()
    {
        base.OnSubPointUp ();
    }

    public override void NextElement ()
    {
        if (GetElementCount () <= 1)
            return;
        AutoScroll = true;
    }
    
    public override void PreElement ()
    {
        if (GetElementCount () <= 1)
            return;
        AutoScroll = true;
    }
}
