// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33734,y:32671,varname:node_3138,prsc:2|emission-1625-OUT;n:type:ShaderForge.SFN_TexCoord,id:8707,x:32485,y:32912,varname:node_8707,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_TexCoord,id:8997,x:32430,y:33135,varname:node_8997,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Lerp,id:2733,x:32743,y:32844,varname:node_2733,prsc:2|A-4895-OUT,B-8707-UVOUT,T-8997-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:867,x:32381,y:32585,ptovrint:False,ptlb:xz_wenli,ptin:_xz_wenli,varname:node_867,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:697babe0ff89e7d4b93e57125ebc2255,ntxv:0,isnm:False|UVIN-9236-UVOUT;n:type:ShaderForge.SFN_Append,id:4895,x:32698,y:32603,varname:node_4895,prsc:2|A-867-R,B-867-G;n:type:ShaderForge.SFN_Multiply,id:1625,x:33222,y:32730,varname:node_1625,prsc:2|A-773-RGB,B-88-RGB,C-6381-RGB,D-88-A;n:type:ShaderForge.SFN_Tex2d,id:6381,x:32981,y:32942,ptovrint:False,ptlb:texture_xt,ptin:_texture_xt,varname:node_6381,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:697babe0ff89e7d4b93e57125ebc2255,ntxv:0,isnm:False|UVIN-2733-OUT;n:type:ShaderForge.SFN_Color,id:773,x:32838,y:32480,ptovrint:False,ptlb:color,ptin:_color,varname:node_773,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_VertexColor,id:88,x:32875,y:32690,varname:node_88,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:1155,x:31689,y:32719,ptovrint:False,ptlb:xuanzhuan,ptin:_xuanzhuan,varname:node_1155,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_TexCoord,id:3927,x:31821,y:32432,varname:node_3927,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Rotator,id:9236,x:32094,y:32474,varname:node_9236,prsc:2|UVIN-3927-UVOUT,SPD-1155-OUT;proporder:867-6381-773-1155;pass:END;sub:END;*/

Shader "cgwell/aplha_nosie" {
    Properties {
        _xz_wenli ("xz_wenli", 2D) = "white" {}
        _texture_xt ("texture_xt", 2D) = "white" {}
        _color ("color", Color) = (0.5,0.5,0.5,1)
        _xuanzhuan ("xuanzhuan", Float ) = 1
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _xz_wenli; uniform float4 _xz_wenli_ST;
            uniform sampler2D _texture_xt; uniform float4 _texture_xt_ST;
            uniform float4 _color;
            uniform float _xuanzhuan;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_8573 = _Time;
                float node_9236_ang = node_8573.g;
                float node_9236_spd = _xuanzhuan;
                float node_9236_cos = cos(node_9236_spd*node_9236_ang);
                float node_9236_sin = sin(node_9236_spd*node_9236_ang);
                float2 node_9236_piv = float2(0.5,0.5);
                float2 node_9236 = (mul(i.uv0-node_9236_piv,float2x2( node_9236_cos, -node_9236_sin, node_9236_sin, node_9236_cos))+node_9236_piv);
                float4 _xz_wenli_var = tex2D(_xz_wenli,TRANSFORM_TEX(node_9236, _xz_wenli));
                float2 node_2733 = lerp(float2(_xz_wenli_var.r,_xz_wenli_var.g),i.uv0,i.uv0);
                float4 _texture_xt_var = tex2D(_texture_xt,TRANSFORM_TEX(node_2733, _texture_xt));
                float3 emissive = (_color.rgb*i.vertexColor.rgb*_texture_xt_var.rgb*i.vertexColor.a);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
