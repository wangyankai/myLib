using UnityEngine;
using System.Collections;

public class SkyBaseAnimationObject : MonoBehaviour,SkyAction
{

	public bool Loop {
		get{ return _loop;}
		set{ _loop = value;}
	}

	[SerializeField]
	private bool
		_loop = false;

	public bool AutoRun {
		get{ return _AutoRun;}
		set{ _AutoRun = value;}
	}

	[SerializeField]
	private bool
		_AutoRun = false;

	public float PlayTime {
		get{ return _PlayTime;}
		set{ _PlayTime = value;}
	}

	[SerializeField]
	private float
		_PlayTime = 1;

	public float DelayTime {
		get{ return _DelayTime;}
		set{ _DelayTime = value;}
	}

	[SerializeField]
	private float
		_DelayTime = 0;

	public float AutoStartDelayTime {
		get{ return _AutoStartDelayTime;}
		set{ _AutoStartDelayTime = value;}
	}

	[SerializeField]
	private float
		_AutoStartDelayTime = 1;

	public SkyAniDuration PositionSkyAniDuration {
		get{ return _PositionSkyAniDuration;}
		set{ _PositionSkyAniDuration = value;}
	}

	[SerializeField]
	private SkyAniDuration
		_PositionSkyAniDuration = SkyAniDuration.Linear;

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

	void Start ()
	{
		Init ();
		if (AutoRun) {
			StartCoroutine (delayTimeAction (AutoStartDelayTime, PlayLoop));
		}
	}

	public virtual  void Init ()
	{

		ParentAction = null;
		DelayAction = new SkyAniCallBack ();
		DelayAction.SetCompleteMethod (() => {
			PlayLoop ();});
		PlayAction = new SkyAniCallBack ();
		PlayAction.SetCompleteMethod (() => {
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

		if (DelayAction.OnStartMethod != null) {
			DelayAction.OnStartMethod ();
		}
		if (DelayTime > 0) {
			StartCoroutine (delayTimeAction (DelayTime, () => {
				DelayAction.OnCompleteMethod ();}));
		} else {
			DelayAction.OnCompleteMethod ();
		}
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
	
	protected IEnumerator delayTimeAction (float delayTime, System.Action a)
	{
		yield return new WaitForSeconds (delayTime);
		a ();
	}
}
