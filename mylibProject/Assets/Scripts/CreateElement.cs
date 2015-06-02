using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateElement : MonoBehaviour {

	public SkyBezierCurveOject skyBezierObject;
	public GameObject targarPosition;
	public GameObject startPosition;
	List<SkyBezierCurveOject> elements = new List<SkyBezierCurveOject>();
	public int count = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void create(){
		elements.Clear ();
		for (int i=0; i<count; i++) {
			SkyBezierCurveOject element = Instantiate (skyBezierObject) as SkyBezierCurveOject;
			element.Init();
			element.skyBezierCurve.endPoint = targarPosition.transform.localPosition;
			element.transform.SetParent(transform,false);
			element.transform.localPosition = startPosition.transform.localPosition;
			element.transform.localScale = Vector3.zero;
			element.skyBezierCurve.startPoint = element.transform.localPosition;
			Vector3 temp1 = Vector3.zero;
			temp1.x = genOffset() + element.transform.localPosition.x;
			temp1.y = genOffset();
			Vector3 temp = Vector3.zero;
			temp.x = genOffset() - element.transform.localPosition.x;
			temp.y = genOffset();

			element.skyBezierCurve.middlePoints.Clear();
			element.skyBezierCurve.middlePoints.Add(temp1);
			element.skyBezierCurve.middlePoints.Add(temp);
			element.skyBezierCurve.timeDuration =  Random.Range(1,2);
			element.DelayTime = i*1f/count;
			element.skyBezierCurve.CreateCurve();
			element.ActionCallBack.AddCompleteMethod(()=>{Destroy(element.gameObject);});
			elements.Add(element);
			element.PlayWithDelay();	
		}
	}


	private float genOffset(){
		return Random.Range(-100,100);
	}
}
