// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unlit shader. Simplest possible textured shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "NavMesh/Default" {
Properties
{
	//Range()
	_H ("色 相 H -180--180", Float) = 0
	_S ("饱和度S 0--3", Float) = 2
	_V ("明 度 V 0--3", Float) = 1
	_NormalValue ("法线强度 0--0.5", Float) = 0.1
	_NormalSeparate("法线明暗分开 0--3", Float) = 1.5
	_NormalIntensity("法线变暗增强强度 0--5",Float) = 2
	_MainTex ("Base (RGB)", 2D) = "white" {}
}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 100
	
	Pass 
	{  
		ZTest LEqual
		
		
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				half4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				half2 uv     : TEXCOORD1;
				half3 color : COLOR0; 
			};

			sampler2D _MainTex;
			half4 _MainTex_ST;
			half  _H;
			half  _S;
			half  _V;
			half _NormalValue;
			half _NormalSeparate;
			half _NormalIntensity;


			float3 HSV_col(float3 RGB, float3 shift)
			{
				float3 RESULT = float3(RGB);
				float VSU = shift.z*shift.y*cos(shift.x*3.14159265/180);
				float VSW = shift.z*shift.y*sin(shift.x*3.14159265/180);
               
				RESULT.x = (.299*shift.z+.701*VSU+.168*VSW)*RGB.x
					+ (.587*shift.z-.587*VSU+.330*VSW)*RGB.y
					+ (.114*shift.z-.114*VSU-.497*VSW)*RGB.z;
						               
				RESULT.y = (.299*shift.z-.299*VSU-.328*VSW)*RGB.x
					+ (.587*shift.z+.413*VSU+.035*VSW)*RGB.y
					+ (.114*shift.z-.114*VSU+.292*VSW)*RGB.z;   
			            
				RESULT.z = (.299*shift.z-.3*VSU+1.25*VSW)*RGB.x
					+ (.587*shift.z-.588*VSU-1.05*VSW)*RGB.y
					+ (.114*shift.z+.886*VSU-.203*VSW)*RGB.z;
               
				return (RESULT);
			}


			
			v2f vert (appdata_base  v)
			{
				v2f o;

				o.vertex = UnityObjectToClipPos(v.vertex);

				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

				o.color = v.normal;

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				half4 col = tex2D(_MainTex, i.texcoord);						

				half r = col.r-0.5;
				half g = col.g-0.5;
				half b = col.b-0.5;

				half colorValue = (col.r + col.g + col.b) - _NormalSeparate;

				half outValue = colorValue;				

				if(colorValue < 0)
				{
					outValue = colorValue / _NormalSeparate * _NormalIntensity;
				}

				col.rgb += _NormalValue * outValue;	
	
				col.rgb = HSV_col(col.rgb,float3(_H,_S,_V));

				return  col;
			}
		ENDCG
	}
	
}

}
