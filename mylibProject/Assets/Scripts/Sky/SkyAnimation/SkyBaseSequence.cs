﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkyBaseSequence : SkyBaseAnimationNormal
{

	public List<SkyAction> AnimationSequence ;

	public override void Init ()
	{
		base.Init ();
		AnimationSequence = new List<SkyAction> ();
		PlayTime = 0;
	}
	
	public SkyBaseSequence ()
	{
		this.Init ();
	}

	public virtual void AppendAction (SkyAction skyAction)
	{

	}

	public virtual void RemoveAction (SkyAction skyAction)
	{

	}

	public void RemoveAll ()
	{
		foreach (SkyAction skyAction in AnimationSequence) {
			skyAction.SetAniamtionSeqence (null);
		}
		AnimationSequence.Clear ();
		PlayTime = 0;
		this.ParentAction.ReComputePlaytime ();
	}

	public  virtual void PlayNext (SkyAction skyAction)
	{

	}

	public virtual void ReComputePlaytime(){
	}
}
