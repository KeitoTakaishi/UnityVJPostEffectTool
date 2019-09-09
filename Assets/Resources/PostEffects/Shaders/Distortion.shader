	Shader "Hidden/Distortion"
	{
		Properties
		{
			_MainTex ("Texture", 2D) = "white" {}
			_power("Power", Float) = 1.0
			_pivotX("PivotX", Float) = 0.5
			_pivotY("PivotX", Float) = 0.5
		
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

				float _power;
				float _pivotX, _pivotY;
				fixed4 frag (v2f i) : SV_Target
				{
					float2 uv = i.uv;
					float2 pivot = float2(_pivotX, _pivotY);
					float finness = 3.0;
					float speed = 10.0;
					pivot.x = pivot.x*sin(uv.x*finness + _Time. y* speed);
					pivot.y = pivot.y*sin(uv.y*finness + _Time.y * speed);
					uv = uv - float2(pivot.x, pivot.y);
					uv *= pow(length(uv), _power);
					uv = uv + float2(pivot.x, pivot.y);
                
					fixed4 col = tex2D(_MainTex, uv);
				

					return col;
				}
				ENDCG
			}
		}
	}
