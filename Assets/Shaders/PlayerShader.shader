Shader "MyShaders/PlayerShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SampleNum ("Number of Samples", Integer) = 32
        _MinAngle ("Dying Progress", Range(0, 3.15)) = 0
        _IsPopping ("Is Popping", Integer) = 0
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
// Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it uses non-square matrices
#pragma exclude_renderers gles
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
            int _SampleNum;
            int _IsPopping;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            bool IsGreaterThanMinAngle(float2 pos)
            {
                float2 radius;
                float angle;
                
                radius = pos - 0.5;
                radius = normalize(radius);
                angle = acos(dot(radius, float2(1, 0)));
                return angle >= _MinAngle;
            }
            
            float2 GetSampleCenter(float2 pos, float sampleLength, float halfSampleLength)
            {
                float2 mod;
                float2 res;
                
                mod = pos % sampleLength;
                res = pos + halfSampleLength - mod;
                return res;
            }

            float2 GetSampleDistFromDeathLine(float2 sampleCen)
            {
                float2 dist;
                float2 sampleCenVec;
                float sampleAngle;
                float diffAngle;
                float2 deathLineVec;
                float2 res;
                
                sampleCenVec = sampleCen - 0.5;
                sampleAngle = acos(dot(normalize(sampleCenVec), float2(1, 0)));
                diffAngle = _MinAngle - sampleAngle;
                deathLineVec = length(sampleCenVec) * cos(diffAngle) * float2(cos(_MinAngle), sin(_MinAngle));
                deathLineVec.y = deathLineVec.y * sign(sampleCenVec.y);
                res = deathLineVec - sampleCenVec;
                return res;
            }
            
            bool IsInsideRect(float2 p, float2 center, float halfWidth, float halfHeight, float angle)
            {
                float2 centerPointVec;
                float2 widthVer;
                float2 heightVer;
                float2 dist;
                
                centerPointVec = p - center;
                widthVer = float2(cos(angle), sin(angle));
                heightVer.x = -widthVer.y;
                heightVer.y = widthVer.x;
                dist.x = dot(centerPointVec, widthVer);
                dist.y = dot(centerPointVec, heightVer);
                dist = abs(dist);
                return dist.x <= halfWidth && dist.y <= halfHeight;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 textel;
                float sampleLength;
                float halfSampleLength;
                float2 sampleCen;
                fixed4 res;
                
                textel = tex2D(_MainTex, i.uv);
                i.uv.x = i.uv.x * 4;
                i.uv.y = (i.uv.y - 0.75) * 4;
                sampleLength = 1.0 / float(_SampleNum);
                halfSampleLength = sampleLength / 2.0;
                sampleCen = GetSampleCenter(i.uv, sampleLength, halfSampleLength);
                if (_IsPopping == 0)
                {
                    if (_MinAngle <= 0) return textel;
                    if (_MinAngle >= 3.14) return fixed4(0, 0, 0, 0);
                    float2 dist;
                    bool isAngleGreater;
                    bool isSampleCloseToDeathLine;
                    
                    isAngleGreater = IsGreaterThanMinAngle(sampleCen);
                    dist = GetSampleDistFromDeathLine(sampleCen);
                    dist = abs(dist);
                    isSampleCloseToDeathLine = dist.x <= halfSampleLength & dist.y <= halfSampleLength;
                    res.xyz = textel.xyz;
                    res.w =  textel.w * (isAngleGreater | isSampleCloseToDeathLine);
                }
                else
                {
                    bool3 insideRect;
                    float3x2 centers;
                    float3x2 dims;
                    float3 angles;
                    
                    sampleCen.x = lerp(sampleCen.x, 1 - sampleCen.x, sampleCen.x > 0.5);
                    sampleCen.y = lerp(sampleCen.y, 1 - sampleCen.y, sampleCen.y > 0.5);
                    centers[0] = float2(0.15, 0.5);
                    centers[1] = float2(0.26, 0.31);
                    centers[2] = float2(0.36, 0.21);
                    dims[0] = float2(0.1, 0.02);
                    dims[1] = float2(0.085, 0.025);
                    dims[2] = float2(0.08, 0.025);
                    angles.x = 0;
                    angles.y = radians(50);
                    angles.z = radians(70);
                    insideRect.x = IsInsideRect(sampleCen, centers[0], dims[0][0], dims[0][1], angles.x);
                    insideRect.y = IsInsideRect(sampleCen, centers[1], dims[1][0], dims[1][1], angles.y);
                    insideRect.z = IsInsideRect(sampleCen, centers[2], dims[2][0], dims[2][1], angles.z);
                    res = lerp(fixed4(0, 0, 0, 0), fixed4(1, 1, 0, 1), any(insideRect));
                }
                return res;
            }
            ENDCG
        }
    }
}