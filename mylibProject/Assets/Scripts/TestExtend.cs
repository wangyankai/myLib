using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestExtend : MonoBehaviour {

	public Button button;

	// Use this for initialization
	void Start () {
//		this.DOX ();
		SkyAniCallBack skyAniComplete = new SkyAniCallBack ();
//		skyAniComplete.setCompleteMethod (()=>{Debug.Log ("EndTest  ");});
//		SkyAnimator.moveFrom (button.gameObject,10,new Vector3(100,100,0),true,SkyAniDuration.Linear,skyAniComplete);
		SkyAnimator.moveBy (button.gameObject,10,new Vector3(100,100,0),true,SkyAniDuration.Linear,skyAniComplete);
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (testX);
	}

	public float testX =0;
}
