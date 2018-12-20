Shader "Custom/WallStencil" 
{
   	Properties
	{
		_MyTexture("Select Texture",2D) = "white"{}	
	}

    SubShader 
	{
        Tags 
		{
		    "RenderType"="Opaque"
            "Queue"="Geometry+1"
        }

		//LOD 100
		//Blend SrcAlpha OneMinusSrcAlpha
		//ZWriteOff
		//Culloff

		CGPROGRAM
		
			#pragma surface surf Lambert 

			sampler2D _MyTexture;

			struct Input 
			{
				float2 uv_MyTexture;
			};

			void surf (Input IN, inout SurfaceOutput o) 
			{
				o.Albedo = tex2D(_MyTexture,IN.uv_MyTexture).rgb;
			}
		ENDCG
     
		Pass 
		{       
			Stencil 
			{
                Ref 1
                Comp Equal
            }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);

				return o;
			}

			half4 frag(v2f i) : SV_Target
			{
				return half4(0,0,0,1);
			}
	
			ENDCG
		}       
	}
}
