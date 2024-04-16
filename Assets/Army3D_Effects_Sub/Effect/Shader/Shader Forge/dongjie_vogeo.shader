Shader "Shader Forge/dongjie_vogeo" {
	Properties {
		_BenTi ("BenTi", 2D) = "white" {}
		_BenTiColor ("BenTiColor", Vector) = (1,1,1,1)
		_BenTi_normal ("BenTi_normal", 2D) = "bump" {}
		_normal02 ("normal02", Range(0, 2)) = 2
		_xiaosan_texture ("xiaosan_texture", 2D) = "white" {}
		_xiosan ("xiosan", Range(0, 1.5)) = 0.3076923
		_xiaosan_color ("xiaosan_color", Vector) = (0.4078432,0.4627451,0.5019608,1)
		_xiaosan_bian ("xiaosan_bian", Range(0, 0.2)) = 0.03658742
		_bian_color ("bian_color", Vector) = (0.7490196,0.7490196,0.7490196,1)
		_Bing_wenli ("Bing_wenli", 2D) = "white" {}
		_waifaguang_bian ("waifaguang_bian", Range(0, 2)) = 2
		_waifaguang ("waifaguang", Vector) = (0.4196079,0.6862745,1,1)
		_Normal_bingwenli ("Normal_bingwenli", 2D) = "bump" {}
		_Normal_bingwenli_qiangdu ("Normal_bingwenli_qiangdu", Range(0, 2)) = 2
		_specular ("specular", Range(0, 2)) = 1.579953
		_gloss ("gloss", Range(0, 1)) = 0.4330766
		_tuqi ("tuqi", Range(0, 1)) = 0.1335529
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Diffuse"
	//CustomEditor "ShaderForgeMaterialInspector"
}