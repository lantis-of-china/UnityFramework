// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33590,y:33057,varname:node_9361,prsc:2|alpha-2790-OUT,refract-6099-OUT;n:type:ShaderForge.SFN_Tex2d,id:1817,x:32412,y:33276,ptovrint:False,ptlb:node_1817,ptin:_node_1817,varname:_node_1817,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Append,id:9745,x:32743,y:33264,varname:node_9745,prsc:2|A-1817-R,B-1817-G;n:type:ShaderForge.SFN_Multiply,id:4446,x:33009,y:33417,varname:node_4446,prsc:2|A-9745-OUT,B-1817-A,C-62-A;n:type:ShaderForge.SFN_VertexColor,id:62,x:32696,y:33736,varname:node_62,prsc:2;n:type:ShaderForge.SFN_Slider,id:3029,x:32397,y:33634,ptovrint:False,ptlb:node_3029,ptin:_node_3029,varname:_node_3029,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5395398,max:1;n:type:ShaderForge.SFN_Multiply,id:6099,x:33052,y:33652,varname:node_6099,prsc:2|A-4446-OUT,B-3029-OUT;n:type:ShaderForge.SFN_Vector1,id:2790,x:33009,y:33234,varname:node_2790,prsc:2,v1:0;proporder:1817-3029;pass:END;sub:END;*/

Shader "cgwell/niuqu" {
    Properties {
        _node_1817 ("node_1817", 2D) = "white" {}
        _node_3029 ("node_3029", Range(0, 1)) = 0.5395398
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            //#pragma only_renderers d3d9 d3d11 glcore gles 
            //#pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _node_1817; uniform float4 _node_1817_ST;
            uniform float _node_3029;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 _node_1817_var = tex2D(_node_1817,TRANSFORM_TEX(i.uv0, _node_1817));
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + ((float2(_node_1817_var.r,_node_1817_var.g)*_node_1817_var.a*i.vertexColor.a)*_node_3029);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
                float3 finalColor = 0;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,0.0),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
