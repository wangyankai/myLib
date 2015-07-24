using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum UIAnimation
{
	NOAnimation,
	Scale,
	Left,
	Custom,
	Right,
	Top,
	Bottom,
}

public enum UIDisplayState
{
	Showining,
	Normal,
	Showouting,
	Disable
}

public class UIWindow : MonoBehaviour
{

	public bool DestoryOnQuit = false;
	public UIAnimation UIAnimationIn = UIAnimation.NOAnimation;
	public SkyAniDuration AniDurationIn = SkyAniDuration.Linear;
	public float AppearTime = 1f;
	public UIAnimation UIAnimationOut = UIAnimation.NOAnimation;
	public SkyAniDuration AniDurationOut = SkyAniDuration.Linear;
	public float DisappearTime = 1f;
	public bool AutoQuit = false;
	public float DisplayTime = 2;
	public UIDisplayState MUIDisplayState;

	void Start ()
	{
		Init ();
		ShowIn ();
	}

	public virtual void Init ()
	{
		float t = Time.realtimeSinceStartup;

		Debug.Log (t);
		while (Time.realtimeSinceStartup - t < 5) {
			 
        }
		Debug.Log (Time.realtimeSinceStartup);
		MUIDisplayState = UIDisplayState.Disable;
		initPosition = this.transform.localPosition;
		OutAction = new SkyAniCallBack ();
		InAction = new SkyAniCallBack ();
		InAction.SetStartMethod (() => {
			MUIDisplayState = UIDisplayState.Showining;});
		InAction.SetCompleteMethod (() => {
			MUIDisplayState = UIDisplayState.Normal;});

		OutAction.SetStartMethod (() => {
			MUIDisplayState = UIDisplayState.Showouting;});
		OutAction.SetCompleteMethod (() => {
			MUIDisplayState = UIDisplayState.Disable;});

		InAction.AddCompleteMethod (TickUntilQuit);

	}

	public float GetTotalDisplayTime ()
	{
		return AppearTime + DisappearTime + DisplayTime;
	}

	private void TickUntilQuit ()
	{
		if (AutoQuit) {
			SkyDelayAnimation skyDelayAnimation = SkyAnimator.delayTo (DisplayTime, ShowOut);
			skyDelayAnimation.Play ();
		}
	}

	public virtual void ShowIn ()
	{
		if (MUIDisplayState == UIDisplayState.Disable) {
			shwoInAnimation ();
			gameObject.SetActive (true);
		}
	}

	private void shwoInAnimation ()
	{
		switch (UIAnimationIn) {
		case UIAnimation.Scale:
			scaleIn ();
			break;
		case UIAnimation.Bottom:
			bottomIn ();
			break;
		case UIAnimation.Top:
			topIn ();
			break;
		case UIAnimation.Left:
			leftIn ();
			break;
		case UIAnimation.Right:
			rightIn ();
			break;
		case UIAnimation.Custom:
			customIn ();
			break;
		default:

			noAnimationIn ();
			break;
		}
	}

	private void noAnimationIn ()
	{

		if (InAction.OnStartMethod != null) {
			InAction.OnStartMethod ();
		}
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localScale = Vector3.one;
		rectTransform.localPosition = new Vector3 (0, 0, 0);
		if (InAction.OnCompleteMethod != null) {
			InAction.OnCompleteMethod ();
		}
	}

	private void scaleIn ()
	{
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localScale = Vector3.one;
		SkyAnimator.scaleFrom (gameObject, AppearTime, Vector3.one * 0.05f, AniDurationIn, InAction);
	}

	private void bottomIn ()
	{
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localPosition = initPosition;
		SkyAnimator.moveFrom (gameObject, AppearTime, new Vector3 (initPosition.x, -Screen.height, 0), true, AniDurationIn, InAction);
	}

	private void topIn ()
	{
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localScale = Vector3.one;
		rectTransform.localPosition = initPosition;
		SkyAnimator.moveFrom (gameObject, AppearTime, new Vector3 (initPosition.x, Screen.height, 0), true, AniDurationIn, InAction);
	}

	private void leftIn ()
	{
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localScale = Vector3.one;
		rectTransform.localPosition = initPosition;
		SkyAnimator.moveFrom (gameObject, AppearTime, new Vector3 (-Screen.width, initPosition.y, 0), true, AniDurationIn, InAction);
	}

	private void rightIn ()
	{
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localScale = Vector3.one;
		rectTransform.localPosition = initPosition;
		SkyAnimator.moveFrom (gameObject, AppearTime, new Vector3 (Screen.width, initPosition.y, 0), true, AniDurationIn, InAction);
	}

	protected virtual void customIn ()
	{
	}

	public virtual void ShowOut ()
	{
		if (MUIDisplayState == UIDisplayState.Normal) {
			setQuitAction ();
			shwoOutAnimation ();
		}
	}

	private void setQuitAction ()
	{
		if (DestoryOnQuit) {
			OutAction.AddCompleteMethod (() => {
				Destroy (gameObject);
			}
			);
		} else {
			OutAction.AddCompleteMethod (() => {
				gameObject.SetActive (false);
			}
			);
		}
	}

	private void shwoOutAnimation ()
	{
		switch (UIAnimationOut) {
		case UIAnimation.Scale:
			scaleOut ();
			break;
		case UIAnimation.Bottom:
			bottomOut ();
			break;
		case UIAnimation.Top:
			topOut ();
			break;
		case UIAnimation.Left:
			leftOut ();
			break;
		case UIAnimation.Right:
			rightOut ();
			break;
		case UIAnimation.Custom:
			customOut ();
			break;
		default:
			noAnimationOut ();
			break;
		}

	}

	private void noAnimationOut ()
	{
		if (OutAction.OnStartMethod != null) {
			OutAction.OnStartMethod ();
		}
		if (OutAction.OnCompleteMethod != null) {
			OutAction.OnCompleteMethod ();
		}
	}

	private void scaleOut ()
	{
		SkyAnimator.scaleTo (gameObject, DisappearTime, Vector3.one * 0, AniDurationOut, OutAction);
	}

	private void bottomOut ()
	{
		SkyAnimator.moveTo (gameObject, DisappearTime, new Vector3 (initPosition.x, -Screen.height, 0), true, AniDurationOut, OutAction);
	}

	private void topOut ()
	{
		SkyAnimator.moveTo (gameObject, DisappearTime, new Vector3 (initPosition.x, Screen.height, 0), true, AniDurationOut, OutAction);
	}

	private void leftOut ()
	{
		SkyAnimator.moveTo (gameObject, DisappearTime, new Vector3 (-Screen.width, initPosition.y, 0), true, AniDurationOut, OutAction);
	}
	
	private void rightOut ()
	{
		SkyAnimator.moveTo (gameObject, DisappearTime, new Vector3 (Screen.width, initPosition.y, 0), true, AniDurationOut, OutAction);
	}

	protected virtual void customOut ()
	{
	}

	public SkyAniCallBack InAction = null;
	public SkyAniCallBack OutAction = null;
	private Vector3 initPosition;
}
