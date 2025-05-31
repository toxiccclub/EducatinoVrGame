Shader "PolyArtMaskTint_URP"
{
    Properties
    {
        _Smoothness("Smoothness", Range(0 , 1)) = 0
        _Metallic("Metallic", Range(0 , 1)) = 0
        _Hair("Hair", Color) = (0,0,0,0)
        _InnerCloth("InnerCloth", Color) = (0,0,0,0)
        _OuterClothes("OuterClothes", Color) = (0,0,0,0)
        _PolyArtAlbedo("PolyArtAlbedo", 2D) = "white" {}
        _PolyArtMask("PolyArtMask", 2D) = "white" {}
    }

        SubShader
    {
        Tags { "RenderPipeline" = "UniversalPipeline" "RenderType" = "Opaque" }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 position : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 position : SV_POSITION;
            };

            sampler2D _PolyArtAlbedo;
            sampler2D _PolyArtMask;
            float4 _Hair, _InnerCloth, _OuterClothes;
            float _Metallic, _Smoothness;

            v2f vert(appdata v)
            {
                v2f o;
                o.position = TransformObjectToHClip(v.position.xyz);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                float4 albedo = tex2D(_PolyArtAlbedo, i.uv);
                float4 mask = tex2D(_PolyArtMask, i.uv);

                float4 blendColor = min(mask.r, _OuterClothes) + min(mask.g, _InnerCloth) + min(mask.b, _Hair);
                float4 finalColor = lerp(albedo, saturate(albedo * blendColor) * 2.0, (mask.r + mask.g + mask.b));

                return float4(finalColor.rgb, 1.0);
            }
            ENDHLSL
        }
    }
}
