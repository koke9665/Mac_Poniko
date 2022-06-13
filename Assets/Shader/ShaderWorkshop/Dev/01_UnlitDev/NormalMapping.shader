// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlite/NormalMapping"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_NormalTex("NormalTexture", 2D) = "white" {}
		_DiffuseColor("Diffuse Color",Color) = (1,1,1,1)
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
			#pragma multi_compile_fog
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float3 tangent : TANGENT0;
				float texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float3 light : COLOR1;
				float texcoord : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			uniform float4 light_pos;
			uniform float k_diffuse;
			uniform float k_ambient

            float4x4 InvTangentMatrix(
				float3 tangent,
				float3 binormal,
				float3 normal )
            {
				float4x4 mat = float4x4(float4(tangent.x,tangent.y,tangent.z , 0.0f),
								float4(binormal.x,binormal.y,binormal.z, 0.0f),
								float4(normal.x,normal.y,normal.z, 0.0f),
								float4(0,0,0,1)
								);
				return transpose( mat ); 
            }
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				float3 n = normalize(v.normal);
				float3 t = normalize(v.tangent);
				float3 b = cross(n,t);

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float3 normal = float4(UnpackNormal(tex2D(_NormalTex,i,texcoord)),1);
				float3 lightvec = normalize(i.light.xyz);
				float diffuse = max(0,dot(normal,lightvec));
				return diffuse * tex2D(_MainTex,i.uv)
			}
			ENDCG
		}
	}
}
