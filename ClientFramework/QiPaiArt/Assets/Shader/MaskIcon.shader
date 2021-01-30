// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ImageEffect/MaskIcon"
{ 
Properties 
{ 
[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
_Mask ("Base (RGB)", 2D) = "white" {} 


_Color ("Tint", Color) = (1,1,1,1)
}

SubShader 
{
Tags
{ 
"Queue"="Transparent"
"IgnoreProjector"="True"
"RenderType"="Transparent"
}

Cull Off
Lighting Off
ZWrite Off
Blend SrcAlpha OneMinusSrcAlpha

Pass
{ 
CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
//#include "UnityUI.cginc"

//#pragma multi_compile __ UNITY_UI_ALPHACLIP

struct a2v
{
fixed2 uv : TEXCOORD0;
half4 vertex : POSITION;
float4 color : COLOR;
};

fixed4 _Color;

struct v2f
{
fixed2 uv : TEXCOORD0;
half4 vertex : SV_POSITION;
float4 color : COLOR;
};

sampler2D _MainTex;
sampler2D _Mask; 

v2f vert (a2v i)
{
v2f o;
o.vertex = UnityObjectToClipPos(i.vertex);
o.uv = i.uv;

o.color = i.color * _Color;
return o;
}

fixed4 frag (v2f i) : COLOR
{
half4 color = tex2D(_MainTex, i.uv) * i.color; 
half4 mask = tex2D(_Mask, i.uv); 
color.a *= mask.a;
return color;
}
ENDCG
} 
} 
}