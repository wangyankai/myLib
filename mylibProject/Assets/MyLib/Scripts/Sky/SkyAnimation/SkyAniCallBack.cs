using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SkyAniCallBack
{

	public TweenCallback OnCompleteMethod;
	public TweenCallback OnStartMethod;
	public TweenCallback OnStepCompleteMethod;

	public SkyAniCallBack ()
	{
	}

	public void SetCompleteMethod (System.Action a)
	{
		OnCompleteMethod = new TweenCallback (a);
	}
	
	public void SetStartMethod (System.Action a)
	{
		OnStartMethod = new TweenCallback (a);
	}
	
	public void SetStepCompleteMethod (System.Action a)
	{
		OnStepCompleteMethod = new TweenCallback (a);
	}

	public void AddCompleteMethod (System.Action a)
	{
		OnCompleteMethod += new TweenCallback (a);
	}

	public void AddStartMethod (System.Action a)
	{
		OnStartMethod += new TweenCallback (a);
	}

	public void AddStepCompleteMethod (System.Action a)
	{
		OnStepCompleteMethod += new TweenCallback (a);
	}


}
