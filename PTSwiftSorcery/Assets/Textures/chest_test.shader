// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33243,y:32642,varname:node_4013,prsc:2|emission-1238-OUT;n:type:ShaderForge.SFN_NormalVector,id:8036,x:32376,y:32974,prsc:2,pt:False;n:type:ShaderForge.SFN_Multiply,id:2533,x:32721,y:32670,varname:node_2533,prsc:2|A-1283-RGB,B-3079-OUT;n:type:ShaderForge.SFN_Lerp,id:1238,x:33009,y:32648,varname:node_1238,prsc:2|A-6797-RGB,B-2533-OUT,T-695-OUT;n:type:ShaderForge.SFN_Color,id:1283,x:32409,y:32662,ptovrint:False,ptlb:Glow Colour,ptin:_GlowColour,varname:node_1283,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.666,c3:0,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:8577,x:32523,y:33166,ptovrint:False,ptlb:Exp,ptin:_Exp,varname:node_8577,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:3079,x:32409,y:32853,ptovrint:False,ptlb:Glow Intensity,ptin:_GlowIntensity,varname:node_3079,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2.33;n:type:ShaderForge.SFN_Multiply,id:695,x:33034,y:33052,varname:node_695,prsc:2|A-6877-OUT,B-795-OUT,C-3761-OUT;n:type:ShaderForge.SFN_OneMinus,id:6877,x:32806,y:32954,varname:node_6877,prsc:2|IN-6595-OUT;n:type:ShaderForge.SFN_Fresnel,id:6595,x:32645,y:32970,varname:node_6595,prsc:2|NRM-8036-OUT,EXP-8577-OUT;n:type:ShaderForge.SFN_Slider,id:795,x:33161,y:33327,ptovrint:False,ptlb:Strength,ptin:_Strength,varname:node_795,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Tex2d,id:6797,x:32555,y:32397,ptovrint:False,ptlb:Base,ptin:_Base,varname:node_6797,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3ec821d2ac6e66e4aa6442ac9c02ff56,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Time,id:5066,x:32312,y:33267,varname:node_5066,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3023,x:32547,y:33282,varname:node_3023,prsc:2|A-5066-T,B-6951-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6951,x:32347,y:33533,ptovrint:False,ptlb:Time Value,ptin:_TimeValue,varname:node_6951,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Sin,id:6098,x:32715,y:33282,varname:node_6098,prsc:2|IN-3023-OUT;n:type:ShaderForge.SFN_Add,id:3761,x:32894,y:33292,varname:node_3761,prsc:2|A-6098-OUT,B-6631-OUT;n:type:ShaderForge.SFN_Vector1,id:6631,x:32672,y:33469,varname:node_6631,prsc:2,v1:1;proporder:1283-3079-8577-795-6797-6951;pass:END;sub:END;*/

Shader "Shader Forge/chest_test" {
    Properties {
        _GlowColour ("Glow Colour", Color) = (1,0.666,0,1)
        _GlowIntensity ("Glow Intensity", Float ) = 2.33
        _Exp ("Exp", Float ) = 1
        _Strength ("Strength", Range(0, 1)) = 0
        _Base ("Base", 2D) = "white" {}
        _TimeValue ("Time Value", Float ) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _GlowColour;
            uniform float _Exp;
            uniform float _GlowIntensity;
            uniform float _Strength;
            uniform sampler2D _Base; uniform float4 _Base_ST;
            uniform float _TimeValue;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 _Base_var = tex2D(_Base,TRANSFORM_TEX(i.uv0, _Base));
                float4 node_5066 = _Time + _TimeEditor;
                float3 emissive = lerp(_Base_var.rgb,(_GlowColour.rgb*_GlowIntensity),((1.0 - pow(1.0-max(0,dot(i.normalDir, viewDirection)),_Exp))*_Strength*(sin((node_5066.g*_TimeValue))+1.0)));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
