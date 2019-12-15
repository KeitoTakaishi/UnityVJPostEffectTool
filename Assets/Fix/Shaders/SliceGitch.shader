Shader "Hidden/SliceGitch"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "Libs/SimplexNoise3D.cginc"

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

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			float _BlockWidth;
			float _BlockHeight;
			float _Fineness;
			float _Speed;
			float _Threshold;

			fixed4 frag(v2f i) : SV_Target
			{
				float2 uv = i.uv;
				float2 blockSize = float2(_BlockWidth, _BlockHeight);
				uv = floor(uv * blockSize) / blockSize;
				float noise = simplexNoise(float3(uv.x, uv.y * 10.0, _Time.y));
				float4 col = tex2D(_MainTex, i.uv);

				float2 offset = (0.1, -0.2);

				if (noise > _Threshold) {
					float theta = simplexNoise(float3(uv.x, uv.y*_Fineness, _Time.w*_Speed));

					col = tex2D(_MainTex, i.uv + float2(1.0*sin(theta), 1.0 / _BlockHeight));

					//color shift
					col.r = tex2D(_MainTex, i.uv + float2(1.0*sin(theta) + offset.x, 1.0 / _BlockHeight));
					col.b = tex2D(_MainTex, i.uv + float2(1.0*sin(theta) + offset.y, 1.0 / _BlockHeight));

				}

				return col;
			}
			ENDCG
		}
	}
}
