using UnityEngine;
using System.Collections;

public class SkyAniSequence : SkyBaseSequence
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
		skyAction.ParentAction = null;
		PlayTime += skyAction.PlayTime;
		if (ParentAction != null)
			this.ParentAction.ReComputePlaytime ();
	}

	public override void RemoveAction (SkyAction skyAction)
	{
		AnimationSequence.Remove (skyAction);
		skyAction.ParentAction = null;
		PlayTime -= skyAction.PlayTime;
		if (ParentAction != null)
			this.ParentAction.ReComputePlaytime ();
	}
	
	public override void PlayLoop ()
	{
		base.PlayLoop ();
		if (AnimationSequence.Count > 0) {
			AnimationSequence [0].Play ();
		} else {
			PlayCallBack.OnCompleteMethod ();
		}
	}
	
	public  override void PlayNext (SkyAction skyAction)
	{
		if (AnimationSequence.Contains (skyAction)) {
			int index = AnimationSequence.IndexOf (skyAction);
			if (index < AnimationSequence.Count - 1) {
				AnimationSequence [index + 1].Play ();
			} else {
				PlayCallBack.OnCompleteMethod ();
			}
		}
	}

	public override void ReComputePlaytime ()
	{
		PlayTime = 0;
		foreach (SkyAction skyAction in AnimationSequence) {
			PlayTime += skyAction.PlayTime;
		}
	}
}
