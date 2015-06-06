using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SkyDelayAnimation : SkyBaseAnimationNormal {
	
	public SkyDelayAnimation(){
		this.Init ();
	}

	public override void Play ()
	{
		base.Play ();
		delayTimeAction (PlayTime,playAction);
	}

}
