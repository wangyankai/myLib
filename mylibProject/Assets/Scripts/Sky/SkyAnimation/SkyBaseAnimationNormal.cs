using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SkyBaseAnimationNormal : SkyAction
{

	public bool loop = false;
	public bool AutoRun = false;
	public float PlayTime = 1;
	public float DelayTime = 4;
	public float AutoStartDelayTime = 1;
	public SkyAniDuration PositionSkyAniDuration = SkyAniDuration.Linear;
	public SkyAniCallBack delayAction;
	public SkyAniCallBack playAction;
	public SkyAniSequence  sequence = null;


	public virtual  void Init ()
	{
		delayAction = new SkyAniCallBack ();
		delayAction.AddCompleteMethod (() => {
			Play ();});
		playAction = new SkyAniCallBack ();
		playAction.AddCompleteMethod (() => {
			if (playAction.OnStepCompleteMethod != null) {
				playAction.OnStepCompleteMethod ();
			}
			PlayNextAction();
			if (loop)
				DelayAction ();
		});
	}
	
	public virtual	void Play ()
	{
		if (playAction.OnStartMethod != null) {
			playAction.OnStartMethod ();
		}
	}
	
	public void PlayWithDelay ()
	{
		if (DelayTime > 0) {
			DelayAction ();
		} else {
			Play ();
		}
	}
	
	public virtual void DelayAction ()
	{

	}

	public virtual void PlayNextAction(){
       if (sequence != null) {
			sequence.PlayNext(this);
		}
	}

	public virtual void SetAniamtionSeqence(SkyAniSequence skyAniSequence){
		this.sequence = skyAniSequence;
	}

	public virtual void RemoveFromSeqence(){
		if (sequence != null) {
			sequence.RemoveAction(this);
		}
	}

	public  bool IsLoop(){
		return loop;
	}

	public  void SetLoop(bool isLoop){
		this.loop = isLoop;
	}

	public  bool IsAutoRun(){
		return AutoRun;
	}

	public void SetAutoRun(bool isAutoRun){
		this.AutoRun = isAutoRun;
	}

	public  float GetPlayTime(){
		return PlayTime;
	}

	public void SetPlayTime(float playTime){
		this.PlayTime = playTime;
	}

	public  float GetDelayTime(){
		return DelayTime;
	}

	public void SetDelayTime(float delayTime){
		this.DelayTime = delayTime;
	}

	public float time;

	public  Tweener RunDelayTime ()
	{
		this.time = 0;
		return DOTween.To (() => DelayTime, delegate (float x) {
			this.time = x;
		}, 1, 1).SetTarget (this);
	}


}
