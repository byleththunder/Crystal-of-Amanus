Shader "Custom/Crystal" {
	Properties {
		_Color ("Color", Color) = (0.5,0.5,0.5,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_SpecularMap("Specular",2D) = "white" {}
		_Cube("Reflection Cubemap", Cube) = "_Skybox" {TexGen CubeReflect}
		_ReflectColour("Reflection Colour", Color) = (1,1,1,0.5)
		_ReflectionBrightness("Reflection Brightness", Float) = 1.0
		_RimColour("Rim Colour", Color) = (0.26,0.19,0.16,0.0)
	}
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf BlinnPhong alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _SpecularMap;
		samplerCUBE _Cube;
		fixed4 _Color;
		fixed4 _ReflectColour;
		fixed4 _RimColour;
		fixed _ReflectionBrightness;
		
		fixed _Alfa;
		struct Input {
			float2 uv_MainTex;
			float3 worldRefl;
			float3 viewDir;
		};

		
		
		
		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			half4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			//Alpha
			o.Alpha = _Color.a * c.a;
			//Specular map
			half Spec = tex2D(_SpecularMap,IN.uv_MainTex).r;
			o.Specular = Spec;
			//Reflection Map
			fixed4 reflcol = texCUBE(_Cube, IN.worldRefl);
			reflcol *= c.a;
			o.Emission = reflcol.rgb * _ReflectColour.rgb * _ReflectionBrightness;
			o.Alpha = o.Alpha * _ReflectColour.a;
			//Rim - Luz emitida baseada nos angulos entre a superfice da normal e a direcao
			half intensity = 1.0 - saturate(dot (normalize(IN.viewDir),o.Normal));
			o.Emission += intensity * _RimColour.rgb;
			
			
		
			
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
