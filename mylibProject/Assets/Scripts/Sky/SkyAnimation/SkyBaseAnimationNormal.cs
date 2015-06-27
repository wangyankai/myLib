using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SkyBaseAnimationNormal : SkyAction
{

	public bool Loop {
		get;
		set;
	}

	public bool AutoRun {
		get;
		set;
	}

	public float PlayTime {
		get;
		set;
	}

	public float DelayTime {
		get;
		set;
	}

	public float AutoStartDelayTime {
		get;
		set;
	}

	public SkyAniDuration PositionSkyAniDuration {
		get;
		set;
	}

	public SkyAniCallBack DelayAction {
		get;
		set;
	}

	public SkyAniCallBack PlayAction {
		get;
		set;
	}

	public SkyBaseSequence  ParentAction {
		get;
		set;
	}

	public virtual  void Init ()
	{
		PositionSkyAniDuration = SkyAniDuration.Linear;
		ParentAction = null;
		DelayAction = new SkyAniCallBack ();
		DelayAction.AddCompleteMethod (() => {
			PlayLoop ();});
		PlayAction = new SkyAniCallBack ();
		PlayAction.AddCompleteMethod (() => {
			if (PlayAction.OnStepCompleteMethod != null) {
				PlayAction.OnStepCompleteMethod ();
			}
			PlayNext ();
			if (Loop)
				Delay ();
		});
	}
	
	public virtual	void PlayLoop ()
	{
		if (PlayAction.OnStartMethod != null) {
			PlayAction.OnStartMethod ();
		}
	}
	
	public void Play ()
	{
		if (DelayTime > 0) {
			Delay ();
		} else {
			PlayLoop ();
		}
	}
	
	public virtual void Delay ()
	{
		delayTimeAction (DelayTime, DelayAction);
	}

	public virtual void PlayNext ()
	{
		if (ParentAction != null) {
			ParentAction.PlayNext (this);
		}
	}
	
	public virtual void RemoveFromSeqence ()
	{
		if (ParentAction != null) {
			ParentAction.RemoveAction (this);
		}
	}

	public float time;

	public void delayTimeAction (float delayTime, SkyAniCallBack skyAnicallBack)
	{
		Tweener tw = null;
		tw = RunDelayTime (delayTime, delayTime);
		tw.SetTarget (delayTime);
		tw.OnComplete (skyAnicallBack.OnCompleteMethod);
	}

	private  Tweener RunDelayTime (float endValue, float Duration)
	{
		this.time = 0;
		return DOTween.To (() => this.time, delegate (float x) {
			this.time = x;
		}, endValue, Duration).SetTarget (this);
	}


}
