Shader "Hidden/ScanLine"
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
			#pragma multi_compile _  HORIZONTAL VERTICAL
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
			float max;
            fixed4 frag (v2f i) : SV_Target
            {
				float2 uv = i.uv;
			#ifdef HORIZONTAL
				uv = float2(min(uv.x, max), uv.y);
			#elif VERTICAL
				uv = float2(uv.x, min(uv.y, max));
			#elif _
			
			#endif
                fixed4 col = tex2D(_MainTex, uv);
               
                return col;
            }
            ENDCG
        }
    }
}
