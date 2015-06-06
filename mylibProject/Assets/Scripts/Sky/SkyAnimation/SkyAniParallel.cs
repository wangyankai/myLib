using UnityEngine;
using System.Collections;

public class SkyAniParallel : SkyBaseSequence
{

	public override void AppendAction (SkyAction skyAction)
	{
		AnimationSequence.Add (skyAction);
		setAction (skyAction);
	}

	public override  void AddHead (SkyAction skyAction)
	{
		AnimationSequence.Insert (0, skyAction);
		setAction (skyAction);
	}

	private void setAction (SkyAction skyAction)
	{
		skyAction.SetAniamtionSeqence (this);
		if (skyAction.GetPlayTime () > PlayTime) {
			PlayTime = skyAction.GetPlayTime ();
			if (ParentAction != null)
				this.ParentAction.ReComputePlaytime ();
		}
	}

	public override void RemoveAction (SkyAction skyAction)
	{
		AnimationSequence.Remove (skyAction);
		skyAction.SetAniamtionSeqence (null);
		if (PlayTime == skyAction.GetDelayTime ()) {
			ReComputePlaytime ();
			if (ParentAction != null)
				this.ParentAction.ReComputePlaytime ();
		}
	}
	
	public override void ReComputePlaytime ()
	{
		PlayTime = 0;
		foreach (SkyAction skyAction in AnimationSequence) {
			if (skyAction.GetPlayTime () > PlayTime)
				PlayTime = skyAction.GetPlayTime ();
		}
	}

	public override void PlayLoop ()
	{
		base.PlayLoop ();
		if (AnimationSequence.Count > 0) {
			foreach (SkyAction skyAction in AnimationSequence) {
				skyAction.Play ();
			}
			delayTimeAction (PlayTime, playAction);
		} else {
			playAction.OnCompleteMethod ();
		}
	}
	
	public  override void PlayNext (SkyAction skyAction)
	{
	}
	
}
