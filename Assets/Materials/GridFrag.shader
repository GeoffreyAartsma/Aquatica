Shader "Grid/Unlit"
{
	Properties
	{
		_GridColour ("Grid Colour", Color) = (1, 1, 1, 1)
		_BaseColour("Base Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_GridSpacing("Grid spacing", Float) = 10
		_LineWidth("Grid width", Float) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _BaseColour;
			fixed4 _GridColour;
			float _GridSpacing;
			float _LineWidth;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv) * _BaseColour;

				float2 pos = 10.0 /_GridSpacing * i.uv;
				float2 wrapped = frac(pos);
                float2 speeds = fwidth(pos);

                float2 pixelRange = wrapped/speeds;
                float lineWeight = saturate(min(pixelRange.x, pixelRange.y) - _LineWidth);

                // apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);

                return lerp(_GridColour, col, lineWeight);
			}
			ENDCG
		}
	}
}
