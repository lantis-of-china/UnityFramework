// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/MobileURPBloom" {

    Properties{  
        _MainTex("Base (RGB)", 2D) = "white" {}  
        _BlurTex("Blur", 2D) = "white"{}		
    }  

    HLSLINCLUDE
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
    #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"

    struct appdata_img
    {
        float4 vertex : POSITION;
        half2 texcoord : TEXCOORD0;
    };

    struct appdata_base 
    {
        float4 vertex : POSITION;//顶点位置
        float3 normal : NORMAL;//发现
        float4 texcoord : TEXCOORD0;//纹理坐标
        UNITY_VERTEX_INPUT_INSTANCE_ID
    };

    struct v2f_threshold  
    {  
        float4 pos : SV_POSITION;  
        float2 uv : TEXCOORD0;  
    };  

    struct v2f_blur  
    {  
        float4 pos : SV_POSITION;  
        float2 uv  : TEXCOORD0;  
        float4 uv01 : TEXCOORD1;  
        float4 uv23 : TEXCOORD2;  
        float4 uv45 : TEXCOORD3;  
    };  

    struct v2f_bloom  
    {  
        float4 pos : SV_POSITION;  
        float2 uv  : TEXCOORD0;  
        float2 uv2 : TEXCOORD1;  
    };  


	struct v2f_hsv {
		half4 vertex : SV_POSITION;
		half2 texcoord : TEXCOORD0;
		half2 uv     : TEXCOORD1;
		half3 color : COLOR0;
	};

    sampler2D _MainTex;  
    float4 _MainTex_TexelSize;  
    sampler2D _BlurTex;  
    float4 _BlurTex_TexelSize;  
    float4 _offsets;  
    float4 _colorThreshold;  
    float4 _bloomColor;  
    float _bloomFactor;

	int _roundOpen;
	float4 _colorRound;
	float2 _senceSizeHalf;
	float2 _roundCenter;
	float _roundIndeisty;
	float _attenuation;

	half4 _MainTex_ST;
	float _H;
	float _S;
	float _V;
	half _NormalValue;
	half _NormalSeparate;
	half _NormalIntensity;


	float3 HSV_col(float3 RGB, float3 shift)
	{
		float3 RESULT = float3(RGB);
		float VSU = shift.z*shift.y*cos(shift.x*3.14159265 / 180);
		float VSW = shift.z*shift.y*sin(shift.x*3.14159265 / 180);

		RESULT.x = (.299*shift.z + .701*VSU + .168*VSW)*RGB.x
			+ (.587*shift.z - .587*VSU + .330*VSW)*RGB.y
			+ (.114*shift.z - .114*VSU - .497*VSW)*RGB.z;

		RESULT.y = (.299*shift.z - .299*VSU - .328*VSW)*RGB.x
			+ (.587*shift.z + .413*VSU + .035*VSW)*RGB.y
			+ (.114*shift.z - .114*VSU + .292*VSW)*RGB.z;

		RESULT.z = (.299*shift.z - .3*VSU + 1.25*VSW)*RGB.x
			+ (.587*shift.z - .588*VSU - 1.05*VSW)*RGB.y
			+ (.114*shift.z + .886*VSU - .203*VSW)*RGB.z;

		return (RESULT);
	}


	float4 frag_round(float2 pos, float4 color) : SV_Target
	{
		float widhtScale = abs(pos.x - _roundCenter.x) / _senceSizeHalf.x;
		float heightScale = abs(pos.y - _roundCenter.y) / _senceSizeHalf.y;
		float dis = distance(float2(widhtScale, heightScale), float2(1, 1));
		float value = dis - _roundIndeisty;
		if (value > 0)
		{
			value *= _attenuation;
			color -= float4(value *  _colorRound.r, value * _colorRound.g, value*_colorRound.b, value);
			return color;
		}
		return color;
	}

    v2f_threshold vert_threshold(appdata_img v)  
    {  
        v2f_threshold o;  
        o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
        o.uv = v.texcoord.xy;  
#if UNITY_UV_STARTS_AT_TOP  
        if (_MainTex_TexelSize.y < 0)  
            o.uv.y =1- o.uv.y;  
        else
            o.uv.y = o.uv.y;  
#endif    
        return o;  
    }  

    float4 frag_threshold(v2f_threshold i) : SV_Target
    {  
        float4 color = tex2D(_MainTex, i.uv);
        return saturate(color - _colorThreshold);  
    }

    v2f_blur vert_blur(appdata_img v)  
    {  
        v2f_blur o;  
        _offsets *= _MainTex_TexelSize.xyxy;  
        o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
        o.uv = v.texcoord.xy;  

        o.uv01 = v.texcoord.xyxy + _offsets.xyxy * float4(1, 1, -1, -1);  
        o.uv23 = v.texcoord.xyxy + _offsets.xyxy * float4(1, 1, -1, -1) * 2.0;  
        o.uv45 = v.texcoord.xyxy + _offsets.xyxy * float4(1, 1, -1, -1) * 3.0;  

        return o;  
    }  

    float4 frag_blur(v2f_blur i) : SV_Target
    {  
        float4 color = float4(0,0,0,0);
        color += 0.40 * tex2D(_MainTex, i.uv);  
        color += 0.15 * tex2D(_MainTex, i.uv01.xy);  
        color += 0.15 * tex2D(_MainTex, i.uv01.zw);  
        color += 0.10 * tex2D(_MainTex, i.uv23.xy);  
        color += 0.10 * tex2D(_MainTex, i.uv23.zw);  
        color += 0.05 * tex2D(_MainTex, i.uv45.xy);  
        color += 0.05 * tex2D(_MainTex, i.uv45.zw);  
        return color;  
    }  

    v2f_bloom vert_bloom(appdata_img v)  
    {  
        v2f_bloom o;  
        o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		//o.uv = UnityStereoScreenSpaceUVAdjust(v.texcoord, _MainTex_ST);
        o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
#if UNITY_UV_STARTS_AT_TOP 
		o.uv2.xy = o.uv.xy;
        if (_MainTex_TexelSize.y < 0)  
            o.uv.y = 1 - o.uv.y;  
        else
            o.uv.y = o.uv.y;  
#endif    
        return o;  
    }  

    float4 frag_bloom(v2f_bloom i) : SV_Target
    {  
        float4 ori = tex2D(_MainTex, i.uv2);
        float4 blur = tex2D(_BlurTex, i.uv);
        float4 final = ori + _bloomFactor * blur * _bloomColor;

		if (_roundOpen == 0)
		{
			return final;
		}

        return frag_round(i.pos,final);
    }


	v2f_hsv vert_hsv(appdata_base  v)
	{
		v2f_hsv o;
        o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
		o.color = v.normal;

		return o;
	}

    float4 frag_hsv(v2f_hsv i) : SV_Target
	{
		half4 col = tex2D(_MainTex, i.texcoord);

		half r = col.r - 0.5;
		half g = col.g - 0.5;
		half b = col.b - 0.5;

		half colorValue = (col.r + col.g + col.b) - _NormalSeparate;

		half outValue = colorValue;

		if (colorValue < 0)
		{
			outValue = colorValue / _NormalSeparate * _NormalIntensity;
		}

		col.rgb += _NormalValue * outValue;

		col.rgb = HSV_col(col.rgb,float3(_H,_S,_V));

		return  col;
	}

    float4 frag_hsv_2(v2f_hsv i) : SV_Target
	{
		half4 col = tex2D(_MainTex, i.texcoord);
		half4 blur = tex2D(_BlurTex, i.texcoord);
		half r = col.r - 0.5;
		half g = col.g - 0.5;
		half b = col.b - 0.5;

		half colorValue = (col.r + col.g + col.b) - _NormalSeparate;

		half outValue = colorValue;

		if (colorValue < 0)
		{
			outValue = colorValue / _NormalSeparate * _NormalIntensity;
		}

		col.rgb += _NormalValue * outValue;

		col.rgb = HSV_col(col.rgb,float3(_H,_S,_V));

		return  frag_round(i.vertex,col + blur);
	}
    ENDHLSL

    SubShader  
    {  
        Pass  
        {  
            ZTest Off  
            Cull Off  
            ZWrite Off  
            Fog{ Mode Off }  

            HLSLPROGRAM
            #pragma vertex vert_threshold  
            #pragma fragment frag_threshold  
            ENDHLSL
        }  

        Pass  
        {  
            ZTest Off  
            Cull Off  
            ZWrite Off  
            Fog{ Mode Off }  

            HLSLPROGRAM
            #pragma vertex vert_blur  
            #pragma fragment frag_blur  
            ENDHLSL
        }  

        Pass  
        {  

            ZTest Off  
            Cull Off  
            ZWrite Off  
            Fog{ Mode Off }  

            HLSLPROGRAM
            #pragma vertex vert_bloom  
            #pragma fragment frag_bloom  
            ENDHLSL
        }

		Pass
		{

			ZTest Off
			Cull Off
			ZWrite Off
			Fog{ Mode Off }

            HLSLPROGRAM
			#pragma vertex vert_hsv  
			#pragma fragment frag_hsv
            ENDHLSL
		}

		Pass
		{

			ZTest Off
			Cull Off
			ZWrite Off
			Fog{ Mode Off }

            HLSLPROGRAM
#pragma vertex vert_hsv  
#pragma fragment frag_hsv_2
			ENDHLSL
		}
    }  
}