using UnityEngine;
using System.Collections;

public class SkyGameCamera : MonoBehaviour
{
	
		public float devHeight = 50.4f;
		public float devWidth = 96f;
		public bool debug = false;
	    
		// Use this for initialization
		void Start ()
		{
		
				resetSize ();
		
		}
	
		// Update is called once per frame
		void Update ()
		{
		  if (debug) {
			resetSize ();
		}
		}

		private void resetSize ()
		{
				float screenHeight = Screen.height;
		
				Debug.Log ("screenHeight = " + screenHeight + "   screenWidth = " + Screen.width);

				float orthographicSize = this.GetComponent<Camera> ().orthographicSize;
		
				Debug.Log ("Old orthographicSize = " + orthographicSize);
		
				float aspectRatio = Screen.width * 1.0f / Screen.height;
		
				float cameraWidth = orthographicSize * 2 * aspectRatio;
		
				Debug.Log ("cameraWidth = " + cameraWidth);
		
				if (cameraWidth < devWidth) {
						orthographicSize = devWidth / (2 * aspectRatio);
						Debug.Log ("new DevWidth orthographicSize = " + orthographicSize);
						this.GetComponent<Camera> ().orthographicSize = orthographicSize;
				}
		}
}
