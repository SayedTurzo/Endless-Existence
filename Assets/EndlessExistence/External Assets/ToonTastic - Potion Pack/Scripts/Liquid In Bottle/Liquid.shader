Shader "Universal Render Pipeline/Lit/Liquid"
{
    Properties
    {
        [HDR] _Colour ("Colour", Color) = (1,1,1,1)
        _FillAmount ("Fill Amount", Range(-10,10)) = 0.0
        [HideInInspector] _WobbleX ("WobbleX", Range(-1,1)) = 0.0
        [HideInInspector] _WobbleZ ("WobbleZ", Range(-1,1)) = 0.0
        _TopColor ("Top Color", Color) = (1,1,1,1)
        _FoamColor ("Foam Line Color", Color) = (1,1,1,1)
        _Rim ("Foam Line Width", Range(0,0.1)) = 0.0
        _RimColor ("Rim Color", Color) = (1,1,1,1)
        _RimPower ("Rim Power", Range(0,10)) = 0.0
    }

    SubShader
    {
        Tags {"Queue"="Geometry"  "DisableBatching" = "True" "RenderPipeline"="Universal"}

        Pass
        {
            ZWrite On
            Cull Off // we want the front and back faces
            AlphaToMask On // transparency
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            #define UNITY_PI 3.1415926535897932384626433832795

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 viewDir : COLOR;
                float3 normal : COLOR1;
                float fillEdge : TEXCOORD1;
                float4 _ShadowCoord : TEXCOORD2;
            };

            float _FillAmount, _WobbleX, _WobbleZ;
            float4 _TopColor, _RimColor, _FoamColor, _Colour;
            float _Rim, _RimPower;

            float4 RotateAroundYInDegrees (float4 vertex, float degrees)
            {
                float alpha = degrees * UNITY_PI / 180;
                float sina, cosa;
                sincos(alpha, sina, cosa);
                float2x2 m = float2x2(cosa, sina, -sina, cosa);
                return float4(vertex.yz , mul(m, vertex.xz)).xzyw ;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                float3 viewDir = normalize(_WorldSpaceCameraPos - v.vertex.xyz);
                o.viewDir = viewDir;
                o.normal = v.normal;
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex.xyz);
                float3 worldPosX = RotateAroundYInDegrees(float4(worldPos, 0), 360);
                float3 worldPosZ = float3(worldPosX.y, worldPosX.z, worldPosX.x);
                float3 worldPosAdjusted = worldPos + (worldPosX * _WobbleX) + (worldPosZ * _WobbleZ);
                o.fillEdge = worldPosAdjusted.y + _FillAmount;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // rim light
                half dotProduct = 1 - pow(dot(i.normal, i.viewDir), _RimPower);
                half4 RimResult = smoothstep(0.5, 1.0, dotProduct);
                RimResult *= _RimColor;
                // foam edge
                half4 foam = ( step(i.fillEdge, 0.5) - step(i.fillEdge, (0.5 - _Rim)));
                half4 foamColored = foam * (_FoamColor * 0.75);
                // rest of the liquid
                half4 result = step(i.fillEdge, 0.5) - foam;
                half4 resultColored = result * _Colour;
                // both together
                half4 finalResult = resultColored + foamColored;
                finalResult.rgb += RimResult;
                // color of backfaces/ top
                half4 topColor = _TopColor * (foam + result);
                //VFACE returns positive for front facing, negative for backfacing
                return finalResult;
            }
            ENDHLSL
        }
    }
}
