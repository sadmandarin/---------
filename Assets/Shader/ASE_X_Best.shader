Shader "ASE_X/Best" {
	Properties {
		_Color ("Color", Vector) = (0.5019608,0.5019608,0.5019608,0.5019608)
		_Main_Tex ("Main_Tex", 2D) = "white" {}
		_Main_U ("Main_U", Float) = 0
		_Main_V ("Main_V", Float) = 0
		_Mask_01 ("Mask_01", 2D) = "white" {}
		_Mask_U ("Mask_U", Float) = 0
		_Maks_V ("Maks_V", Float) = 0
		_SoftDissolveTex ("SoftDissolveTex", 2D) = "white" {}
		_SoftDissolveIndensity ("SoftDissolveIndensity", Range(0, 1.05)) = 0
		_SoftDissolveSoft ("SoftDissolveSoft", Float) = 0.5
		_Dissolve_U ("Dissolve_U", Float) = 0
		_Dissolve_V ("Dissolve_V", Float) = 0
		_Indensity ("Indensity", Float) = 1
		_Opacity ("Opacity", Float) = 1
		[HideInInspector] _texcoord ("", 2D) = "white" {}
		[HideInInspector] __dirty ("", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
	//CustomEditor "ASEMaterialInspector"
}