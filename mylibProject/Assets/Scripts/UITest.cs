using UnityEngine;
using System.Collections;
using System;

public class UITest : MonoBehaviour {

	TimeRecord myTimeRecord;
	// Use this for initialization
	void Start () {
		myTimeRecord = new TimeRecord ("test", SkyTime.MINUTE * 10, false);
		Debug.Log (myTimeRecord.ToString());
	}
	
	// Update is called once per frame
	void Update () {
//		letTime ();
	}

	public void ConvetTime(){
//		Debug.Log("nowTime  : " + DateTime.Now.ToString());
//		long nowTime = SkyTime.ConvertDateToLong (DateTime.Now);
//		DateTime dataTime = SkyTime.ConvertLongToDate (nowTime);
//		Debug.Log("nowTime  : " + nowTime);
//		Debug.Log (dataTime.ToString());
//		long leftTime = myTimeRecord.GetLeftTime ();
//		Debug.Log (leftTime);
//		long leftHour = leftTime / SkyTime.HOUR;
//		leftTime -= leftHour*SkyTime.HOUR;
//		long leftMinute = leftTime / SkyTime.MINUTE;
//		leftTime -= leftMinute*SkyTime.MINUTE;
//		long leftSecond = leftTime;
//		Debug.Log (leftHour + " : " + leftMinute + " : " + leftSecond);
	}

	long lastLeftTime =0;

	public void letTime(){
		long leftTime = myTimeRecord.GetLeftTime ();
		if (leftTime != lastLeftTime) {
			lastLeftTime=leftTime;
			long leftHour = leftTime / SkyTime.HOUR;
			leftTime -= leftHour * SkyTime.HOUR;
			long leftMinute = leftTime / SkyTime.MINUTE;
			leftTime -= leftMinute * SkyTime.MINUTE;
			long leftSecond = leftTime;
			Debug.Log (leftHour + " : " + leftMinute + " : " + leftSecond);
		}
	}

	public void ActivityStart(){
		myTimeRecord.CheckActivityStart ();
		Debug.Log (myTimeRecord.ToString());
	}
}
