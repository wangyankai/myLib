using UnityEngine;
using System.Collections;

[ExecuteInEditMode,RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshImage : MonoBehaviour
{
	public float Width;
	public float shadoWidth;
//	public Vector3 StartPoint;
//	public Vector3 EndPint;
	public Color StartColor;
	public Color EndColor;
	public Vector3[] points;
	// Use this for initialization
	void Start ()
	{
//		MeshRectangle ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		MeshRectangle ();
	}

	void MeshRectangle ()
	{ 
		MeshFilter mFilter = gameObject.GetComponent<MeshFilter> ();  
		MeshRenderer mRen = gameObject.GetComponent<MeshRenderer> (); 
		Vector3[] vertices = new Vector3[(points.Length-1)*8];
		int[] triangles = new int[(points.Length-1)*24];
		Color[] colors = new Color[(points.Length-1)*8];

		Vector3[] normals = new Vector3[(points.Length-1)*8];
		Vector2[] uvs = new Vector2[(points.Length-1)*8];
		for (int i=0; i<points.Length-1; i++) {

			Vector2 k = new Vector2 (points[i+1].x - points[i].x, points[i+1].y - points[i].y);
		
			float lenth = k.x*k.x + k.y+k.y;
//			Debug.Log("k " + k.ToString()+ "   ");
			float angle = Vector2.Angle (Vector2.zero, k)*Mathf.PI/180f;
			float distan = Vector2.Distance(Vector2.zero, k);

			float width_x_half = (k.y/distan * Width / 2f);
			float width_y_half = (k.x/distan * Width / 2f);
			float s_x_half = (k.y/distan * (Width+shadoWidth )/ 2f);
			float s_y_half = (k.x/distan * (Width+shadoWidth ) / 2f);
//			Debug.Log("distan " + distan);
//			Debug.Log("distan " + angle);
//			Debug.Log("width_x_half " + width_x_half);
//			Debug.Log("width_y_half " + width_y_half);



			vertices [0+8*i] = new Vector3 (points[i].x - width_x_half, points[i].y + width_y_half, points[i].z);
			vertices [1+8*i] = new Vector3 (points[i].x + width_x_half, points[i].y - width_y_half, points[i].z);
			vertices [2+8*i] = new Vector3 (points[i+1].x + width_x_half, points[i+1].y - width_y_half, points[i+1].z);
			vertices [3+8*i] = new Vector3 (points[i+1].x - width_x_half, points[i+1].y + width_y_half, points[i+1].z);

			vertices [4+8*i] = new Vector3 (points[i].x - s_x_half, points[i].y + s_y_half, points[i].z);
			vertices [5+8*i] = new Vector3 (points[i].x + s_x_half, points[i].y - s_y_half, points[i].z);
			vertices [6+8*i] = new Vector3 (points[i+1].x + s_x_half, points[i+1].y - s_y_half, points[i+1].z);
			vertices [7+8*i] = new Vector3 (points[i+1].x - s_x_half, points[i+1].y + s_y_half, points[i+1].z);


			triangles[0+24*i] = 0+8*i;
			triangles[1+24*i] = 1+8*i;
			triangles[2+24*i] = 2+8*i;
			triangles[3+24*i] = 2+8*i;
			triangles[4+24*i] = 3+8*i;
			triangles[5+24*i] = 0+8*i;

			triangles[6+24*i] = 1+8*i;
			triangles[7+24*i] = 5+8*i;
			triangles[8+24*i] = 6+8*i;
			triangles[9+24*i] = 6+8*i;
			triangles[10+24*i] = 2+8*i;
			triangles[11+24*i] = 1+8*i;

			triangles[12+24*i] = 4+8*i;
			triangles[13+24*i] = 0+8*i;
			triangles[14+24*i] = 3+8*i;
			triangles[15+24*i] = 3+8*i;
			triangles[16+24*i] = 7+8*i;
			triangles[17+24*i] = 4+8*i;


			if(i>0){
				triangles[18+24*i] = 0+8*(i);
				triangles[19+24*i] = 1+8*(i);
				triangles[20+24*i] = 2+8*(i-1);
				triangles[21+24*i] = 2+8*(i-1);
				triangles[22+24*i] = 3+8*(i-1);
				triangles[23+24*i] = 0+8*(i);
			}else{
				triangles[18+24*i] = 0;
				triangles[19+24*i] = 0;
				triangles[20+24*i] = 0;
				triangles[21+24*i] = 0;
				triangles[22+24*i] = 0;
				triangles[23+24*i] = 0;
			}

			normals [0+8*i] = new Vector3 (0, 0, -5);
			normals [1+8*i] = new Vector3 (0, 0, -5);
			normals [2+8*i] = new Vector3 (0, 0, -5);
			normals [3+8*i] = new Vector3 (0, 0, -5); 
			normals [4+8*i] = new Vector3 (0, 0, -5);
			normals [5+8*i] = new Vector3 (0, 0, -5);
			normals [6+8*i] = new Vector3 (0, 0, -5);
			normals [7+8*i] = new Vector3 (0, 0, -5); 
	
			colors [0+8*i] = StartColor;
			colors [1+8*i] = StartColor;
			colors [2+8*i] = StartColor;
			colors [3+8*i] = StartColor;

			colors [4+8*i] = EndColor;
			colors [5+8*i] = EndColor;
			colors [6+8*i] = EndColor;
			colors [7+8*i] = EndColor;


			uvs [0+8*i] = new Vector2 (0, 0); 
			uvs [1+8*i] = new Vector2 (1, 0);
			uvs [2+8*i] = new Vector2 (1, 1); 
			uvs [3+8*i] = new Vector2 (0, 1);   
			uvs [4+8*i] = new Vector2 (0, 0); 
			uvs [5+8*i] = new Vector2 (1, 0);
			uvs [6+8*i] = new Vector2 (1, 1); 
			uvs [7+8*i] = new Vector2 (0, 1);   
		}




		Mesh mesh = new Mesh ();  
		mesh.hideFlags = HideFlags.DontSave;
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.colors = colors;
		mesh.uv = uvs; 
		mesh.normals = normals;
		mFilter.mesh = mesh;
	}


}
