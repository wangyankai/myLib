using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UI.Utils
{
		public class ResizeByAuto : MonoBehaviour
		{
				public float devHeight = 3f;
				public float devWidth = 4f;
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
						
						float targetScale = transform.localScale.x * Screen.width * devHeight / (Screen.height * devWidth);
						
						
						transform.localScale = new Vector3 (targetScale, targetScale, targetScale);
				}
		}
}
