Shader "Hidden/GridFlash"
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

            fixed4 frag (v2f i) : SV_Target
            {
				int step = 5;
				fixed4 cols[25];
				float2 uv = float2(i.uv);//vec2(min(max(0.0,pos.x),1.0),min(max(0.0,pos.y),1.0));

				for (int i = 0; i < step; i++) {
					for (int j = 0; j < step; j++) {
						cols[i*step + j] = tex2D(_MainTex,float2((uv.x + float(j) - 1.0), (uv.y + float(i) - 1.0)));
						cols[i*step + j].r = (cols[i*step + j].r + cols[i*step + j].g + cols[i*step + j].b) / 3.0;
					}
				}

				float mn = 1.0;
				float mx = 0.0;
				for (int i = 0; i < step*step; i++) {
					mn = min(cols[i].r,mn);
					mx = max(cols[i].r,mx);
				}
				float dst = abs(1.0 - (mx - mn));

				fixed4 col = float4(float3(float3(dst, dst, dst) + cols[12].rgb),1.0);

				return col;
            }
            ENDCG
        }
    }
}
