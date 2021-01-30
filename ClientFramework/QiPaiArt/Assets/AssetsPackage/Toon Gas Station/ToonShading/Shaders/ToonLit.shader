//Shader "Toon/Lit" 
//{
//	Properties 
//	{
//		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
//		_MainTex ("Base (RGB)", 2D) = "white" {}
//		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 
//	}
//
//	SubShader 
//	{
//		Tags 
//		{ 
//			"RenderPipeline" = "UniversalPipeline"
//			"RenderType"="Opaque" 
//		}
//		LOD 200
//
//		Pass
//		{
//			Name "Pass"
//			Tags
//			{
//				//"LightMode" = ""
//			}
//
//			HLSLINCLUDE
//			#pragma surface surf ToonRamp
//			#pragma lighting ToonRamp exclude_path:prepass
//			//#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
//			//#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
//			//#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
//			//#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
//			//#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"
//			sampler2D _Ramp;
//			sampler2D _MainTex;
//			float4 _Color;
//
//			struct Input 
//			{
//				float2 uv_MainTex : TEXCOORD0;
//			};
//
//			inline half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half atten)
//			{
//				#ifndef USING_DIRECTIONAL_LIGHT
//				lightDir = normalize(lightDir);
//				#endif
//
//				half d = dot(s.Normal, lightDir) * 0.5 + 0.5;
//				half3 ramp = tex2D(_Ramp, float2(d,d)).rgb;
//
//				half4 c;
//				c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
//				c.a = 0;
//				return c;
//			}
//
//			void surf(Input IN, inout SurfaceOutput o) 
//			{
//				half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
//				o.Albedo = c.rgb;
//				o.Alpha = c.a;
//			}
//			ENDHLSL
//		}
//	} 
//
//	Fallback "Diffuse"
//}

Shader  "Toon/Lit" 
{
    Properties
    {
        _Color("Color(RGB)",Color) = (1,1,1,1)
        _MainTex("MainTex",2D) = "gary"{}
    }
    SubShader
    {
        Tags
        {
        // ��Ⱦ���߱�ǣ���Ӧ�Ĺ���C#����UniversalRenderPipeline.cs�е�
        // Shader.globalRenderPipeline = UniversalPipeline,LightweightPipeline,
        // ֻ�д���UniversalPipeline��LightweightPipeline��Tag��SubShader�Ż���Ч.
        // ��Ҫ���������ڱ�ǵ�ǰ���SubShader�������ĸ������µ�.
        "RenderPipeline" = "UniversalPipeline"
        "RenderType" = "Opaque"
        //"Queue" = "Geometry+0"
        }

    Pass
    {
        Name "Pass"
        Tags
        {

        }

        // Render State
        Blend One Zero, One Zero
        Cull Back
        ZTest LEqual
        ZWrite On
        //��OpenGL ES2.0��ʹ��HLSLcc������,Ŀǰ����OpenGL ES2.0ȫ��Ĭ��ʹ��HLSLcc������.
        HLSLPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma prefer_hlslcc gles
        #pragma exclude_renderers d3d11_9x
        #pragma target 2.0
        #pragma multi_compile_instancing

        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"

        // �����������Ķ��壬GPU��������ĳһ��������һ��������Էǳ����ٵĺ�GPU��������
        // ��ΪҪռGPU�Դ棬�������������Ǻܴ�
        // CBUFFER_START = �����������Ŀ�ʼ��CBUFFER_END = �����������Ľ�����
        // UnityPerMaterial = ÿһ������������һ��Cbuffer��������Properties���涨�������(Texture����)��
        // ����Ҫ�ڳ����������������������Ҷ�����һ��Cbuffer��ͨ����Щ�����������ܵ�SRP�ĺ�������
        CBUFFER_START(UnityPerMaterial)
        // ��������������������
        half4 _Color;
        CBUFFER_END

            // ������������ķ��붨��:
            // ������������ֿ�����ĺô���ʲô��
            // ���ù��ߵ�����Ķ����ǺͲ������Ķ���󶨵ģ��������Ķ����������Ƶġ�
            // ��URP������Ķ���Ͳ������Ķ����Ƿ���ģ����������˺ܶ���������Զ�����һ��������
            TEXTURE2D(_MainTex);//����Ķ��壬����Ǳ��뵽GLES2.0ƽ̨���൱��samler2D _MainTex;����TEXTURE2D _MainTex
            float4 _MainTex_ST;
            // �������Ķ���(��������������붨��),��������ָ����Ĺ���ģʽ���ظ�ģʽ,�˹�����OpenGL ES2.0�ϲ�֧�֣��൱��ûд.
            // 1.SAMPLER(sampler_textureName):sampler+�������ƣ����ֶ�����ʽ�Ǳ�ʾ����textureName�������Inspector����еĲ�����ʽ.
            // 2.SAMPLER(_filter_wrap):����SAMPLER(point_clamp),ʹ���Զ���Ĳ��������ã��Զ���Ĳ�����һ��Ҫͬʱ��������ģʽ<filter>���ظ�ģʽ<wrap>������.
            // 3.SAMPLER(_filter_wrapU_wrapV):����SAMPLER(linear_clampU_mirrorV),��ͬʱ�����ظ�ģʽ��U��V�Ĳ�ֵͬ.
            // 4.filter:point/linear/triLinear
            // 5.wrap:clamp/repeat/mirror/mirrorOnce

            //Ϊ�˷������ ����Ԥ����
            #define smp SamplerState_Point_Repeat
            // SAMPLER(sampler_MainTex); Ĭ�ϲ�����
            SAMPLER(smp);
            // ������ɫ��������
            struct Attributes
            {
                float3 positionOS : POSITION;
                float2 uv :TEXCOORD0;
            };

            // ������ɫ�������
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv :TEXCOORD0;

            };

            // ������ɫ��
            Varyings vert(Attributes v)
            {
                Varyings o = (Varyings)0;
                o.uv = TRANSFORM_TEX(v.uv,_MainTex);
                o.positionCS = TransformObjectToHClip(v.positionOS);
                return o;
            }

            // Ƭ����ɫ��
            half4 frag(Varyings i) : SV_TARGET
            {
                //����������� SAMPLE_TEXTURE2D(������������������uv)
                half4 mainTex = SAMPLE_TEXTURE2D(_MainTex,smp,i.uv);
                half4 c = _Color * mainTex;
                return c;
            }

            ENDHLSL
        }
    }
        FallBack "Hidden/Shader Graph/FallbackError"
}
