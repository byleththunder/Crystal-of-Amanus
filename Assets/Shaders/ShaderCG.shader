Shader "Custom/ShaderCG" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_SecTex ("Secundary (RGB)", 2D) = "white" {}
		_Power("Potencia",Float) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		
		#pragma surface surf Lambert


		sampler2D _MainTex;
		sampler2D _SecTex;
		half _Power;
		
		struct Input {
			float2 uv_MainTex;
			float2 uv_SecTex;
		};

		

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			half4 c2 = tex2D (_SecTex, IN.uv_SecTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.Emission = c2.rgb * _Power * (_SinTime.w+1);
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
