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
			
			fixed4 frag (v2f i) : SV_Target
			{
				//八个方向颜色,(1,1)是当前像素
				
				fixed4 col0_0 = tex2D(_MainTex, half2(i.texcoord.x - 2.0f/640.0f, i.texcoord.y - 2.0f/640.0f));
				fixed4 col1_0 = tex2D(_MainTex, half2(i.texcoord.x, i.texcoord.y - 2.0f/640.0f));
				fixed4 col2_0 = tex2D(_MainTex, half2(i.texcoord.x + 2.0f/640.0f, i.texcoord.y - 2.0f/640.0f));
				fixed4 col0_1 = tex2D(_MainTex, half2(i.texcoord.x - 2.0f/640.0f, i.texcoord.y));
				fixed4 col1_1 = tex2D(_MainTex, half2(i.texcoord.x, i.texcoord.y));
				fixed4 col2_1 = tex2D(_MainTex, half2(i.texcoord.x + 2.0f/640.0f, i.texcoord.y));
				fixed4 col0_2 = tex2D(_MainTex, half2(i.texcoord.x - 2.0f/640.0f, i.texcoord.y + 2.0f/640.0f));
				fixed4 col1_2 = tex2D(_MainTex, half2(i.texcoord.x, i.texcoord.y + 2.0f/640.0f));
				fixed4 col2_2 = tex2D(_MainTex, half2(i.texcoord.x + 2.0f/640.0f, i.texcoord.y + 2.0f/640.0f));

				fixed4 col = col0_0 + col1_0 + col2_0 + col0_1 + col2_1 + col0_2 + col1_2 + col2_2;
				col = col/8.0f;
				return col;
				//边界查找

			}
		ENDCG
	}
}
	FallBack "Diffuse"
}
