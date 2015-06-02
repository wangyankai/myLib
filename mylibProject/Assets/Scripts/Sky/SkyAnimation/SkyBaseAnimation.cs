using UnityEngine;
using System.Collections;

public class SkyBaseAnimation : MonoBehaviour
{

	public bool loop = false;
	public bool AutoRun = false;
	public float PlayTime = 1;
	public float DelayTime = 1;
	public float AutoStartDelayTime = 1;
	public SkyAniDuration PositionSkyAniDuration = SkyAniDuration.Linear;
	protected SkyAniCallBack delayAction;
	protected SkyAniCallBack playAction;
	
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
		delayAction.AddCompleteMethod (() => {
			Play ();});
		playAction = new SkyAniCallBack ();
		playAction.AddCompleteMethod (() => {
			if (playAction.OnStepCompleteMethod != null) {
				playAction.OnStepCompleteMethod ();
			}
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

	public void PlayWithDelay(){
		DelayAction ();
	}

	protected virtual void DelayAction ()
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
	
	protected IEnumerator delayTimeAction (float delayTime, System.Action a)
	{
		yield return new WaitForSeconds (delayTime);
		a ();
	}
}
