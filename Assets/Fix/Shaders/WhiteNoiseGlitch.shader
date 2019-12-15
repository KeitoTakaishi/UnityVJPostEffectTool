Shader "Hidden/WhiteNoiseGlitch"
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
			#include "Libs/NoiseUtils.cginc"
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


			fixed4 frag(v2f i) : SV_Target
			{
				float2 uv = i.uv;

				//------
				//simple white nosie
				float whiteNoise = rnd(float2(uv)+float2(_Time.yy));
				//------

				//------
				//blockNoise
				float2 buv = floor(uv * float2(10.0, 100.0)) / float2(10.0, 100.0);
				buv = buv * float2(10.0, 7.5);
				float blockNoise = simplexNoise(float3 (buv.x, buv.y, _Time.y * 3.0));
				//------

				//------
				//displacement
				float2 b = floor(uv * float2(1.0, 1.0)) / float2(1.0, 1.0);
				float displacement = simplexNoise(float3 (b.x, b.y, _Time.y * 3.0));
				displacement *= 0.1;
				//------

				//-------
				//GlitchNoise
				fixed4 col = tex2D(_MainTex, uv);
				float2 offSet1 = float2(0.01 * sin(_Time.y*100.0 + uv.x),
										0.01 * sin(_Time.y*100.0 + uv.y)
										);
				float2 offSet2 = float2(-0.01 * sin(_Time.y*100.0 + uv.x),
										0.01 * sin(_Time.y*100.0 + uv.y)
										);

				float thresold = 0.75;
				col.r = tex2D(_MainTex, uv + (displacement + offSet1) * step(thresold, blockNoise)).r;
				col.b = tex2D(_MainTex, uv + (displacement + offSet2) * step(thresold, blockNoise)).b;

				//--------
				col.rgb *= whiteNoise;
				col.rgb *= 2.0;
				return col;
			}
			ENDCG
		}
	}
}
