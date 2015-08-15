using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class SkySetParticleSortingLayer : MonoBehaviour {

	public string sortingLayerName="Default";
	public int sortingOrder=0;

	// Use this for initialization
	void Start () {
		Onchanged ();
	}
	#if UNITY_EDITOR
	// Update is called once per frame
	void Update () {
//		if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode) {
//			this.enabled = false;
//		} else {
			Onchanged();
//		}
	}
	#else

	#endif

	private void Onchanged(){
		if (particleSystem.renderer.sortingLayerName != sortingLayerName ||particleSystem.renderer.sortingOrder != sortingOrder) {
			particleSystem.renderer.sortingLayerName = sortingLayerName;
			particleSystem.renderer.sortingOrder = sortingOrder;
		}
	}
}
