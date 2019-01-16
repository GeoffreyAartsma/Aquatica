Shader "Grid/Standard"
{
	Properties
	{
		_BaseColor("Base Color", Color) = (1,1,1,1)
		_GridColor("Grid color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_CellSize("Cell size", Float) = 10 
		_GridSize("Total Grid size", Int) = 100
		_LineWidth("Line width", Float) = 1
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input
		{
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _BaseColor;
		fixed4 _GridColor;
		float _CellSize;
		float _LineWidth;
        int _GridSize;

		void surf(Input IN, inout SurfaceOutputStandard o)
 		{
			// Albedo comes from a texture tinted by color
			fixed4 col = tex2D(_MainTex, IN.uv_MainTex) * _BaseColor;

			// grid overlay
			float2 pos = (float)_GridSize / _CellSize * IN.uv_MainTex;
			float2 wrapped = frac(pos);
            float2 speeds = fwidth(pos);

            float2 pixelRange = wrapped/speeds;
            float lineWeight = saturate(min(pixelRange.x, pixelRange.y) - _LineWidth);
			col.rgb = lerp(_GridColor, col, lineWeight);

			o.Albedo = col.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = col.a;
		}
		ENDCG
	}

	FallBack "Diffuse"
}
