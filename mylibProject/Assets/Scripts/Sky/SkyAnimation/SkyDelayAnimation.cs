using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SkyDelayAnimation : SkyBaseAnimationNormal {
	public override void Init ()
	{
		base.Init ();
	}

	public SkyDelayAnimation(){
		this.Init ();
	}

	public override void Play ()
	{
		Tweener tw = null;
		tw = RunDelayTime ();
		tw.OnComplete (playAction.OnCompleteMethod);
	}

}
