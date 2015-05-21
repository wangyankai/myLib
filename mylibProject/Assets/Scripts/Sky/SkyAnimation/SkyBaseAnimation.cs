using UnityEngine;
using System.Collections;

public class SkyBaseAnimation : MonoBehaviour
{

	public bool loop = true;
	public bool AutoRun = true;
	public float PlayTime=1;
	public float DelayTime=1;
	public float AutoStartDelayTime=1;
	public SkyAniDuration PositionSkyAniDuration = SkyAniDuration.Linear;
	protected SkyAniCallBack delayComplete;
	protected SkyAniCallBack playComplete;
	
	void Start ()
	{
		baseInit ();
		Init ();
		if (AutoRun) {
			StartCoroutine (delayTimeAction (AutoStartDelayTime, Play));
		}
	}

	void baseInit ()
	{
		delayComplete = new SkyAniCallBack ();
		delayComplete.SetCompleteMethod (() => {
			Play ();});
		playComplete = new SkyAniCallBack ();
		playComplete.SetCompleteMethod (() => {
			PlayEnd ();
			if (loop)
				DelayAction ();});
	}

	public virtual  void Init ()
	{

	}
	
	public virtual	void Play ()
	{
	
	}

	public virtual void PlayEnd ()
	{
	}
	
	public virtual void DelayAction ()
	{

	}
	
	protected IEnumerator delayTimeAction (float delayTime, System.Action a)
	{
		yield return new WaitForSeconds (delayTime);
		a ();
	}
}
