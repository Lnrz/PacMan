Shader "MyShaders/WallShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _IsFlashing ("Is Flashing", Integer) = 0
        _Frequency ("Flash Frequency", Range(0,10)) = 2
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }
        LOD 100
        Cull OFF
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            bool _IsFlashing;
            float _Frequency;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col;
                fixed4 textCol;
                fixed3 flashCol;
                
                textCol = tex2D(_MainTex, i.uv);
                flashCol = lerp(textCol.xyz, float3(1, 1, 1), floor(_Time.y * _Frequency) % 2 == 0);
                col.xyz = lerp(textCol.xyz, flashCol, _IsFlashing);
                col.w = textCol.w;
                return col;
            }
            ENDCG
        }
    }
}
