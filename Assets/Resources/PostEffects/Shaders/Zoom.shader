Shader "Hidden/Zoom"
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

			float rnd(float2 scale, float seed, float2 uv) {
				return frac(sin(dot(uv.xy + seed, scale)) * 43758.5453 + seed);
			}

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
			float _Height;
			int _LoopNum;
			float _Strength;


			//const float2 centeroffset = float2(256, 256);
            fixed4 frag (v2f i) : SV_Target
            {

				float4 col = float4(0.0, 0.0, 0.0, 1.0);
				float2 tFrag = float2(1.0, 1.0) / float2(1.0, 1.0);
				float3 destColor = float4(0.0, 0.0, 0.0, 0.0);
				float random = rnd(float2(12.9898, 78.233), 0.0, i.uv);
		
				float2 center = float2(0.5, 0.5);
				float2 fc = i.uv;
				float2 fcc = fc - center;
				float totalWeight = 0.0;
				
				for (float i = 0.0; i <= _LoopNum; i++) {
					float percent = (i + random) / _LoopNum;
					float weight = percent - percent * percent;
					float2  t = fc - fcc * percent * _Strength / _LoopNum;
					destColor += tex2D(_MainTex, t * tFrag).rgb * weight;
					totalWeight += weight;
					
				}       
				col.rgb = destColor / totalWeight;
				
				return col;
            }
            ENDCG
        }
    }
}
