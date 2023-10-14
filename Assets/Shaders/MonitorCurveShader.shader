Shader "PostProcessorShaders/MonitorCurveShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            static const float horizontalStrength = 1.8;
            static const float horizontalRadius = sqrt(pow(horizontalStrength, 2) + 0.25);
            static const float verticalStrength = 3.6;
            static const float verticalRadius = sqrt(pow(verticalStrength, 2) + 0.25);

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                float3 center = float3(0.5, i.uv.y, horizontalStrength);
                float3 p = float3(i.uv.x, i.uv.y, 0);
                float3 diff = p - center;
                float3 diffNorm = normalize(diff);
                float remLen = horizontalRadius - length(diff);
                float2 newUV = i.uv;
                newUV.x = p.x + diffNorm.x * remLen;
                
                center = float3(i.uv.x, 0.5, verticalStrength);
                diff = p - center;
                diffNorm = normalize(diff);
                remLen = verticalRadius - length(diff);
                newUV.y = p.y + diffNorm.y * remLen;
    
                /*
                if (newUV.y <= 0.5)
                {
                    return float4(newUV.y, 0, 0, 1);
                }
                else
                {
                    return float4(1 - newUV.y, 0, 0, 1);
                }
                */
    
                fixed4 col = tex2D(_MainTex, newUV);
                return col;
            }
            ENDCG
        }
    }
}
