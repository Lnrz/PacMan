Shader "MyShaders/PostProcessorShaders/ObliqueBandShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Width ("Band Width", Range(0, 0.5)) = 0.256
        _Angle ("Band Angle", Range(0, 90)) = 67.5
        _Strength ("Band Strength", Range(0, 0.3)) = 0.03
        _Speed ("Band Speed", Integer) = 5
    }
    SubShader
    {
        Cull Off
        ZWrite Off
        ZTest Always
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _Width;
            float _Angle;
            float _Strength;
            int _Speed;
            
            float DistFromBand(float x, float bandAngle, float2 p)
            {
                float bandAngleCos;
                float bandAngleSin;
                float2 dist;
                float2 normDist;
                float ipot;
                float bandPointAngle;
                float bandPointDist;
                
                bandAngleCos = cos(bandAngle);
                bandAngleSin = sin(bandAngle);
                dist = p - float2(x, 0);
                normDist = normalize(dist);
                ipot = length(dist);
                bandPointAngle = acos(dot(normDist, float2(bandAngleCos, bandAngleSin)));
                bandPointDist = ipot * sin(bandPointAngle);
                return bandPointDist;
            }
            
            float GetStrengthModifier(float distFromBand, float maxDist)
            {
                float mod;
                float relDiff;
                float damp;
                
                relDiff = (maxDist - distFromBand) / maxDist;
                damp = 1 / pow(2, (4 - relDiff * 10) / 4 * 5);
                mod = lerp(1, damp, relDiff < 0.4);
                return mod;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col;
                float time;
                float xs;
                float xd;
                float x1;
                float x2;
                float halfWidth;
                float angle;
                float cotan;
                float offset;
                float distFromBand1;
                float strengthMod1;
                bool inBand1;
                float distFromBand2;
                float strengthMod2;
                bool inBand2;
                bool notBlack;

                time = (_Time.y % _Speed) / _Speed;
                halfWidth = _Width / 2;
                angle = _Angle * 3.14 / 180;
                cotan = cos(angle) / sin(angle);
                offset = 0.5 + 2 * halfWidth + cotan;
                xs = 0.5 - offset;
                xd = 0.5 + offset;
                x1 = lerp(xd, xs, time);
                distFromBand1 = DistFromBand(x1, angle, i.uv);
                inBand1 = distFromBand1 <= halfWidth;
                strengthMod1 = GetStrengthModifier(distFromBand1, halfWidth);
                x2 = lerp(x1 + offset, x1 - offset, x1 >= 0.5);
                distFromBand2 = DistFromBand(x2, angle, i.uv);
                inBand2 = distFromBand2 <= halfWidth;
                strengthMod2 = GetStrengthModifier(distFromBand2, halfWidth);
                col = tex2D(_MainTex, i.uv);
                notBlack = col.x != 0 || col.y != 0 || col.z != 0;
                col = lerp(col, col + _Strength * strengthMod1, inBand1 && notBlack);
                col = lerp(col, col + _Strength * strengthMod2, inBand2 && notBlack);
                return col;
            }
            ENDCG
        }
    }
}
