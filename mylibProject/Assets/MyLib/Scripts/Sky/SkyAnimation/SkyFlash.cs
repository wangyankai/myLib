using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkyFlash : SkyBaseAnimationObject
{
	
	private float parentHight = 1;
	private float parentWidth = 1;
	public Vector3 scaleMin;
	public Vector3 scaleMax;
	public Color colorMin;
	public Color colorMax;
	public Vector3 positionMin;
	public Vector3 positionMid;
	public Vector3 positionMax;
	SkyAniCallBack scalFirstComplete;
	SkyAniCallBack colorFirstComplete;
	SkyAniCallBack positionFirstComplete;
	private Image mImage;

	public override void Init ()
	{
		base.Init ();
		RectTransform parentTransform = transform.parent.transform as RectTransform;
		parentHight = parentTransform.rect.height;
		parentWidth = parentTransform.rect.width;
		mImage = GetComponent<Image> ();
		mImage.color = colorMin;
		scalFirstComplete = new SkyAniCallBack ();
		scalFirstComplete.SetCompleteMethod (() => {
			SkyAnimator.scaleTo (gameObject, PlayTime / 2f, scaleMin, SkyAniDuration.Linear, null);});
		positionFirstComplete = new SkyAniCallBack ();
		positionFirstComplete.AddCompleteMethod (() => {
			SkyAnimator.moveTo (gameObject, PlayTime / 2f, SkyUtil.reletiveToLocal (positionMax, parentWidth, parentHight), true, PositionSkyAniDuration, PlayCallBack);});
		colorFirstComplete = new SkyAniCallBack ();
		colorFirstComplete.SetCompleteMethod (() => {
			SkyAnimator.colorTo (mImage, PlayTime / 2f, colorMin, SkyAniDuration.Linear, null);});
	}

	public override void PlayLoop ()
	{
		base.PlayLoop ();
		gameObject.SetActive (true);
		transform.localScale = scaleMin;
		transform.localPosition = SkyUtil.reletiveToLocal (positionMin, parentWidth, parentHight);
		SkyAnimator.moveTo (gameObject, PlayTime / 2f, SkyUtil.reletiveToLocal (positionMid, parentWidth, parentHight), true, PositionSkyAniDuration, positionFirstComplete);
		SkyAnimator.scaleTo (gameObject, PlayTime / 2f, scaleMax, SkyAniDuration.Linear, scalFirstComplete);
		SkyAnimator.colorTo (mImage, PlayTime / 2f, colorMax, SkyAniDuration.Linear, colorFirstComplete);
	}

	public override void Delay ()
	{
//          SkyAnimator.colorTo (mImage, DelayTime, colorMin, SkyAniDuration.Linear, delayComplete);
		base.Delay ();
	}

}
