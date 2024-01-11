Shader "Custom/WaterToon"
{
    Properties
    {
        // Depth
        [Header(Depth Properties)]
        _DepthGradientShallow("Depth Gradient Shallow", Color) = (0.325, 0.807, 0.971, 0.725)
        _DepthGradientDeep("Depth Gradient Deep", Color) = (0.086, 0.407, 1, 0.749)
        _DepthMaxDistance("Depth Maximum Distance", Float) = 1

        // Wave
        [Header(Wave Properties)]
        _SurfaceNoise("Surface Noise", 2D) = "white" {}
        _SurfaceNoiseCutoff("Surface Noise Cutoff", Range(0, 1)) = 0.777
        
        [Header(Foam Properties)]
        _FoamMaxDistance("Foam Maximum Distance", Float) = 0.4
        _FoamMinDistance("Foam Minimum Distance", Float) = 0.04
        _FoamColor("Foam Color", Color) = (1,1,1,1)

        // Animation 
        [Header(Animation Properties)]
        _SurfaceNoiseScroll("Surface Noise Scroll Amount", Vector) = (0.03, 0.03, 0, 0)
        _SurfaceDistortionAmount("Surface Distortion Amount", Range(0, 1)) = 0.27
    }

    SubShader{

        Tags
        {
            "Queue" = "Transparent"
        }

        Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define SMOOTHSTEP_AA 0.001
            #define TAU 6.28318530718

            // Depth
            float4 _DepthGradientShallow;
            float4 _DepthGradientDeep;
            float _DepthMaxDistance;

            // Water depth
            sampler2D _CameraDepthTexture;
            sampler2D _CameraNormalsTexture;

            // Wave
            sampler2D _SurfaceNoise;
            float4 _SurfaceNoise_ST;
            float _SurfaceNoiseCutoff;

            // Foam
            float _FoamMaxDistance;
            float _FoamMinDistance;
            float4 _FoamColor;

            // Animation
            float2 _SurfaceNoiseScroll;

            // Distortion
            sampler2D _SurfaceDistortion;
            float4 _SurfaceDistortion_ST;
            float _SurfaceDistortionAmount;

            // Blend
            float4 alphaBlend(float4 top, float4 bottom)
            {
                float3 color = (top.rgb * top.a) + (bottom.rgb * (1 - top.a));
                float alpha = top.a + bottom.a * (1 - top.a);

                return float4(color, alpha);
            }

            struct MeshData {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID  
            };

            struct Interpolators {
                float2 noiseUV : TEXCOORD0;
                float2 distortUV : TEXCOORD1;
                float4 screenPosition : TEXCOORD2;
                float3 viewNormal : NORMAL;
                float4 pos : SV_POSITION;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            Interpolators vert(MeshData v) {
                Interpolators o;
    
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT(Interpolators, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.pos = UnityObjectToClipPos(v.vertex);
                o.screenPosition = ComputeScreenPos(o.pos);
                o.noiseUV = TRANSFORM_TEX(v.uv, _SurfaceNoise);
                o.distortUV = TRANSFORM_TEX(v.uv, _SurfaceDistortion);
                return o;
            }

            fixed4 frag(Interpolators i) : SV_Target{

                // Water Depth
                float existingDepth01 = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPosition)).r;
                float existingDepthLinear = LinearEyeDepth(existingDepth01);

                float depthDifference = existingDepthLinear - i.screenPosition.w;

                // Water Color
                float waterDepthDifference01 = saturate(depthDifference / _DepthMaxDistance);
                float4 waterColor = lerp(_DepthGradientShallow, _DepthGradientDeep, waterDepthDifference01);

                // Normals
                float3 existingNormal = tex2Dproj(_CameraNormalsTexture, UNITY_PROJ_COORD(i.screenPosition));
                float3 normalDot = saturate(dot(normalize(existingNormal), i.viewNormal));

                // Foam
                float foamDistance = lerp(_FoamMaxDistance, _FoamMinDistance, normalDot);
                float foamDepthDifference01 = saturate(depthDifference / foamDistance);
                float surfaceNoiseCutoff = foamDepthDifference01 * (1 -_SurfaceNoiseCutoff);

                // Distortion
                float2 distortSample = (tex2D(_SurfaceDistortion, i.distortUV).xy * 2 - 1) * _SurfaceDistortionAmount;

                // Animation
                float2 noiseUV = float2(
                    (i.noiseUV.x + _Time.y * _SurfaceNoiseScroll.x) + distortSample.x, 
                    (i.noiseUV.y + _Time.y * _SurfaceNoiseScroll.y) + distortSample.y
                 );

                // Wave
                float surfaceNoiseSample = tex2D(_SurfaceNoise, noiseUV).r;
                float surfaceNoise = smoothstep(
                    surfaceNoiseCutoff - SMOOTHSTEP_AA, 
                    surfaceNoiseCutoff + SMOOTHSTEP_AA, 
                    surfaceNoiseSample
                );

                // Blending two region of the water tiling


                // Foam Color
                float4 surfaceNoiseColor = _FoamColor;
                surfaceNoiseColor.a *= surfaceNoise;

                return alphaBlend(surfaceNoiseColor, waterColor);
            }
            ENDCG
        }
    }
}