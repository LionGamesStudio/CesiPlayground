Shader "Custom/SkyboxToon"
{
    Properties{
        _Color("Tint Color", Color) = (1, 1, 1, 1)
        _Skybox("Skybox", Cube) = "_Skybox" {}

        [Header(Rim Lighting)]
        _RimColor("Rim Color", Color) = (1, 1, 1, 1)
        _RimPower("Rim Power", Range(0, 8)) = 2

        [Header(Clouds)]
        _CloudDensity("Cloud Density", Range(0.1, 1)) = 0.5
        _CloudSpeed("Cloud Speed", Range(0.1, 5)) = 1.0
        _CloudScale("Cloud Scale", Range(1, 10)) = 5
        _NoiseTex("Cloud Noise Texture", 3D) = "white" {}
    }

    SubShader{
        Tags { "RenderType" = "Background" }

        Pass {
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            samplerCUBE _Skybox;
            sampler3D _NoiseTex;
            fixed4 _Color;

            // Rim lighting
            fixed4 _RimColor;
            half _RimPower;

            // Clouds
            half _CloudDensity;
            half _CloudSpeed;
            half _CloudScale;

            struct MeshData {
                float4 vertex : POSITION;
               UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Interpolators {
                float4 pos : SV_POSITION;
                float3 worldRefl : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            Interpolators vert(MeshData v) {
                Interpolators o;
    
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT(Interpolators, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
    
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldRefl = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(Interpolators i) : SV_Target{

                // Cloud generation
                float3 cloudCoords = i.worldRefl * _CloudScale + float3(_Time.y * _CloudSpeed, _Time.y * _CloudSpeed, _Time.y * _CloudSpeed);
                float cloudValue = tex3D(_NoiseTex, cloudCoords).r * _CloudDensity;

                // Skybox color calculation
                fixed4 col = texCUBE(_Skybox, i.worldRefl);
                

                col.rgb *= (1.0 - cloudValue) * _Color.rgb;

                return col;
            }
            ENDCG
        }
    }
    FallBack "Skybox/Cubemap"
}