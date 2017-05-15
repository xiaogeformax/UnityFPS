/*
2017-5-4 17:12:07
@喵喵Mya
支持从正常过度到透明扭曲的shader

ps.纯粹写来玩的，移动端用说不定会爆炸哦，别乱用！
*/

Shader "Mya/stealth"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_NorPow("Normal Power" , Range(0,1)) = 0.5//法线扰动强度
		_blend("blend" , Range(0,1)) = 0.5//混合
	}
	SubShader
	{
		Tags { "RenderType"="Transparent""IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 100


		GrabPass {} 


		Pass
		{
		Tags { "LightMode" = "ForwardBase"  }

			

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 normal : NORMAL;
				
			};

			struct v2f
			{
				float4 uv_Grab : TEXCOORD0; 
				float2 uv_tex : TEXCOORD1;
				float4 pos : SV_POSITION;

			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _GrabTexture; 
			float4 _GrabTexture_ST;
			float _NorPow ,_blend;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);  

				o.uv_Grab = ComputeGrabScreenPos(o.pos);
				half3 worldNormal  = normalize(mul(v.normal, (float3x3)unity_WorldToObject)); 
				half3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz; 
				half3 worldViewDir =normalize( _WorldSpaceCameraPos.xyz - worldPos); 
				float r = ((1 - dot(worldViewDir, worldNormal))*2-1) * _NorPow;
				o.uv_Grab += r;
				o.uv_tex = TRANSFORM_TEX(v.uv, _MainTex);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

				half4 texcol = tex2D(_MainTex, i.uv_tex);
				half4 Grabcol = tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(i.uv_Grab));
				half3 c = lerp(texcol.rgb , Grabcol.rgb,_blend);

				return fixed4(c,1);
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
