using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkyColorAnimation : SkyBaseAnimation {

	public Color colorMin;
	public Color colorMax;

	SkyAniCallBack colorFirstComplete;
	private Image mImage;
	
	public override void Init(){
	
		mImage = GetComponent<Image> ();
		mImage.color = colorMin;

		colorFirstComplete = new SkyAniCallBack ();
		colorFirstComplete.SetCompleteMethod (()=>{ SkyAnimator.colorTo (mImage, PlayTime/2f, colorMin, SkyAniDuration.Linear, playComplete);});

	}
	
	public override void Play(){
		gameObject.SetActive (true);
		SkyAnimator.colorTo (mImage, PlayTime/2f, colorMax, SkyAniDuration.Linear, colorFirstComplete);
	}
	
	public override void DelayAction(){
			SkyAnimator.colorTo (mImage, DelayTime, colorMin, SkyAniDuration.Linear, delayComplete);
	}
	

}
