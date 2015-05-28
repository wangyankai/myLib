using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum UIAnimation{
	NOAnimationIn,NOAnimationOut,ScaleIn,ScaleOut,LeftIn,LeftOut,RightIn,RightOut,TopIn,TopOut,BottomIn,BottomOut
}

public class UIWindow : MonoBehaviour {

	public UIAnimation UIAnimationIn = UIAnimation.NOAnimationIn;
	public SkyAniDuration VkAniDurationIn = SkyAniDuration.Linear;
	public UIAnimation UIAnimationOut = UIAnimation.NOAnimationOut;
	public SkyAniDuration VkAniDurationOut = SkyAniDuration.Linear;
	public GameObject buttonPrefab;
	// Use this for initialization
	void Start () {
		vkAniCompleteOut = new SkyAniCallBack();
		vkAniCompleteOut.AddCompleteMethod (delegate(){
			Debug.Log("I'm out");
			gameObject.SetActive(false);
		});
	}

	public void genButton(float x,float y,float width,float height){
		GameObject newButton = Instantiate (buttonPrefab) as GameObject;
		newButton.transform.parent = transform;
		newButton.transform.localPosition = new Vector3 (0,0,-4);
		RectTransform subRectTransform = newButton.transform as RectTransform;
		subRectTransform.localScale = new Vector3 (1, 1, 1);
		subRectTransform.offsetMin = new Vector2 (x-width/2f,y-height/2f);
		subRectTransform.offsetMax = new Vector2 (x+width/2f,y+height/2f);
		}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowIn(){
		shwoInAnimation();
		gameObject.SetActive(true);
	}

	private void shwoInAnimation(){
		switch(UIAnimationIn){
		case UIAnimation.ScaleIn:
			scaleIn();
			break;
		case UIAnimation.BottomIn:
			bottomIn();
			break;
		case UIAnimation.TopIn:
			topIn();
			break;
		case UIAnimation.LeftIn:
			leftIn();
			break;
		case UIAnimation.RightIn:
			rightIn();
			break;
		default:
			noAnimationIn();
			break;
		}
	}

	private void noAnimationIn(){
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localScale = Vector3.one;
		rectTransform.localPosition = new Vector3(0,0,0);
	}

	private void scaleIn(){
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localScale = Vector3.one;
		rectTransform.localPosition = new Vector3(0,0,0);
		SkyAnimator.scaleFrom(gameObject,1f,Vector3.one*0.05f,VkAniDurationIn,vkAniCompleteIn);
	}

	private void bottomIn(){
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localPosition = new Vector3(0,0,0);
		SkyAnimator.moveFrom(gameObject,1f,new Vector3(0,-Screen.height,0),true,VkAniDurationIn,vkAniCompleteIn);
	}

	private void topIn(){
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localScale = Vector3.one;
		rectTransform.localPosition = new Vector3(0,0,0);
		SkyAnimator.moveFrom(gameObject,1f,new Vector3(0,Screen.height,0),true,VkAniDurationIn,vkAniCompleteIn);
	}

	private void leftIn(){
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localScale = Vector3.one;
		rectTransform.localPosition = new Vector3(0,0,0);
		SkyAnimator.moveFrom(gameObject,1f,new Vector3(-Screen.width,0,0),true,VkAniDurationIn,vkAniCompleteIn);
	}

	private void rightIn(){
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localScale = Vector3.one;
		rectTransform.localPosition = new Vector3(0,0,0);
		SkyAnimator.moveFrom(gameObject,1f,new Vector3(Screen.width,0,0),true,VkAniDurationIn,vkAniCompleteIn);
	}



	public void ShowOut(){
		shwoOutAnimation();
	}
	
	private void shwoOutAnimation(){
		switch(UIAnimationOut){
		case UIAnimation.ScaleOut:
			scaleOut();
			break;
		case UIAnimation.BottomOut:
			bottomOut();
			break;
		case UIAnimation.TopOut:
			topOut();
			break;
		case UIAnimation.LeftOut:
			leftOut();
			break;
		case UIAnimation.RightOut:
			rightOut();
			break;
		default:
			noAnimationOut();
			break;
		}
	}

	private void noAnimationOut(){
		gameObject.SetActive(false);
	}

	private void scaleOut(){
		SkyAnimator.scaleTo(gameObject,1f,Vector3.one*0,VkAniDurationIn,vkAniCompleteOut);
	}

	private void bottomOut(){
		SkyAnimator.moveTo(gameObject,1f,new Vector3(0,-Screen.height,0),true,VkAniDurationIn,vkAniCompleteOut);
	}

	private void topOut(){
		SkyAnimator.moveTo(gameObject,1f,new Vector3(0,Screen.height,0),true,VkAniDurationIn,vkAniCompleteOut);
	}

	private void leftOut(){
		SkyAnimator.moveTo(gameObject,1f,new Vector3(-Screen.width,0,0),true,VkAniDurationIn,vkAniCompleteOut);
	}
	
	private void rightOut(){
		SkyAnimator.moveTo(gameObject,1f,new Vector3(Screen.width,0,0),true,VkAniDurationIn,vkAniCompleteOut);
	}

	private SkyAniCallBack vkAniCompleteIn =null;
	private SkyAniCallBack vkAniCompleteOut =null;
}
