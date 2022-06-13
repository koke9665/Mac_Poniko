// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/OutlineDev"
{
	Properties
	{
		_Color ("MainColor",Color)=(1,1,1,1)
		_LineColor("LineColor",Float)=1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		Cull front

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
				float4 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			float4 _Color;
			float _LineColor;
			
			v2f vert (appdata v)
			{
				v2f o;
				v.vertex.xyz += v.normal.xyz * _LineColor;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

				return float4(_Color.xyz,1);

			}
			ENDCG
		}
	}
}
