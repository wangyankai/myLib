using UnityEngine;
using System.Collections;

//[RequireComponent( typeof(BoxCollider2D) )]
public class DragTest : MonoBehaviour {
	private Vector2 startPoint;
	private Vector2 endPoint;
	void Start () {
		 
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log ("updata");
	}

	void OnMouseDown () {
		Debug.Log ("MouseDown");
		startPoint = Input.mousePosition;
		Debug.Log (startPoint.x + "  "+ startPoint.y);
	}
	
	void OnMouseUp () {
		Debug.Log ("MouseUp");
		endPoint = Input.mousePosition;
		Debug.Log (endPoint.x + "  "+endPoint.y);
		if (endPoint.x < startPoint.x) {
			Debug.Log ("down");
		}
		if (endPoint.x > startPoint.x) {
			Debug.Log ("up");
		}
		if (endPoint.y < startPoint.y) {
			Debug.Log ("right");
		}
		if (endPoint.y > startPoint.y) {
			Debug.Log ("left");
		}
	}
}
