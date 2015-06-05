using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkyAniSequence : SkyBaseAnimationNormal {

	public List<SkyAction> AnimationSequence ;

	public override void Init ()
	{
		base.Init ();
		AnimationSequence = new List<SkyAction> ();
		PlayTime = 0;
	}
	
	public SkyAniSequence(){
		this.Init ();
	}

	public void AppendAction(SkyAction skyAction){
		AnimationSequence.Add (skyAction);
		skyAction.SetAniamtionSeqence (this);
		PlayTime += skyAction.GetPlayTime ();
	}

	public void RemoveAction(SkyAction skyAction){
		AnimationSequence.Remove (skyAction);
		skyAction.SetAniamtionSeqence (null);
		PlayTime -= skyAction.GetPlayTime ();
	}

	public void RemoveAll(){
		foreach (SkyAction skyAction in AnimationSequence) {
			skyAction.SetAniamtionSeqence (null);
			AnimationSequence.Remove (skyAction);
		}
		PlayTime = 0;
	}

	public override void Play ()
	{
		if (AnimationSequence.Count > 0) {
			AnimationSequence[0].Play();
		} else {
			playAction.OnCompleteMethod();
		}
	}

	public void PlayNext(SkyAction skyAction){
		if (AnimationSequence.Contains (skyAction)) {
			int index = AnimationSequence.IndexOf (skyAction);
			if (index < AnimationSequence.Count - 1) {
				AnimationSequence [index + 1].Play ();
			} else {
				playAction.OnCompleteMethod();
			}
		}
	}
}
