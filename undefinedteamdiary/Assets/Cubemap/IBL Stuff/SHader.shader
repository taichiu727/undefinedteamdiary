// Shader created with Shader Forge Beta 0.25 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.25;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:3,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:True,rmgx:True,hqsc:True,hqlp:False,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0;n:type:ShaderForge.SFN_Final,id:1,x:31913,y:32768|diff-2-RGB,spec-2-A,gloss-124-OUT,normal-8-RGB,amdfl-78-OUT,amspl-132-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:32707,y:32625,ptlb:Main Texture,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8,x:33474,y:32784,ptlb:Normal,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Cubemap,id:72,x:33425,y:32985,ptlb:IBL 1,cube:3eb649cf38bab97439d7e03412477408,pvfc:0|DIR-87-OUT;n:type:ShaderForge.SFN_Lerp,id:78,x:32652,y:32866|A-72-RGB,B-79-RGB,T-80-OUT;n:type:ShaderForge.SFN_AmbientLight,id:79,x:33119,y:33156;n:type:ShaderForge.SFN_Vector1,id:80,x:33120,y:33012,v1:0.5;n:type:ShaderForge.SFN_NormalVector,id:87,x:33652,y:32952,pt:True;n:type:ShaderForge.SFN_Cubemap,id:94,x:33076,y:33355,ptlb:IBL Spec|DIR-96-OUT;n:type:ShaderForge.SFN_Slider,id:95,x:33009,y:33584,ptlb:Reflection Strenth,min:0,cur:0,max:1;n:type:ShaderForge.SFN_ViewReflectionVector,id:96,x:33381,y:33313;n:type:ShaderForge.SFN_Multiply,id:97,x:32665,y:33252|A-94-RGB,B-95-OUT;n:type:ShaderForge.SFN_Multiply,id:105,x:32542,y:33136|A-2-A,B-97-OUT;n:type:ShaderForge.SFN_Slider,id:124,x:32628,y:32809,ptlb:Gloss Slider,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Fresnel,id:130,x:32417,y:33385|EXP-131-OUT;n:type:ShaderForge.SFN_Slider,id:131,x:32713,y:33480,ptlb:Specular Fresnel,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:132,x:32219,y:33133|A-105-OUT,B-130-OUT;proporder:2-8-72-94-95-124-131;pass:END;sub:END;*/

Shader "Custom/SHader" {
    Properties {
        _MainTexture ("Main Texture", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        _IBL1 ("IBL 1", Cube) = "_Skybox" {}
        _IBLSpec ("IBL Spec", Cube) = "_Skybox" {}
        _ReflectionStrenth ("Reflection Strenth", Range(0, 1)) = 0
        _GlossSlider ("Gloss Slider", Range(0, 1)) = 0
        _SpecularFresnel ("Specular Fresnel", Range(0, 1)) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform samplerCUBE _IBL1;
            uniform samplerCUBE _IBLSpec;
            uniform float _ReflectionStrenth;
            uniform float _GlossSlider;
            uniform float _SpecularFresnel;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float4 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float2 node_136 = i.uv0;
                float3 normalLocal = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_136.rg, _Normal))).rgb;
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i)*2;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL)*InvPi * attenColor + UNITY_LIGHTMODEL_AMBIENT.xyz*2;
///////// Gloss:
                float gloss = exp2(_GlossSlider*10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float4 node_2 = tex2D(_MainTexture,TRANSFORM_TEX(node_136.rg, _MainTexture));
                float3 specularColor = float3(node_2.a,node_2.a,node_2.a);
                float specularMonochrome = dot(specularColor,float3(0.3,0.59,0.11));
                float HdotL = max(0.0,dot(halfDirection,lightDirection));
                float3 fresnelTerm = specularColor + ( 1.0 - specularColor ) * pow((1.0 - HdotL),5);
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float3 fresnelTermAmb = specularColor + ( 1.0 - specularColor ) * pow((1.0 - NdotV),5);
                float alpha = 1.0 / ( sqrt( (Pi/4.0) * gloss + Pi/2.0 ) );
                float visTerm = ( NdotL * ( 1.0 - alpha ) + alpha ) * ( NdotV * ( 1.0 - alpha ) + alpha );
                visTerm = 1.0 / visTerm;
                float normTerm = (gloss + 8.0 ) / (8.0 * Pi);
                float3 specularAmb = ((node_2.a*(texCUBE(_IBLSpec,viewReflectDirection).rgb*_ReflectionStrenth))*pow(1.0-max(0,dot(normalDirection, viewDirection)),_SpecularFresnel)) * fresnelTermAmb;
                float3 specular = (floor(attenuation) * _LightColor0.xyz)*NdotL * pow(max(0,dot(halfDirection,normalDirection)),gloss)*fresnelTerm*visTerm*normTerm + specularAmb;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                diffuseLight += lerp(texCUBE(_IBL1,normalDirection).rgb,UNITY_LIGHTMODEL_AMBIENT.rgb,0.5); // Diffuse Ambient Light
                diffuseLight *= 1-specularMonochrome;
                finalColor += diffuseLight * node_2.rgb;
                finalColor += specular;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float _GlossSlider;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float4 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float2 node_137 = i.uv0;
                float3 normalLocal = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_137.rg, _Normal))).rgb;
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i)*2;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL)*InvPi * attenColor;
///////// Gloss:
                float gloss = exp2(_GlossSlider*10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float4 node_2 = tex2D(_MainTexture,TRANSFORM_TEX(node_137.rg, _MainTexture));
                float3 specularColor = float3(node_2.a,node_2.a,node_2.a);
                float specularMonochrome = dot(specularColor,float3(0.3,0.59,0.11));
                float HdotL = max(0.0,dot(halfDirection,lightDirection));
                float3 fresnelTerm = specularColor + ( 1.0 - specularColor ) * pow((1.0 - HdotL),5);
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float alpha = 1.0 / ( sqrt( (Pi/4.0) * gloss + Pi/2.0 ) );
                float visTerm = ( NdotL * ( 1.0 - alpha ) + alpha ) * ( NdotV * ( 1.0 - alpha ) + alpha );
                visTerm = 1.0 / visTerm;
                float normTerm = (gloss + 8.0 ) / (8.0 * Pi);
                float3 specular = attenColor*NdotL * pow(max(0,dot(halfDirection,normalDirection)),gloss)*fresnelTerm*visTerm*normTerm;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                diffuseLight *= 1-specularMonochrome;
                finalColor += diffuseLight * node_2.rgb;
                finalColor += specular;
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
