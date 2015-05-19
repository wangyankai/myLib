using UnityEngine;
using System.Collections;

public class SkySetParticleSortingLayer : MonoBehaviour {

	public string sortingLayerName="Default";
	public int sortingOrder=0;

	// Use this for initialization
	void Start () {
		particleSystem.renderer.sortingLayerName = sortingLayerName;
		particleSystem.renderer.sortingOrder = sortingOrder;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
