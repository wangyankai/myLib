using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TestForEditor : MonoBehaviour
{
	public float parentHight = 1;
	public float parentWidth = 1;
	public Vector3 point;

	// Use this for initialization
	void Start ()
	{
		getNewSize ();
	}

	public void myUpdate ()
	{
	}

	public void getNewSize(){
//		RectTransform parentTransform = transform as RectTransform;
//		parentHight = parentTransform.rect.height;
//		parentWidth = parentTransform.rect.width;
//		Debug.Log (" H " + parentHight + " w " + parentWidth);
	}

	void OnEnable () {
		getNewSize();
	}

	void Reset () {
//		getNewSize();
	}
}
