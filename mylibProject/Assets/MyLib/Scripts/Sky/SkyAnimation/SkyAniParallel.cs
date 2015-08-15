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
		skyAction.ParentAction = this;
		if (skyAction.PlayTime > PlayTime) {
			PlayTime = skyAction.PlayTime;
			if (ParentAction != null)
				this.ParentAction.ReComputePlaytime ();
		}
	}

	public override void RemoveAction (SkyAction skyAction)
	{
		AnimationSequence.Remove (skyAction);
		skyAction.ParentAction = null;
		if (PlayTime == skyAction.PlayTime) {
			ReComputePlaytime ();
			if (ParentAction != null)
				this.ParentAction.ReComputePlaytime ();
		}
	}
	
	public override void ReComputePlaytime ()
	{
		PlayTime = 0;
		foreach (SkyAction skyAction in AnimationSequence) {
			if (skyAction.PlayTime > PlayTime)
				PlayTime = skyAction.PlayTime;
		}
	}

	public override void PlayLoop ()
	{
		base.PlayLoop ();
		if (AnimationSequence.Count > 0) {
			foreach (SkyAction skyAction in AnimationSequence) {
				skyAction.Play ();
			}
			DelayTimeAction (PlayTime, PlayCallBack);
		} else {
			PlayCallBack.OnCompleteMethod ();
		}
	}
	
	public  override void PlayNext (SkyAction skyAction)
	{
	}
	
}
