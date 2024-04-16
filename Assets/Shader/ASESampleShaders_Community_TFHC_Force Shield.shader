Shader "ASESampleShaders/Community/TFHC/Force Shield" {
	Properties {
		[HideInInspector] __dirty ("", Float) = 1
		_Color ("Color", Vector) = (0,0,0,0)
		_Albedo ("Albedo", 2D) = "white" {}
		_Normal ("Normal", 2D) = "bump" {}
		_Opacity ("Opacity", Range(0, 1)) = 0.5
		_ShieldPatternColor ("Shield Pattern Color", Vector) = (0.2470588,0.7764706,0.9098039,1)
		_ShieldPattern ("Shield Pattern", 2D) = "white" {}
		[IntRange] _ShieldPatternSize ("Shield Pattern Size", Range(1, 20)) = 5
		_ShieldPatternPower ("Shield Pattern Power", Range(0, 100)) = 5
		_ShieldRimPower ("Shield Rim Power", Range(0, 10)) = 7
		_ShieldAnimSpeed ("Shield Anim Speed", Range(-10, 10)) = 3
		_ShieldPatternWaves ("Shield Pattern Waves", 2D) = "white" {}
		_ShieldDistortion ("Shield Distortion", Range(0, 0.03)) = 0.01
		_IntersectIntensity ("Intersect Intensity", Range(0, 1)) = 0.2
		_IntersectColor ("Intersect Color", Vector) = (0.03137255,0.2588235,0.3176471,1)
		_HitPosition ("Hit Position", Vector) = (0,0,0,0)
		_HitTime ("Hit Time", Float) = 0
		_HitColor ("Hit Color", Vector) = (1,1,1,1)
		_HitSize ("Hit Size", Float) = 0.2
		[HideInInspector] _texcoord ("", 2D) = "white" {}
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
	Fallback "Diffuse"
	//CustomEditor "ASEMaterialInspector"
}