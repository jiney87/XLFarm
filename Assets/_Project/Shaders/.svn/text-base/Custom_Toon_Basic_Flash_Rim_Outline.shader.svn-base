Shader "Custom/Toon/Basic/Flash_Rim_Outline" {
	Properties {
		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { }

		_FlashColor("闪光",Color) = (1,1,1,1)
		_FlashDiff("_FlashDiff（闪光程度）",range(0,1))=0

		_RimColor("边缘光",Color) = (1,1,1,1)
		_RimDiff("_RimDiff（边缘光程度）",range(0,1))=0

		_OutlineColor ("描边颜色", Color) = (0,0,0,1)
		_Outline ("_Outline（描边宽度）", Range (0, 0.005)) = .005
	}


	SubShader {
		Tags { "RenderType"="Opaque" }
		Pass {
			//Name "CUSTOM_TOON_BASE"
			//Cull Off
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			samplerCUBE _ToonShade;
			fixed4 _MainTex_ST;
			fixed4 _Color;

			fixed4 _FlashColor;
			fixed _FlashDiff;

			fixed4 _RimColor;
			fixed _RimDiff;

			struct appdata {
				fixed4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;//UV用半精度
				fixed3 normal : NORMAL;
			};
			
			struct v2f {
				fixed4 pos : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				fixed3 cubenormal : TEXCOORD1;

				fixed4 color : Color;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.cubenormal = mul (UNITY_MATRIX_MV, fixed4(v.normal,0));
				//染色
				o.color= _FlashColor*_FlashDiff;
				//边缘光
				fixed3 viewDir=normalize(ObjSpaceViewDir(v.vertex));
				fixed dotValue=1-saturate(dot(v.normal,viewDir));
				o.color+=(1-step(dotValue,_RimDiff))*_RimColor;

				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = _Color * tex2D(_MainTex, i.texcoord);
				fixed4 cube = texCUBE(_ToonShade, i.cubenormal);
				fixed4 c = fixed4(2.0f * cube.rgb * col.rgb   + i.color.rgb    , col.a);
				return c;
			}
			ENDCG			
		}

		//延法线方向
		Pass{

			//Name "OUTLINE"
			//Tags { "LightMode" = "Always" }
			Cull Front
			ZWrite On
			ColorMask RGB
			//Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata {
				fixed4 vertex : POSITION;
				fixed3 normal : NORMAL;
			};

			struct v2f {
				fixed4 pos : SV_POSITION;
				fixed4 color : COLOR;
			};

			fixed _Outline;
			fixed4 _OutlineColor;

			v2f vert(appdata v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

				fixed3 norm   = normalize(mul ((fixed3x3)UNITY_MATRIX_IT_MV, v.normal));
				fixed2 offset = TransformViewToProjection(norm.xy);

				#ifdef UNITY_Z_0_FAR_FROM_CLIPSPACE //to handle recent standard asset package on older version of unity (before 5.5)
					o.pos.xy += offset * UNITY_Z_0_FAR_FROM_CLIPSPACE(o.pos.z) * _Outline;
				#else
					o.pos.xy += offset * o.pos.z * _Outline;
				#endif
				o.color = _OutlineColor;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return i.color;
			}
			ENDCG
		}
	}

	Fallback "VertexLit"
}
