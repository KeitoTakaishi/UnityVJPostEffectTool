﻿Shader "Hidden/VerticalSymmetry"
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
			#define PI 3.14

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

			float2x2 rotate(float a) {
				float s = sin(a), c = cos(a);
				return float2x2(c, s, -s, c);
			}


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
			   float2 uv = i.uv;
			 
			   uv = 2.0 * uv - 1.0;
			   uv.x = abs(uv.x);
			   float theta = fmod(_Time.y, 180.0);
			   uv = mul(uv, rotate(-1.0*theta));
			   uv = (uv + float2(1.0, 1.0))/2.0;

			
			   

			   
			   /*
			   uv = uv.* 2.0 - float2(1.0, 1.0);
			   //uv = abs(uv);
			   for (int i = 0; i < 2; i++) {
				   //uv = abs(uv * 1.5) - float2(1.0, 1.0);
				   //uv = mul(uv, rotate(_Time.y));
				  
			   }
			   */

			   //uv.x = uv.x * 2.0 - 1.0;
			//uv.x = abs(uv.x);
			   //uv.x = (uv.x * 1.5) - 1.0;
			  // uv = mul(uv, rotate(-_Time.y));
			   fixed4 col = tex2D(_MainTex, uv);
			   return col;
            }
            ENDCG
        }
    }
}
