Shader "Grid/Standard"
{
	Properties
	{
		_BaseColor("Base Color", Color) = (1,1,1,1)
		_GridColor("Grid color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_GridSpacing("Grid size", Float) = 10
		_LineWidth("Line width", Float) = 1
		_SelectionColor("Color selection", Color) = (1,0,0,0)
		_SelectedArea("Selected area (xy are minumum, zw are maximum", Vector) = (0.1, 0.1, 0.1, 0.1)
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
		float _GridSpacing;
		float _LineWidth;
		fixed4 _SelectionColor;
		half4 _SelectedArea;

		void surf(Input IN, inout SurfaceOutputStandard o)
 		{
			// Albedo comes from a texture tinted by color
			fixed4 background_color = tex2D(_MainTex, IN.uv_MainTex) * _BaseColor;

			// Selection
			half2 bl = step(_SelectedArea.xy, IN.uv_MainTex);
			half2 tr = (1.0 + step(_SelectedArea.zw / 10.0, IN.uv_MainTex)) * -1.0 + 2.0;
			fixed4 col = lerp(background_color, _SelectionColor, bl.x * bl.y * tr.x * tr.y);

			// grid overlay
			half2 pos = 10.0 /_GridSpacing * IN.uv_MainTex;
			half2 wrapped = frac(pos);
            half2 speeds = fwidth(pos);

            half2 pixelRange = wrapped/speeds;
            half lineWeight = saturate(min(pixelRange.x, pixelRange.y) - _LineWidth);
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
