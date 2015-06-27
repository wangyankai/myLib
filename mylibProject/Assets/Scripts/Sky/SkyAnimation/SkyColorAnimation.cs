using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkyColorAnimation : SkyBaseAnimationObject {

	public Color colorMin;
	public Color colorMax;

	SkyAniCallBack colorFirstComplete;
	private Image mImage;
	
	public override void Init(){
		base.Init ();
		mImage = GetComponent<Image> ();
		mImage.color = colorMin;

		colorFirstComplete = new SkyAniCallBack ();
		colorFirstComplete.SetCompleteMethod (()=>{ SkyAnimator.colorTo (mImage, PlayTime/2f, colorMin, SkyAniDuration.Linear, PlayCallBack);});

	}
	
	public override void PlayLoop(){
		base.PlayLoop ();
		gameObject.SetActive (true);
		SkyAnimator.colorTo (mImage, PlayTime/2f, colorMax, SkyAniDuration.Linear, colorFirstComplete);
	}
	
	public override void Delay(){
//			SkyAnimator.colorTo (mImage, DelayTime, colorMin, SkyAniDuration.Linear, delayComplete);
		base.Delay ();
	}
	

}
