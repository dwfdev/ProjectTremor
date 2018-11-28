// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32063,y:32679,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:6a985dfcd1d3ca94e9753748ed65c697,ntxv:0,isnm:False|UVIN-7431-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-1755-OUT,B-2053-RGB,C-797-RGB,D-9248-OUT,E-8655-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32063,y:32850,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32063,y:33002,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1102941,c2:0.1102941,c3:0.1102941,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:31769,y:33166,varname:node_9248,prsc:2,v1:10;n:type:ShaderForge.SFN_Multiply,id:1755,x:32137,y:32378,varname:node_1755,prsc:2|A-6074-RGB,B-8983-RGB;n:type:ShaderForge.SFN_Tex2d,id:8983,x:31595,y:32116,ptovrint:False,ptlb:node_8983,ptin:_node_8983,varname:node_8983,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5cb8efbfd43b6d54888d782703d959dd,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9528,x:31576,y:31799,ptovrint:False,ptlb:node_9528,ptin:_node_9528,varname:node_9528,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-7431-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7019,x:30647,y:32797,ptovrint:False,ptlb:y_speed,ptin:_y_speed,varname:node_7019,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:50;n:type:ShaderForge.SFN_ValueProperty,id:8528,x:30647,y:32719,ptovrint:False,ptlb:x_speed,ptin:_x_speed,varname:node_8528,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Time,id:7195,x:30665,y:32353,varname:node_7195,prsc:2;n:type:ShaderForge.SFN_Append,id:3140,x:30833,y:32719,varname:node_3140,prsc:2|A-8528-OUT,B-7019-OUT;n:type:ShaderForge.SFN_Multiply,id:9078,x:31051,y:32444,varname:node_9078,prsc:2|A-7195-TSL,B-3140-OUT;n:type:ShaderForge.SFN_TexCoord,id:7104,x:31051,y:32255,varname:node_7104,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:7431,x:31251,y:32335,varname:node_7431,prsc:2|A-7104-UVOUT,B-9078-OUT;n:type:ShaderForge.SFN_Multiply,id:8655,x:32333,y:33070,varname:node_8655,prsc:2|A-6074-A,B-797-A;proporder:6074-797-8983-7019-8528;pass:END;sub:END;*/

Shader "Shader Forge/Test 2" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (0.1102941,0.1102941,0.1102941,1)
        _node_8983 ("node_8983", 2D) = "white" {}
        _y_speed ("y_speed", Float ) = 50
        _x_speed ("x_speed", Float ) = 0
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
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform sampler2D _node_8983; uniform float4 _node_8983_ST;
            uniform float _y_speed;
            uniform float _x_speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_7195 = _Time;
                float2 node_7431 = (i.uv0+(node_7195.r*float2(_x_speed,_y_speed)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_7431, _MainTex));
                float4 _node_8983_var = tex2D(_node_8983,TRANSFORM_TEX(i.uv0, _node_8983));
                float3 emissive = ((_MainTex_var.rgb*_node_8983_var.rgb)*i.vertexColor.rgb*_TintColor.rgb*10.0*(_MainTex_var.a*_TintColor.a));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0.5,0.5,0.5,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
