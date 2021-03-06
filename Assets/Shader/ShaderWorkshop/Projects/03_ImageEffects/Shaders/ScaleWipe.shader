// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/ScreenGlitch"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			#define M_2PI (6.28318530717958)

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _Radius;

			fixed4 frag (v2f i) : SV_Target
			{
				float aspect = _ScreenParams.x * (_ScreenParams.w - 1.0);
				float maxLength = length(float2(0.5, 0.5 / aspect));
				
				i.uv -= (float2)0.5;
				i.uv.y /= aspect;
				float div = max(_Radius, length(i.uv) / maxLength);
				i.uv = _Radius * i.uv / div;
				i.uv.y *= aspect;
				i.uv += (float2)0.5;

				return tex2D(_MainTex, i.uv);
			}
			ENDCG
		}
	}
}
