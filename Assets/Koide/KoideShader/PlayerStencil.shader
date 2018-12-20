
Shader "Custom/PlayerStencil" 
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MyTexture("Texture",2D) = "white"{}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}

	SubShader 
	{
        Tags 
		{
			//"Queue" = "Geometry"
		    "RenderType"="Opaque"
        }

		LOD 100
		
        Pass 
		{ 
			Stencil 
			{
                Ref 1
                Comp Always
				Pass Replace
            }

			CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
			#pragma multi_compile_fog

			#include "UnityCG.cginc"		

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv  : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MyTexture;
			float4 _MyTexture_ST;
			half _Glossiness;
			half _Metallic;
			fixed4 _Color;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv,_MyTexture);
				UNITY_TRANSFER_FOG(o,o.vertex);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MyTexture,i.uv);

				UNITY_APPLY_FOG(i.fogCoord,col);
				return col;
			}
			
			ENDCG
		}       
	}
}
