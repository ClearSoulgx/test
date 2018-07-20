Shader "GP/Sjw/animate_1"
{
	Properties
	{
		
	_Outline ("Outline",Range(0,1)) = 0.1
		_OutlineColor ("Outline Color" ,Color) =(0,0,0,1)

		_MainTex("Texture", 2D) = "white" {}
		_Color("Color tint",Color) = (1,1,1,1)
		_Ramp("Ramp Texture", 2D) = "white" {}
		_Specular("Specular", Color) = (1, 1, 1, 1)
		_Threshold("Specular threshold", Range(0.6,1)) = 0.98
		//	_SpecularScale("Specular Scale", Range(0, 0.1)) = 0.01
	}
	SubShader
	{
		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry" }
		//LOD 100

		Pass
		{	
			NAME "OUTLINE"

			Cull Front
			//ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			//#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				//float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				//float2 uv : TEXCOORD0;
				
				float4 vertex : SV_POSITION;
			};

			//sampler2D _MainTex;
			//float4 _MainTex_ST;
			float _Outline;
			fixed4 _OutlineColor;
			
			v2f vert (appdata v)
			{
				v2f o;

				float4 pos = mul(UNITY_MATRIX_MV, v.vertex);
				//float4 pos = UnityObjectToViewPos(v.vertex);
				float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
				//if (normal.z > 0) normal.z = -normal.z;
				normal.z = -0.5;
				pos = pos + float4(normalize(normal), 0)*_Outline;
				o.vertex = mul(UNITY_MATRIX_P, pos);
				//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				//UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				//fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
				//return col;
				return float4(_OutlineColor.rgb,1);
			}
				ENDCG
	}

		Pass{
				Tags {"LightMode" = "ForwardBase"}

				Cull Back

				CGPROGRAM

#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_fwdbase

#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"
#include "UnityShaderVariables.cginc"

				fixed4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;		//use in macro TRANSFER_SHADOW
			sampler2D _Ramp;
			fixed4 _Specular;
			fixed _Threshold;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f {
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
				float3 worldNormal : NROMAL;
				//float3 worldPos : POSITION;
				float3 worldPos : TEXCOORD1;
				SHADOW_COORDS(2)
			};


			v2f vert(appdata v) {		//force use name v because macro TRANSFER_SHADOW ---- mul(_Object2world,v.vertex);
				v2f o;

				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

				TRANSFER_SHADOW(o);
				return o;
			}

			float4 frag(v2f i) : SV_Target{
				fixed3 worldNormal = normalize(i.worldNormal);
				fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
				fixed3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
				fixed3 worldHalfDir = normalize(worldLightDir + worldViewDir);		//blinn

				fixed4 cor = tex2D(_MainTex, i.uv);
				//fixed3 albedo = (cor.rgb+_Color.rgb)/2.0;
				fixed3 albedo = (cor.rgb *_Color.rgb) ;

				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz*albedo;
				
				UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);
				fixed h_lam = dot(worldNormal, worldLightDir)*0.5 + 0.5;
				h_lam *= atten;
				//TODO
				fixed3 diffuse = _LightColor0.rgb*albedo*tex2D(_Ramp, float2(h_lam, h_lam)).rgb;

				fixed spec = dot(worldNormal, worldHalfDir);
				//fixed w = 0.01;
				fixed w = fwidth(spec) * 3.0;
				spec = lerp(0, 1, smoothstep(-w, w, spec - _Threshold))*step(0.0001, 1 - _Threshold);
				fixed3 specular = _Specular.rgb * spec;

				return fixed4(ambient + diffuse + specular, 1.0);
				//return fixed4(ambient , 1.0);
			
			
			}
				ENDCG
			}

			
			/*Pass{
				Tags{ "LightMode" = "ForwardBase" }

				Cull Back

				CGPROGRAM

#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_fwdbase

#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"
#include "UnityShaderVariables.cginc"

				fixed4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;		//use in macro TRANSFER_SHADOW
			sampler2D _Ramp;
			fixed4 _Specular;
			fixed _Threshold;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f {
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
				float3 worldNormal : NROMAL;
				//float3 worldPos : POSITION;
				float3 worldPos : TEXCOORD1;
				SHADOW_COORDS(2)
			};


			v2f vert(appdata v) {		//force use name v because macro TRANSFER_SHADOW ---- mul(_Object2world,v.vertex);
				v2f o;

				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

				TRANSFER_SHADOW(o);
				return o;
			}
			float4 frag(v2f i) : SV_Target{
				fixed3 worldNormal = normalize(i.worldNormal);
			fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
			fixed3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
			fixed3 worldHalfDir = normalize(worldLightDir + worldViewDir);		//blinn

			fixed4 cor = tex2D(_MainTex, i.uv);
			fixed3 albedo = cor.rgb*_Color.rgb;


			fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz*albedo;
			
			UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);
			fixed h_lam = dot(worldNormal, worldLightDir)*0.5 + 0.5;
			h_lam *= atten;
			//TODO
			fixed3 diffuse = _LightColor0.rgb*albedo*tex2D(_Ramp, float2(h_lam, h_lam)).rgb;

			fixed spec = dot(worldNormal, worldHalfDir);
			//fixed w = 0.01;
			fixed w = fwidth(spec) * 3.0;
			spec = lerp(0, 1, smoothstep(-w, w, spec - _Threshold))*step(0.0001, 1 - _Threshold);
			fixed3 specular = _Specular.rgb * spec;
			return fixed4(ambient + diffuse + specular, 1.0);
			}

				ENDCG
			}*/


	}
	FallBack "Diffuse"
}
