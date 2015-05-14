using UnityEngine;
using System.Collections;

public class SkyUtil {

	public static Vector3 reletiveToLocal(Vector3 relative,float width,float hight){
		return new Vector3 (width *(relative.x - 0.5f), hight*(relative.y-0.5f), 0);
	}
}
