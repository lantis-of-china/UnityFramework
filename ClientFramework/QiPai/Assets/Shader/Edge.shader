// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Edge" {
	Properties {
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}//主相机的纹理
	}


	SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 100
	
	Cull Off
	Blend Off
	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			float lookup(fixed2 p, float dx, float dy)
			{
				fixed2 uv = p.xy + fixed2(dx , dy ) / fixed2(640, 640);
				fixed4 c = tex2D(_MainTex, uv.xy);
				return (c.r + c.g + c.b)/3;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed2 p = i.texcoord.xy;
				float gx = 0.0;
				gx += -1.0 * lookup(p, -1.0, -1.0);        //相当于右边的像素灰度 - 左边的像素灰度
				gx += -2.0 * lookup(p, -1.0,  0.0);
				gx += -1.0 * lookup(p, -1.0,  1.0);
				gx +=  1.0 * lookup(p,  1.0, -1.0);
				gx +=  2.0 * lookup(p,  1.0,  0.0);
				gx +=  1.0 * lookup(p,  1.0,  1.0);
				
				float gy = 0.0;							
				gy += -1.0 * lookup(p, -1.0, -1.0);			//相当于上边的像素灰度 - 下边的像素灰度
				gy += -2.0 * lookup(p,  0.0, -1.0);
				gy += -1.0 * lookup(p,  1.0, -1.0);
				gy +=  1.0 * lookup(p, -1.0,  1.0);
				gy +=  2.0 * lookup(p,  0.0,  1.0);
				gy +=  1.0 * lookup(p,  1.0,  1.0);

				float g = 1.0f - gx*gx - gy*gy;
				//g = floor(g + 0.1);
    
				fixed4 col = fixed4(g, g, g, 1.0f);

				//原色
				fixed4 c = tex2D(_MainTex, i.texcoord.xy);

				//将原色与边界混合
				//col = c*col.r;




				return col;

			}
		ENDCG
	}
}
	FallBack "Diffuse"
}