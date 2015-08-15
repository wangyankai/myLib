using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UI.Utils
{
	public class ResizeDialog : MonoBehaviour
	{
		public float devHeight = 9f;
		public float devWidth = 16f;
		// Use this for initialization
		void Start ()
		{
			resetSize ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		private void resetSize ()
		{
						
			float targetScale = transform.localScale.x * (Screen.height * devWidth) / (Screen.width * devHeight);
						
						
			transform.localScale = new Vector3 (targetScale, transform.localScale.y, transform.localScale.z);
		}
	}
}
