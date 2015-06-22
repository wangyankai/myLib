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

public class UIWindow : MonoBehaviour
{

	public UIAnimation UIAnimationIn = UIAnimation.NOAnimation;
	public SkyAniDuration AniDurationIn = SkyAniDuration.Linear;
	public UIAnimation UIAnimationOut = UIAnimation.NOAnimation;
	public SkyAniDuration AniDurationOut = SkyAniDuration.Linear;

	void Start ()
	{
		Init ();
		ShowIn ();
	}

	public virtual void Init ()
	{
		outAction = new SkyAniCallBack ();
		inAction = new SkyAniCallBack ();
		inAction.SetStartMethod (() => {
			Debug.Log ("aaaaaaabbbb");});
		inAction.AddStartMethod (() => {
			Debug.Log ("aaaaaaaaaa");});

	}

	

//	public void genButton(float x,float y,float width,float height){
//		GameObject newButton = Instantiate (buttonPrefab) as GameObject;
//		newButton.transform.parent = transform;
//		newButton.transform.localPosition = new Vector3 (0,0,-4);
//		RectTransform subRectTransform = newButton.transform as RectTransform;
//		subRectTransform.localScale = new Vector3 (1, 1, 1);
//		subRectTransform.offsetMin = new Vector2 (x-width/2f,y-height/2f);
//		subRectTransform.offsetMax = new Vector2 (x+width/2f,y+height/2f);
//		}

	public virtual void ShowIn ()
	{
		shwoInAnimation ();
		gameObject.SetActive (true);
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

		if (inAction.OnStartMethod != null) {
			inAction.OnStartMethod ();
		}
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localScale = Vector3.one;
		rectTransform.localPosition = new Vector3 (0, 0, 0);
		if (inAction.OnCompleteMethod != null) {
			inAction.OnCompleteMethod ();
		}
	}

	private void scaleIn ()
	{
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localScale = Vector3.one;
		rectTransform.localPosition = new Vector3 (0, 0, 0);
		SkyAnimator.scaleFrom (gameObject, 1f, Vector3.one * 0.05f, AniDurationIn, inAction);
	}

	private void bottomIn ()
	{
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localPosition = new Vector3 (0, 0, 0);
		SkyAnimator.moveFrom (gameObject, 1f, new Vector3 (0, -Screen.height, 0), true, AniDurationIn, inAction);
	}

	private void topIn ()
	{
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localScale = Vector3.one;
		rectTransform.localPosition = new Vector3 (0, 0, 0);
		SkyAnimator.moveFrom (gameObject, 1f, new Vector3 (0, Screen.height, 0), true, AniDurationIn, inAction);
	}

	private void leftIn ()
	{
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localScale = Vector3.one;
		rectTransform.localPosition = new Vector3 (0, 0, 0);
		SkyAnimator.moveFrom (gameObject, 1f, new Vector3 (-Screen.width, 0, 0), true, AniDurationIn, inAction);
	}

	private void rightIn ()
	{
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localScale = Vector3.one;
		rectTransform.localPosition = new Vector3 (0, 0, 0);
		SkyAnimator.moveFrom (gameObject, 1f, new Vector3 (Screen.width, 0, 0), true, AniDurationIn, inAction);
	}

	protected virtual void customIn ()
	{
	}

	public virtual void ShowOut ()
	{
		shwoOutAnimation ();
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
		if (outAction.OnStartMethod != null) {
			outAction.OnStartMethod ();
		}
		if (outAction.OnCompleteMethod != null) {
			outAction.OnCompleteMethod ();
		}
	}

	private void scaleOut ()
	{
		SkyAnimator.scaleTo (gameObject, 1f, Vector3.one * 0, AniDurationIn, outAction);
	}

	private void bottomOut ()
	{
		SkyAnimator.moveTo (gameObject, 1f, new Vector3 (0, -Screen.height, 0), true, AniDurationIn, outAction);
	}

	private void topOut ()
	{
		SkyAnimator.moveTo (gameObject, 1f, new Vector3 (0, Screen.height, 0), true, AniDurationIn, outAction);
	}

	private void leftOut ()
	{
		SkyAnimator.moveTo (gameObject, 1f, new Vector3 (-Screen.width, 0, 0), true, AniDurationIn, outAction);
	}
	
	private void rightOut ()
	{
		SkyAnimator.moveTo (gameObject, 1f, new Vector3 (Screen.width, 0, 0), true, AniDurationIn, outAction);
	}

	protected virtual void customOut ()
	{
	}

	public SkyAniCallBack inAction = null;
	public SkyAniCallBack outAction = null;
}
