Shader "PostProcessorShaders/MonitorCurveShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Kx ("Horizontal Curvature Strength", Range(0.0001, 1.0)) = 0.2
        _Ky ("Vertical Curvature Strength", Range(0.0001, 1.0)) = 0.15
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
            
            float _Kx = 0.2;
            float _Ky = 0.1;

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
                float2 ru;
                
                ru = i.uv - float2(0.5, 0.5);
                ru = pow(length(ru), 2);
                
                i.uv.x = 0.5 + (i.uv.x - 0.5) / (2 * _Kx * ru) * (1 - sqrt(1 - 4 * _Kx * ru));
                i.uv.y = 0.5 + (i.uv.y - 0.5) / (2 * _Ky * ru) * (1 - sqrt(1 - 4 * _Ky * ru));
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
