using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SkyDragEvent : MonoBehaviour
 {

	private Vector2 startPoint;
	private Vector2 endPoint;
	public virtual void OnBeginDrag (UnityEngine.EventSystems.PointerEventData eventData)
	{
		Debug.Log ("start drag");
		if (eventData.button != PointerEventData.InputButton.Left) {
			return;
		}
		_isDraging = true;
		startPoint = eventData.position;
		Debug.Log (startPoint.x + "  "+ startPoint.y);
//		Debug.Log ("start drag");
	}
	
	public virtual void OnDrag (UnityEngine.EventSystems.PointerEventData eventData)
	{	
//		base.OnDrag (eventData);
	}
	
	public virtual void OnEndDrag (UnityEngine.EventSystems.PointerEventData eventData)
	{
		Debug.Log ("end drag");
		if (eventData.button != PointerEventData.InputButton.Left) {
			return;
		}

		_isDraging = false;
		endPoint = eventData.position;
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
//		Debug.Log ("end drag");
	}

	public bool IsDraging {
		get { return _isDraging;}
	}
	
	private bool _isDraging;
	
}
