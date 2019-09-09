Shader "Hidden/PixleSorting"
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
			#include "noise4D.cginc"

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


			//-------------------------------------------------------------
			float rand(float v) {
				return frac(sin(dot(v.x, 12.9898)) * 43758.5453);
			}
			
			float rand2D(float2 co) {
				return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
			}

			float rand3D(float3 co) {
				return frac(sin(dot(co.xyy, float3(12.9898, 78.233, 34.32074))) * 43758.5453);
			}


			float3 snoise3D(float4 x)
			{
				float s = snoise(x);
				float s1 = snoise(float4(x.y - 19.1, x.z + 33.4, x.x + 47.2, x.w));
				float s2 = snoise(float4(x.z + 74.2, x.x - 124.5, x.y + 99.4, x.w));
				float3 c = float3(s, s1, s2);
				return c;
			}

			float3 curlNoise(float4 p) {

				const float e = 0.0009765625;
				float4 dx = float4(e, 0.0, 0.0, 0.0);
				float4 dy = float4(0.0, e, 0.0, 0.0);
				float4 dz = float4(0.0, 0.0, e, 0.0);

				float3 p_x0 = snoise3D(p - dx);
				float3 p_x1 = snoise3D(p + dx);
				float3 p_y0 = snoise3D(p - dy);
				float3 p_y1 = snoise3D(p + dy);
				float3 p_z0 = snoise3D(p - dz);
				float3 p_z1 = snoise3D(p + dz);

				float x = p_y1.z - p_y0.z - p_z1.y + p_z0.y;
				float y = p_z1.x - p_z0.x - p_x1.z + p_x0.z;
				float z = p_x1.y - p_x0.y - p_y1.x + p_y0.x;

				const float divisor = 1.0 / (2.0 * e);
				return normalize(float3(x, y, z) * divisor);
			}
			//-------------------------------------------------------------

            sampler2D _MainTex;
            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 destColor;
				float2 uv = i.uv;
                fixed4 c = tex2D(_MainTex, uv);
				
				float mono = clamp(length(c.rgb), 0.0, 1.0);

                
				destColor = fixed4(mono, mono, mono, 1.0);

				uv.y = frac(_Time.x*0.0 + uv.y);
				//float2 blockSize = float2(int(10.0 * rand( float2(floor(uv.x*50.0)/50.0, uv.y))), 1500.0);


				//sizeにoffSetをつけている
				float slide = clamp(sin(_Time.y*1.0 + uv.y*100.0), 0.0, 1.0);
				float2 baseBlockSize = float2(slide + 5.0 + 10.0*rand(floor((uv.y)*700.0) / 700.0), 300.0);

				////float2 blockSize = float2(4.0 + 1.0*sin(uv.y*100.0 + _Time.y*1.0), 300.0*+100.0*sin(_Time.y*0.3));
				float2 blockSize = float2(baseBlockSize.x, baseBlockSize.y);

				uv = floor(uv * blockSize) / blockSize;
				
				if (mono > 0.9 + 0.05*sin(_Time.y)) {
					
					float offSetX = frac(uv.x + floor(i.uv.y*10.0) / 10.0);
					float3 noise = curlNoise(float4( uv.x + offSetX, uv.y, 1.0,_Time.x*0.0));
					//float l = length(noise - nextNoise);
				
					destColor.rgb = noise;

					
					float finnes = 100.0;
					noise = curlNoise(float4(noise.x * finnes, noise.y * finnes, noise.z * finnes, _Time.y*0.0));
					//for debug
					//destColor.rgb = noise;
					

					//uv.x = frac(uv.x * length(noise))/ length(noise);
					
					//float power = smoothstep(frac(_Time.x * 0.2), 1.0, baseBlockSize.x);
					float power = lerp(3.0, baseBlockSize.x, frac(_Time.y*0.3));
					uv.x = floor(i.uv.x * power)/ power;
					power = 0.03;
					uv = uv + float2(length(noise) * power, 0.0);
					
					destColor = tex2D(_MainTex, uv);
				}
				else {
						destColor = tex2D(_MainTex, i.uv);
						float d = 0.005;

						destColor += tex2D(_MainTex, i.uv + float2(-d, 0.0));
						destColor += tex2D(_MainTex, i.uv + float2(d, 0.0)) ;
						destColor += tex2D(_MainTex, i.uv + float2(0.0, d));
						destColor += tex2D(_MainTex, i.uv + float2(0.0, -d));
						destColor /= 5.0;
				}

                return destColor;
            }
            ENDCG
        }
    }
}
