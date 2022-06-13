Shader "Custom/UV Debug"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SubTex("SubTexture",2D)="white"{}
		_lengh("lengh",float)=1
		_Fps("Fps",float)=24
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
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float2 puv: TEXCOORD1;
				float4 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 puv: TEXCOORD1;
				UNITY_FOG_COORDS(1)
				float4 vertex : POSITION;
				float4 normal : NORMAL;
			};

			sampler2D _MainTex;
			sampler2D _SubTex;
			float4 _MainTex_ST;
			float4 _SubTex_ST;
			float _lengh;
			
			v2f vert (appdata v)
			{
				v2f o;
				v.vertex.x += _lengh * (tan(float2(v.uv.y, floor(_Time.y * _lengh) / _lengh)) - 0.5);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv,_MainTex);
				UNITY_TRANSFER_FOG(o,o.vertexer);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
