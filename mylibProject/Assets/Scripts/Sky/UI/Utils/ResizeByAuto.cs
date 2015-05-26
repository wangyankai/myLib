using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UI.Utils
{
	[ExecuteInEditMode]
	public class ResizeByAuto : MonoBehaviour
	{
		public float devHeight = 3f;
		public float devWidth = 4f;
		private float  currentRatio = 3f / 4;
		public Vector3 initScale;
		// Use this for initialization
		void Start ()
		{
			Onchanged ();
		}
	
		#if UNITY_EDITOR
		// Update is called once per frame
		void Update () {
					if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode) {
//						this.enabled = false;
					} else {
						Onchanged();
					}
		}
		#else
		#endif

		public void Onchanged ()
		{				
			float temp = (Screen.width * 1f) / Screen.height;
			if (temp != currentRatio) {
				currentRatio = temp;
				float targetScale = initScale.x * devHeight * currentRatio / devWidth;
				transform.localScale = new Vector3 (targetScale, targetScale, targetScale);
			}
		}
	}
}
