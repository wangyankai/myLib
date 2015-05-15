Shader "GAF/GAFMask"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_StencilID ("Stencil ID", Float) = 0
		_CustomColorMultiplier ("Color multiplier", Color) = (1,1,1,1)
		_CustomColorOffset("Color offset", Vector) = (0,0,0,0)
	}

	SubShader
	{
		Tags 
		{
			"Queue"="Transparent"
			"RenderType"="Transparent"
		}

		ColorMask 0
		ZWrite off
		Cull off
		
		Stencil
		{
			Ref [_StencilID]
			Comp Equal
			Pass IncrSat
			Fail Keep
		}

		Pass
		{
			CGPROGRAM 
			
			#include "GAFShaderBase.cginc"

			#pragma vertex gaf_minimal_vert  
			#pragma fragment gaf_mask_frag 
 
			ENDCG
		}
	}
}
