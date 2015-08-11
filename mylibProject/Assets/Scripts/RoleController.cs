using UnityEngine;
using System.Collections;

public class RoleController : MonoBehaviour 
{
	private SkinnedMeshRenderer skinnedMeshRenderer;

	private bool shiftStatus;

	void Awake()
	{
		skinnedMeshRenderer = this.GetComponentInChildren<SkinnedMeshRenderer> ();
		MaterialController.SetOutlineMaterial(skinnedMeshRenderer, Random.Range(1, 4));
	}

	void Update()
	{
//		this.shiftStatus = false;
//
//		if (Input.GetKey (KeyCode.LeftShift)) 
//		{
//			this.shiftStatus = true;
//		}
//
//		if(Input.GetMouseButtonDown(0) && !shiftStatus)
//		{
//			if(this.skinnedMeshRenderer == null) return;
//			// 设置灰度材质
//			MaterialController.SetGrayMaterial(skinnedMeshRenderer);
//		}
//
//		if(Input.GetMouseButtonUp(0) && !shiftStatus)
//		{
//			if(this.skinnedMeshRenderer == null) return;
//			// 取消设置灰度材质
//			MaterialController.CancelSetGrayMaterial(skinnedMeshRenderer);
//		}
//
//		if (Input.GetMouseButtonDown (1)) 
//		{
//			if(this.skinnedMeshRenderer == null) return;
//			// 设置发光材质
//
//			Hashtable shaderParams = new Hashtable();
//			shaderParams.Add("_InnerColor", Color.yellow);
//			shaderParams.Add("_RimColor", Color.yellow);
//			shaderParams.Add("_InnerColorPower", 0.9f);
//			shaderParams.Add("_RimPower", 4f);
//
//			MaterialController.SetLightMaterial(skinnedMeshRenderer, Random.Range(1, 3), shaderParams);
//		}
//		
//		if (Input.GetMouseButtonUp (1)) 
//		{
//			if(this.skinnedMeshRenderer == null) return;
//			// 取消设置发光材质
//			MaterialController.CancelSetLightMaterial(skinnedMeshRenderer);
//		}
//		
//		if (Input.GetMouseButtonDown (0) && shiftStatus) 
//		{
//			if(this.skinnedMeshRenderer == null) return;
//			// 设置边缘材质
//			MaterialController.SetOutlineMaterial(skinnedMeshRenderer, Random.Range(1, 4));
//		}
//		
//		if (Input.GetMouseButtonUp (0) && shiftStatus) 
//		{
//			if(this.skinnedMeshRenderer == null) return;
//			// 取消设置边缘材质
//			MaterialController.CancelSetOutlineMaterial(skinnedMeshRenderer);
//		}
	}
}
