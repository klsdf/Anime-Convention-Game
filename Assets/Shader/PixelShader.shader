Shader "Example/URPUnlitShaderTexture"
{
    Properties
    {
        [MainTexture] _BaseMap("Base Map", 2D) = "white" {}
        _BlockSize("Block Size", Range(0.5,16.0)) = 1.0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float2 uv           : TEXCOORD0;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseMap_ST;
                float _BlockSize; // Add a property to control the block size
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // Get the dimensions of the texture
                float width, height;
                _BaseMap.GetDimensions(width, height);
                float2 texelSize = float2( width, height);
                float2 uvtexel = IN.uv * texelSize;
                // Calculate the block coordinates
                float2 blockUV = (uvtexel-fmod(uvtexel, _BlockSize))/texelSize;

                // Sample the texture at the center of the block
                half4 color = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, blockUV);
                return color;
            }
            ENDHLSL
        }
    }
}