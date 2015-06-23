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
	public bool shadowOn = true;
	public bool halfOfBase = true;
	private int VerticesBase = 8;
	private int VerticesNumber;
	private int TrianglesBase = 24;
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

		if (halfOfBase) {
			EndColor = new Color (StartColor.r / 2f, StartColor.g / 2f, StartColor.b / 2f, 0.5f);
		}
		MeshFilter mFilter = gameObject.GetComponent<MeshFilter> ();  
		MeshRenderer mRen = gameObject.GetComponent<MeshRenderer> (); 

		if (shadowOn) {
			VerticesBase = 8;
			TrianglesBase = 36;
		} else {
			VerticesBase = 4;
			TrianglesBase = 12;
		}

		VerticesNumber = (points.Length - 1) * VerticesBase;
		Vector3[] vertices = new Vector3[VerticesNumber];
		int[] triangles = new int[(points.Length - 1) * TrianglesBase];
		Color[] colors = new Color[VerticesNumber];

		Vector3[] normals = new Vector3[VerticesNumber];
		Vector2[] uvs = new Vector2[VerticesNumber];
		for (int i=0; i<points.Length-1; i++) {
			int offset = VerticesBase * i;
			int offsetTriangles = TrianglesBase * i;
			Vector2 k = new Vector2 (points [i + 1].x - points [i].x, points [i + 1].y - points [i].y);
		
			float lenth = k.x * k.x + k.y + k.y;
//			Debug.Log("k " + k.ToString()+ "   ");
			float angle = Vector2.Angle (Vector2.zero, k) * Mathf.PI / 180f;
			float distan = Vector2.Distance (Vector2.zero, k);

			float width_x_half = (k.y / distan * Width / 2f);
			float width_y_half = (k.x / distan * Width / 2f);
			float s_x_half = (k.y / distan * (Width + shadoWidth) / 2f);
			float s_y_half = (k.x / distan * (Width + shadoWidth) / 2f);
//			Debug.Log("distan " + distan);
//			Debug.Log("distan " + angle);
//			Debug.Log("width_x_half " + width_x_half);
//			Debug.Log("width_y_half " + width_y_half);



			vertices [0 + offset] = new Vector3 (points [i].x - width_x_half, points [i].y + width_y_half, points [i].z);
			vertices [1 + offset] = new Vector3 (points [i].x + width_x_half, points [i].y - width_y_half, points [i].z);
			vertices [2 + offset] = new Vector3 (points [i + 1].x + width_x_half, points [i + 1].y - width_y_half, points [i + 1].z);
			vertices [3 + offset] = new Vector3 (points [i + 1].x - width_x_half, points [i + 1].y + width_y_half, points [i + 1].z);


			triangles [0 + offsetTriangles] = 0 + offset;
			triangles [1 + offsetTriangles] = 1 + offset;
			triangles [2 + offsetTriangles] = 2 + offset;
			triangles [3 + offsetTriangles] = 2 + offset;
			triangles [4 + offsetTriangles] = 3 + offset;
			triangles [5 + offsetTriangles] = 0 + offset;

			
			if (i > 0) {
				triangles [6 + offsetTriangles] = 0 + offset;
				triangles [7 + offsetTriangles] = 1 + offset;
				triangles [8 + offsetTriangles] = 2 + (offset - VerticesBase);
				triangles [9 + offsetTriangles] = 2 + (offset - VerticesBase);
				triangles [10 + offsetTriangles] = 3 + (offset - VerticesBase);
				triangles [11 + offsetTriangles] = 0 + offset;
			} else {
				triangles [6 + offsetTriangles] = 0;
				triangles [7 + offsetTriangles] = 0;
				triangles [8 + offsetTriangles] = 0;
				triangles [9 + offsetTriangles] = 0;
				triangles [10 + offsetTriangles] = 0;
				triangles [11 + offsetTriangles] = 0;
			}

			normals [0 + offset] = new Vector3 (0, 0, -5);
			normals [1 + offset] = new Vector3 (0, 0, -5);
			normals [2 + offset] = new Vector3 (0, 0, -5);
			normals [3 + offset] = new Vector3 (0, 0, -5); 

			colors [0 + offset] = StartColor;
			colors [1 + offset] = StartColor;
			colors [2 + offset] = StartColor;
			colors [3 + offset] = StartColor;

			uvs [0 + offset] = new Vector2 (0, 0); 
			uvs [1 + offset] = new Vector2 (1, 0);
			uvs [2 + offset] = new Vector2 (1, 1); 
			uvs [3 + offset] = new Vector2 (0, 1);   


			if (shadowOn) {
				vertices [4 + offset] = new Vector3 (points [i].x - s_x_half, points [i].y + s_y_half, points [i].z);
				vertices [5 + offset] = new Vector3 (points [i].x + s_x_half, points [i].y - s_y_half, points [i].z);
				vertices [6 + offset] = new Vector3 (points [i + 1].x + s_x_half, points [i + 1].y - s_y_half, points [i + 1].z);
				vertices [7 + offset] = new Vector3 (points [i + 1].x - s_x_half, points [i + 1].y + s_y_half, points [i + 1].z);


	

				triangles [12 + offsetTriangles] = 1 + offset;
				triangles [13 + offsetTriangles] = 5 + offset;
				triangles [14 + offsetTriangles] = 6 + offset;
				triangles [15 + offsetTriangles] = 6 + offset;
				triangles [16 + offsetTriangles] = 2 + offset;
				triangles [17 + offsetTriangles] = 1 + offset;

				triangles [18 + offsetTriangles] = 4 + offset;
				triangles [19 + offsetTriangles] = 0 + offset;
				triangles [20 + offsetTriangles] = 3 + offset;
				triangles [21 + offsetTriangles] = 3 + offset;
				triangles [22 + offsetTriangles] = 7 + offset;
				triangles [23 + offsetTriangles] = 4 + offset;


				if (i > 0) {
					triangles [24 + offsetTriangles] = 5 + offset;
					triangles [25 + offsetTriangles] = 1 + offset;
					triangles [26 + offsetTriangles] = 2 + (offset - VerticesBase);
					triangles [27 + offsetTriangles] = 2 + (offset - VerticesBase);
					triangles [28 + offsetTriangles] = 6 + (offset - VerticesBase);
					triangles [29 + offsetTriangles] = 5 + offset;

					triangles [30 + offsetTriangles] = 0 + offset;
					triangles [31 + offsetTriangles] = 4 + offset;
					triangles [32 + offsetTriangles] = 7 + (offset - VerticesBase);
					triangles [33 + offsetTriangles] = 7 + (offset - VerticesBase);
					triangles [34 + offsetTriangles] = 3 + (offset - VerticesBase);
					triangles [35 + offsetTriangles] = 0 + offset;
				} else {
					triangles [24 + offsetTriangles] = 0;
					triangles [25 + offsetTriangles] = 0;
					triangles [26 + offsetTriangles] = 0;
					triangles [27 + offsetTriangles] = 0;
					triangles [28 + offsetTriangles] = 0;
					triangles [29 + offsetTriangles] = 0;

					triangles [30 + offsetTriangles] = 0;
					triangles [31 + offsetTriangles] = 0;
					triangles [32 + offsetTriangles] = 0;
					triangles [33 + offsetTriangles] = 0;
					triangles [34 + offsetTriangles] = 0;
					triangles [35 + offsetTriangles] = 0;
				}
		

		
				normals [4 + offset] = new Vector3 (0, 0, -5);
				normals [5 + offset] = new Vector3 (0, 0, -5);
				normals [6 + offset] = new Vector3 (0, 0, -5);
				normals [7 + offset] = new Vector3 (0, 0, -5); 
	


				colors [4 + offset] = EndColor;
				colors [5 + offset] = EndColor;
				colors [6 + offset] = EndColor;
				colors [7 + offset] = EndColor;


		
				uvs [4 + offset] = new Vector2 (0, 0); 
				uvs [5 + offset] = new Vector2 (1, 0);
				uvs [6 + offset] = new Vector2 (1, 1); 
				uvs [7 + offset] = new Vector2 (0, 1);  
			}
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
