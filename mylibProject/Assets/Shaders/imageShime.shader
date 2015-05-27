Shader "stalendp/imageShine" {
	Properties {
		_image ("image", 2D) = "white" {}
		_percent ("_percent", Range(-5, 5)) = 1
		_angle("angle", Range(0, 1)) = 0
	}
	
	CGINCLUDE
        #include "UnityCG.cginc"           
        
        sampler2D _image;
		float _percent;
		float _angle;
		
        struct v2f {    
            float4 pos:SV_POSITION;    
            float2 uv : TEXCOORD0;   
        };  
  
        v2f vert(appdata_base v) {  
            v2f o;  
            o.pos = mul (UNITY_MATRIX_MVP, v.vertex);  
            o.uv = v.texcoord.xy;  
            return o;  
        }  
  
        fixed4 frag(v2f i) : COLOR0 {
        	// 计算圆角
       		float2 uv = i.uv.xy - float2(0.5);    
            float rx = fmod(uv.x, 0.4);    
            float ry = fmod(uv.y, 0.4);    
            float mx = step(0.4, abs(uv.x));    
            float my = step(0.4, abs(uv.y));    
            float alpha = 1 - mx*my*step(0.1, length(half2(rx,ry)));    
        
        	fixed2x2 rotMat = fixed2x2(0.866,0.5,-0.5,0.866);  // 旋转矩阵，旋转30度
        	
       	 	fixed4 k = tex2D(_image, i.uv);
//       	 	k = fixed4(fixed3(k.r+k.g+k.b)/3, 1);  //灰度设置
			
			uv = i.uv - fixed2(0.5);
			_angle = 6.283*(_angle-0.5);
			float hui = (2-sign(_angle-atan2(uv.y, uv.x)))/3; // 百分比计算
			
			uv = (i.uv + fixed2(_percent)) * 2; // 缩放并位移
       	 	uv = mul(rotMat, uv); //旋转
       	 	
       	 	k +=  fixed4(saturate(lerp(fixed(1),fixed(0),abs(uv.y)))); // 加上光线
            k *= fixed4(fixed3(hui), alpha); // 圆角的运用
            return k;
        }  
    ENDCG    
  
    SubShader {   
        Tags {"Queue" = "Transparent"}     
        ZWrite Off     
        Blend SrcAlpha OneMinusSrcAlpha     
        Pass {    
            CGPROGRAM    
            #pragma vertex vert    
            #pragma fragment frag    
            #pragma fragmentoption ARB_precision_hint_fastest     
  
            ENDCG    
        }
    }
    FallBack Off  
}
