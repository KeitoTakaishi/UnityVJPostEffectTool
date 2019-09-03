Shader "Hidden/Feedback"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_renderTexture1("RenderTexture1", 2D) = "white" {}
		_renderTexture2("RenderTexture2", 2D) = "white" {}
		_renderTexture3("RenderTexture3", 2D) = "white" {}
		_renderTexture4("RenderTexture4", 2D) = "white" {}
		_renderTexture5("RenderTexture5", 2D) = "white" {}
		_renderTexture6("RenderTexture6", 2D) = "white" {}
		_renderTexture7("RenderTexture7", 2D) = "white" {}
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
			sampler2D _renderTexture1;
			sampler2D _renderTexture2;
			sampler2D _renderTexture3;
			sampler2D _renderTexture4;
			sampler2D _renderTexture5;
		
			float _height;
			float _width;
			float _fineness;
			float _frequence;
			float _amp;

            fixed4 frag (v2f i) : SV_Target
            
			{
				float2 uv = i.uv;
				
				float noise = simplexNoise(float3(uv.x*_fineness, uv.y*_fineness, _Time.y*_frequence));
				noise = noise * 2.0 - 1.0;
				uv = i.uv;
				uv.x += noise * _amp;
				uv.y += noise * _amp;
                fixed4 col = tex2D(_renderTexture1, uv);
				col += tex2D(_renderTexture2, uv);
				col += tex2D(_renderTexture3, uv);
				col += tex2D(_renderTexture4, uv);
				col += tex2D(_renderTexture5, uv);
				col /= 5.0;
               
                return col;
            }
            ENDCG
        }
    }
}
