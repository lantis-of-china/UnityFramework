Shader "Unlit/ZTextShader"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" "LightMode"="LightweightForward" }
        LOD 100

        Pass
        {
            ZTest greater
            offset -1,-1

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/core.hlsl"
   

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                return float4(1,0,0,1);
            }
            ENDHLSL
        }
    }
}