Shader "MyShaders/UIinWorldShader"
{
    Properties
    {
        _MainTex ("IGNORE", 2D) = "white" {}
        [NoScaleOffset] _FirstUITex ("First UI Texture", 2D) = "white" {}
        [NoScaleOffset] _SecondUITex ("Second UI Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        { 
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        Cull OFF
        ZTest Always
        ZWrite OFF
        
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
            sampler2D _FirstUITex;
            sampler2D _SecondUITex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col;
                fixed4 textel1;
                fixed4 textel2;
                
                textel1 = tex2D(_FirstUITex, i.uv);
                textel2 = tex2D(_SecondUITex, i.uv);
                col = textel1 + textel2;
                return col;
            }
            ENDCG
        }
    }
}
