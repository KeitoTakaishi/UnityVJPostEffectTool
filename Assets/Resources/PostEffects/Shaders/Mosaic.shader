Shader "Hidden/Mosaic"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
			float _blockSize;
			float _height;
			float _width;

			float tFrag;
			float nFrag;
            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 destColor = float4(0.0, 0.0, 0.0, 1.0);
				float2 uv = i.uv;
				float2 res = uv * float2(_width, _height);

				float offsetX = fmod(res.x, _blockSize);
				float offsetY = fmod(res.y, _blockSize);
				
				
				nFrag = 1.0 / pow(_blockSize ,2.0);

				for (float x = 0.0; x < _blockSize; x += 1.0) {
					for (float y = 0.0; y < _blockSize; y += 1.0) {
						destColor += tex2D(_MainTex, (res + float2(x - offsetX, y - offsetY)) * float2(1.0/_width, 1.0/_height));
					}
				}
                
                return destColor * nFrag;
            }
            ENDCG
        }
    }
}
