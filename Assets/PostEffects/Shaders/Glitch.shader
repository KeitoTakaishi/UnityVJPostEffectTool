Shader "Hidden/Glitch"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Width("Width", Float) = 0.0
		_Height("Height", Float) = 0.0
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
			#include "SimplexNoise3D.cginc"

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
			float2 blockSize;

            fixed4 frag (v2f i) : SV_Target
            {
				float2 uv = i.uv;
				blockSize = float2(1.0, _Height);
				uv = floor(uv * blockSize) / blockSize;
				float noise = simplexNoise(float3(uv.x, uv.y, _Time.y));
				float4 col = tex2D(_MainTex, i.uv);

				float threshould = 0.45;
				float2 offset = (0.1, -0.2);

				if (noise > threshould) {
					float theta = simplexNoise(float3(uv.x, uv.y*70.0, _Time.w));
					col = tex2D(_MainTex, i.uv + float2(0.5*sin(theta), 1.0 / _Height));
					//col.rgb = (float3(1.0, 1.0, 1.0) - col.rgb)*4.0;
					//col.g = 1.0 - col.g;
				}
			
                return col;
            }
            ENDCG
        }
    }
}
