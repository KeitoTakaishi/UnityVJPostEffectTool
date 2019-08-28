Shader "Hidden/RGBShift"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_offSet1("offSet1", Float) = 0.0
		_offSet2("offSet", Float) = 0.0

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

			float _offSet1;
			float _offSet2;

            fixed4 frag (v2f i) : SV_Target
            {
				float2 uv = i.uv;
                fixed4 col = tex2D(_MainTex, uv);
				
				col.r = tex2D(_MainTex, uv + float2(_offSet1, 0.0)).r;
				
				col.b = tex2D(_MainTex, uv + float2(_offSet2, 0.0)).b;
				return col;
            }
            ENDCG
        }
    }
}