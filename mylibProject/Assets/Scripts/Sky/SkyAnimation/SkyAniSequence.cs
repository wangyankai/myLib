using UnityEngine;
using System.Collections;

public class SkyAniSequence : SkyBaseSequence {
	

	public override void AppendAction(SkyAction skyAction){
		AnimationSequence.Add (skyAction);
		skyAction.SetAniamtionSeqence (this);
		PlayTime += skyAction.GetPlayTime ();
		this.sequence.ReComputePlaytime ();
	}
	
	public override void RemoveAction(SkyAction skyAction){
		AnimationSequence.Remove (skyAction);
		skyAction.SetAniamtionSeqence (null);
		PlayTime -= skyAction.GetPlayTime ();
		this.sequence.ReComputePlaytime ();
	}

	
	public override void Play ()
	{
		base.Play ();
		if (AnimationSequence.Count > 0) {
			AnimationSequence[0].Play();
		} else {
			playAction.OnCompleteMethod();
		}
	}
	
	public  override void PlayNext(SkyAction skyAction){
		if (AnimationSequence.Contains (skyAction)) {
			int index = AnimationSequence.IndexOf (skyAction);
			if (index < AnimationSequence.Count - 1) {
				AnimationSequence [index + 1].Play ();
			} else {
				playAction.OnCompleteMethod();
			}
		}
	}

	public override void ReComputePlaytime(){
		PlayTime = 0;
		foreach(SkyAction skyAction in AnimationSequence){
				PlayTime += skyAction.GetPlayTime ();
		}
	}
}
