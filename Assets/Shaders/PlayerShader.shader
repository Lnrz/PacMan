Shader "MyShaders/PlayerShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MinAngle ("Dying Progress", Range(0, 3.15)) = 0
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
            bool _IsDying;
            float _MinAngle;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 textCol;
                fixed4 col;
                float2 radius;
                float2 center;
                float angle;
                
                textCol = tex2D(_MainTex, i.uv);
                center = float2(0.125, 0.875);
                radius = i.uv - center;
                radius = normalize(radius);
                angle = dot(radius, float2(1, 0));
                angle = acos(angle);
                col.xyz = textCol.xyz;
                col.w = textCol.w * (_MinAngle <= angle);
                return col;
            }
            ENDCG
        }
    }
}
