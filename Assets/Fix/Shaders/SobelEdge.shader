Shader "Hidden/SobelEdge"
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
			float4 _MainTex_TexelSize;
			float _coef[9];
            fixed4 frag (v2f i) : SV_Target
            {
				float2 uv = i.uv;
				for (float i = 0.0; i < 9.0; i += 1.0) {
					_coef[(int)i] = 1.0;
				}
				_coef[4] = -8.0;

				float _width = _ScreenParams.x;
				float _height = _ScreenParams.y;

				float2 offset[9];
				offset[0] = float2(-1.0, -1.0);
				offset[1] = float2(0.0, -1.0);
				offset[2] = float2(1.0, -1.0);
				offset[3] = float2(-1.0,  0.0);
				offset[4] = float2(0.0,  0.0);
				offset[5] = float2(1.0,  0.0);
				offset[6] = float2(-1.0,  1.0);
				offset[7] = float2(0.0,  1.0);
				offset[8] = float2(1.0,  1.0);

				fixed4 col = fixed4(0.0, 0.0, 0.0, 0.0);


				float2 fc = uv * float2(_width, _height);
				float2 tFrag = float2(1.0 / _width, 1.0 / _height);
				col += tex2D(_MainTex, (fc + offset[0]) * tFrag) * _coef[0];
				col += tex2D(_MainTex, (fc + offset[1]) * tFrag) * _coef[1];
				col += tex2D(_MainTex, (fc + offset[2]) * tFrag) * _coef[2];
				col += tex2D(_MainTex, (fc + offset[3]) * tFrag) * _coef[3];
				col += tex2D(_MainTex, (fc + offset[4]) * tFrag) * _coef[4];
				col += tex2D(_MainTex, (fc + offset[5]) * tFrag) * _coef[5];
				col += tex2D(_MainTex, (fc + offset[6]) * tFrag) * _coef[6];
				col += tex2D(_MainTex, (fc + offset[7]) * tFrag) * _coef[7];
				col += tex2D(_MainTex, (fc + offset[8]) * tFrag) * _coef[8];

				return col;
            }
            ENDCG
        }
    }
}
