Shader "Custom/Outline"
{
    Properties
    {
        _MainTex ("Main Texture (RBG)", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 0)

		_OutlineTex("Outline Texture", 2D) = "white" {}
		_OutlineColor("Outline Color", Color) = (1, 1, 1, 1)
		_OutlineWidth("Outline Width", Range(1.0, 10.0)) = 1.1
    }

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
		}

		GrabPass
		{
			"_BackgroundTexture"
		}

		Pass
		{
			Name "OUTLINE"
			ZWrite Off
			CGPROGRAM
				//Define for the building function
				#pragma vertex vert
				
				//Define for the coloring function
				#pragma fragment frag

				//Built-in shader functions
				#include "UnityCG.cginc"

				//How the vertex function receives info
				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};//End appdata
				
				//How the fragment function receives info
				struct v2f
				{
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
				};//End v2f

				//Reimport properties into the pass
				float _OutlineWidth;
				float4 _OutlineColor;
				sampler2D _OutlineTex;

				//Vertex function
				v2f vert(appdata IN)
				{
					IN.vertex.xyz *= _OutlineWidth;
					v2f OUT;

					OUT.pos = UnityObjectToClipPos(IN.vertex);
					OUT.uv = IN.uv;

					return OUT;
				}//End vert

				//Fragment function
				fixed4 frag(v2f IN) : SV_Target
				{
					//Wraps the texture around the UVs
					float4 texColor = tex2D(_OutlineTex, IN.uv);
					//Tints the texture
					return texColor * _OutlineColor;
				}//End frag
			ENDCG
		}

		Pass
		{
			Name "OBJECT"
			CGPROGRAM
			//Define for the building function
			#pragma vertex vert

			//Define for the coloring function
			#pragma fragment frag

			//Built-in shader functions
			#include "UnityCG.cginc"

			//How the vertex function receives info
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};//End appdata

			//How the fragment function receives info
			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};//End v2f

			//Reimport properties into the pass
			float4 _Color;
			sampler2D _MainTex;

			//Vertex function
			v2f vert(appdata IN)
			{
				v2f OUT;

				OUT.pos = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;

				return OUT;
			}//End vert

			//Fragment function
			fixed4 frag(v2f IN) : SV_Target
			{
				//Wraps the texture around the UVs
				float4 texColor = tex2D(_MainTex, IN.uv);
				//Tints the texture
				return texColor * _Color;
			}//End frag
		ENDCG
	}
	}
}