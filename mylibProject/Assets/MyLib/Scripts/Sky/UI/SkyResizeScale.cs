using UnityEngine;
using System.Collections;

public class SkyResizeScale : MonoBehaviour {

	public float h = 9;
	public float w = 16;

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (Screen.width*h/(w*Screen.height),1,1);
	}
	
	// Update is called once per frame
	void Update () {
//		transform.localScale = new Vector3 (Screen.width/w,Screen.height/h,1);
	}
}
