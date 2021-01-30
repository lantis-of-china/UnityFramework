Shader "Lines/Colored Blended2"
{
	Properties
	{
		_AColor("Main Color", Color) = (0,1,1,0.5)
		_BackColor("BackColor",Color) = (0,1,1,0.5)
	}

		SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Opaque" }
		Pass
	{
		ZTest LEqual
		Blend SrcAlpha OneMinusSrcAlpha
		Material{ Diffuse[_AColor] }
		Lighting On
	}

		Pass
	{
		ZTest Greater
		Blend SrcAlpha OneMinusSrcAlpha
		Material{ Diffuse[_BackColor] } Lighting On
	}
	}
}