using UnityEngine;
using System.Collections;
using GAF.Core;

public class GAFTest : MonoBehaviour {
	public GAFMovieClip robot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void testClick(){
		robot.setSequence ("stand_left",true);
	}
}
