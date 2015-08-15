using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UI.Utils
{
		public class ResizeBySetting : MonoBehaviour
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
						float targetScale = scale_16_9;
						if (Is_Screen_4X3 ()) {
								targetScale = scale_4_3;
						}
						if (targetScale != scale_16_9) {			
								transform.localScale = new Vector3 (targetScale, targetScale, targetScale);
						}
				}

				public static bool Is_Screen_4X3 ()
				{
						return Is_TargetScreen (4, 3);
				}

				public static bool Is_TargetScreen (int width, int height)
				{
						return Screen.width * height == Screen.height * width;
				}

				private float scale_16_9 = 0.928f;
				private float scale_4_3 = 0.75f;

		}
}
