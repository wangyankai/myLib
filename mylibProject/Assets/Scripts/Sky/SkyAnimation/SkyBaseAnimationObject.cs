using UnityEngine;
using System.Collections;

public class SkyBaseAnimationObject : MonoBehaviour,SkyAction
{

	public bool loop = false;
	public bool AutoRun = false;
	public float PlayTime = 1;
	public float DelayTime = 1;
	public float AutoStartDelayTime = 1;
	public SkyAniDuration PositionSkyAniDuration = SkyAniDuration.Linear;
	public SkyAniCallBack delayAction;
	public SkyAniCallBack playAction;
	public SkyBaseSequence  sequence = null;
	
	void Start ()
	{
		Init ();
		if (AutoRun) {
			StartCoroutine (delayTimeAction (AutoStartDelayTime, Play));
		}
	}

	public virtual  void Init ()
	{
		delayAction = new SkyAniCallBack ();
		delayAction.SetCompleteMethod (() => {
			Play ();});
		playAction = new SkyAniCallBack ();
		playAction.SetCompleteMethod (() => {
			if (playAction.OnStepCompleteMethod != null) {
				playAction.OnStepCompleteMethod ();
			}
			PlayNextAction ();
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

		if (delayAction.OnStartMethod != null) {
			delayAction.OnStartMethod ();
		}
		if (DelayTime > 0) {
			StartCoroutine (delayTimeAction (DelayTime, () => {
				delayAction.OnCompleteMethod ();}));
		} else {
			delayAction.OnCompleteMethod ();
		}
	}

	public virtual void PlayNextAction ()
	{
		if (sequence != null) {
			sequence.PlayNext (this);
		}
	}

	public virtual void SetAniamtionSeqence (SkyBaseSequence skyAniSequence)
	{
		this.sequence = skyAniSequence;
	}

	public virtual void RemoveFromSeqence ()
	{
		if (sequence != null) {
			sequence.RemoveAction (this);
		}
	}

	public  bool IsLoop ()
	{
		return loop;
	}
	
	public  void SetLoop (bool isLoop)
	{
		this.loop = isLoop;
	}
	
	public  bool IsAutoRun ()
	{
		return AutoRun;
	}
	
	public void SetAutoRun (bool isAutoRun)
	{
		this.AutoRun = isAutoRun;
	}
	
	public  float GetPlayTime ()
	{
		return PlayTime;
	}
	
	public void SetPlayTime (float playTime)
	{
		this.PlayTime = playTime;
	}
	
	public  float GetDelayTime ()
	{
		return DelayTime;
	}
	
	public void SetDelayTime (float delayTime)
	{
		this.DelayTime = delayTime;
	}
	
	protected IEnumerator delayTimeAction (float delayTime, System.Action a)
	{
		yield return new WaitForSeconds (delayTime);
		a ();
	}
}
