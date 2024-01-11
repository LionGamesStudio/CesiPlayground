Shader "Custom/Toon"
{
    Properties
    {
        _MainColor("Main Color", Color) = (1, 1, 1, 1)

        [HDR]
        _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)

        [HDR]
        _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
        _Glossiness("Glossiness", Float) = 32

        [HDR]
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount("Rim Amount", Range(0, 1)) = 0.716

        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
    }

    SubShader {

        Tags
        {
            "LightMode" = "ForwardBase"
            "PassFlags" = "OnlyDirectional"
        }

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            // Color
            uniform float4 _MainColor;

            // Ambient
            uniform float4 _AmbientColor;

            // Glossiness
            float _Glossiness;

            // Specular
            float4 _SpecularColor;

            // Rim
            float4 _RimColor;
            float _RimAmount;
            float _RimThreshold;

            struct MeshData {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID  
            };

            struct Interpolators {
                float2 uv : TEXCOORD0;
                float3 viewDir : TEXCOORD1;
                float3 worldNormal : NORMAL;
                float4 pos : SV_POSITION;
                SHADOW_COORDS(2)
                UNITY_VERTEX_OUTPUT_STEREO
            };

            Interpolators vert(MeshData v) {
				Interpolators o;
                
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT(Interpolators, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
    
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = WorldSpaceViewDir(v.vertex);
				o.uv = v.uv;
                TRANSFER_SHADOW(o)
				return o;
			}

            fixed4 frag(Interpolators i) : SV_Target{

                // Directional Light
                float3 normal = normalize(i.worldNormal);
                float NdotL = dot(_WorldSpaceLightPos0, normal);

                // Ambient light
                float shadow = SHADOW_ATTENUATION(i);
                float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);
                float4 light = lightIntensity * _LightColor0;

                // Specular light reflection
                float3 viewDir = normalize(i.viewDir);

                float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
                float NdotH = dot(normal, halfVector);

                float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);

                float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
                float4 specular = specularIntensitySmooth * _SpecularColor;

                // Rim lightning
                float4 rimDot = 1 - dot(viewDir, normal);
                float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
                rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
                float4 rim = rimIntensity * _RimColor;

                return _MainColor * (_AmbientColor + light + specular + rim);
			}
			ENDCG
        }

        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
    
}